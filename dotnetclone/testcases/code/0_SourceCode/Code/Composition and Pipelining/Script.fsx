// This file is a script that can be executed with the F# Interactive.  
// It can be used to explore and test the library project.
// Note that script files will not be part of the project build.

//
// Composition and Pipelining Operators
//

type Place = { Name: string; Population: int }

let places = [ { Name = "New York"; Population = 9000000; }
               { Name = "Los Angeles"; Population = 4000000; }
               { Name = "Frankfurt"; Population = 700000; }
               { Name = "Tokyo"; Population = 13000000;} ]

//Forward Pipeling Operator

let over5MilUppercase = 
    places
    |> List.filter (fun p -> p.Population > 5000000)
    |> List.map (fun p -> p.Name.ToUpper() )

(|>);;

let pf input func = func(input);;

(pf);;


//Backward Pipelining Operator

let over5MilUppercase = 
    List.map (fun (p: Place) -> p.Name.ToUpper() )
    <| (List.filter (fun (p: Place) -> p.Population > 5000000) 
        <| places)

(<|);;

let pb func input = func(input);;

(pb);;

Seq.map (fun x -> x * 2) (Seq.init 10 (fun i -> i * 2))

Seq.map (fun x -> x * 2) <| Seq.init 10 (fun i -> i * 2)

//Forward Composition Operator

let over5MilUppercase = 
    places
    |> (List.filter (fun p -> p.Population > 5000000)
        >> List.map (fun p -> p.Name.ToUpper() ))

(>>);;

let fc fun1 fun2 input = fun2( fun1( input ) );;

(fc);;


//Backward Composition Operator

let over5MilUppercase = 
    List.map (fun (p: Place) -> p.Name.ToUpper() )
    << List.filter (fun (p: Place) -> p.Population > 5000000)
    <| places

(<<);;

let fb fun1 fun2 input = fun1( fun2( input ) );;

(fb);;


//
// Applying Pipelining and Composition
//

let fcNoCurry fun1 fun2 = 
    (fun input -> fun2( fun1( input )))

places 
  |> (fun places -> List.filter (fun p -> p.Population > 5000000) places)
 
//
// Designing for Pipelining
//

type VacationLocation = 
    { Name: string; Pop: int; Density: int; Nightlife: int }

let destinations = 
    [ { Name = "New York"; Pop = 9000000; Density = 27000; Nightlife = 9 }
      { Name = "Munich"; Pop = 1300000; Density = 4300; Nightlife = 7 }
      { Name = "Tokyo"; Pop = 13000000; Density = 15000; Nightlife = 3 }
      { Name = "Rome"; Pop =  2700000; Density = 5500; Nightlife = 5 } ]

//From Loops to Pipelining Example 1

let findVacationImperitive data =
    let mutable outputList = []
    for i = List.length data - 1 downto 0 do
        let current = data.[i]
        let size = current.Pop / current.Density
        if current.Nightlife >= 5 && 
           size >= 200 && 
           current.Density <= 8000 
        then
            outputList <- List.Cons(current.Name, outputList) 
    outputList

findVacationImperitive destinations 

let findVacationPipeline data =
    data
    |> List.filter (fun x -> x.Nightlife >= 5)
    |> List.filter (fun x -> x.Pop / x.Density >= 200)
    |> List.filter (fun x -> x.Density <= 8000)
    |> List.map (fufindVacationPipeline destinationsn x -> x.Name)



//Combining Composition and Pipelining

let getSimpleVacationPipeline nightlifeMin sizeMin densityMax =
    List.filter (fun x -> x.Nightlife >= nightlifeMin)
    >> List.filter (fun x -> x.Pop / x.Density >= sizeMin)
    >> List.filter (fun x -> x.Density <= densityMax)

let myPipeline = getSimpleVacationPipeline 5 200 8000

let applyVacationPipeline data filterPipeline = 
    data 
    |> filterPipeline
    |> List.map (fun x -> x.Name)
    
applyVacationPipeline destinations myPipeline

//Advaced Composition and Pipelining

let getVacationPipeline nightlifeMin sizeMin densityMax searchName =   
    match nightlifeMin with
    | Some(n) -> List.filter (fun x -> x.Nightlife >= n) 
    | None -> id
    >> match sizeMin with 
       | Some(s) -> List.filter (fun x -> x.Pop / x.Density >= s)
       | None -> id
    >> match densityMax with
       | Some(d) -> List.filter (fun x -> x.Density <= d)
       | None -> id
    >> match searchName with
       | Some(sn) -> List.filter (fun x -> x.Name.Contains(sn))
       | None -> id

let myPipeline = getVacationPipeline (Some 5) (Some 200) (Some 8000) None

applyVacationPipeline destinations myPipeline

//Refactored Advanced Composition and Pipelining

let getVacationPipeline nightlifeMin sizeMin densityMax searchName =
    let getFilter filter some = 
        match some with
        | Some(v) -> List.filter (filter v)
        | None -> id
    getFilter (fun nlMin x -> x.Nightlife >= nlMin) nightlifeMin
    >> getFilter (fun sMin x -> x.Pop / x.Density >= sMin) sizeMin
    >> getFilter (fun dMax x -> x.Density <= dMax) densityMax
    >> getFilter (fun sName x -> x.Name.Contains(sName)) searchName
