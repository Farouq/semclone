#light

module SimpleCompositeTypes

open ProFSharp
open HelperTypes

// ======= Options =========
[<Example("Options 1")>]
let option_examples_1() =
    let nothing : string option = None
    let something : string option = Some("Ted Neward")
    System.Console.WriteLine("nothing = {0}", nothing)
    System.Console.WriteLine("something = {0}", something.Value)
    
[<Example("Options 2")>]
let option_examples_2() =
    let nothing : Option<string> = None
    let something : Option<string> = Some("Ted Neward")
    System.Console.WriteLine("nothing = {0}", nothing)
    System.Console.WriteLine("something = {0}", something.Value)

[<Example("Options 3")>]
let option_examples_3() =
    let possibleValue =
        if (System.DateTime.Now.Millisecond % 2) = 0 then
            None
        else
            Some("Have a happy day!")
    if possibleValue.IsSome then
        System.Console.WriteLine("Ah, we got a good value. Good!")
        System.Console.WriteLine(possibleValue.Value)

[<Example("Options 4")>]
let option_examples_4() =
    // This will deliberately throw an exception
    //
    let nothing : string option = None
    if nothing.Equals(None) then
        System.Console.WriteLine("None.Equals(None)")
    System.Console.WriteLine(nothing.GetHashCode())
    System.Console.WriteLine(nothing.ToString())

[<Example("Options 5")>]
let option_examples_5() =
    let possibleValue =
        if (System.DateTime.Now.Millisecond % 2) = 0 then
            None
        else
            Some("Have a happy day!")
    Option.iter (fun o -> System.Console.WriteLine(o.ToString()))
                possibleValue
                

// ======= Tuples ==========
[<Example("Tuples 1")>]
let tuple_examples_1() =
    let myName : (string * string) = ("Ted", "Neward")
    let myDescription : (string * string * int * int) = ("Ted", "Neward", 38, 98053)
    System.Console.WriteLine("Hello, {0}", myName)

[<Example("Tuples 2")>]
let tuple_examples_2() =
    let myName = ("Ted", "Neward")
    let herName = ("Sarah", "Michelle", "Gellar")
    let cityState = ("Phoenix", "AZ")
    System.Console.WriteLine("myName = herName? {0}", 
        myName.GetType().Equals(herName.GetType()))
    System.Console.WriteLine("myName = cityState? {0}", 
        myName.GetType().Equals(cityState.GetType()))
        
[<Example("Tuples 3")>]
let tuple_examples_3() =
    let me = ("Ted", "Neward")
    let firstName = fst me
    let lastName = snd me
    System.Console.WriteLine("Hello, {0} {1}", firstName, lastName)

[<Example("Tuples 4")>]
let tuple_examples_4() =    
    let me = ("Ted", "Neward", 38, "Redmond", "WA")
    let (firstName, lastName, age, city, state) = me
    System.Console.WriteLine("Hello, {0} {1}", firstName, lastName)
    
[<Example("Tuples 5")>]
let tuple_examples_5() =    
    let me = ("Ted", "Neward", 38, "Redmond", "WA")
    let (firstName, _, _, city, _) = me
    System.Console.WriteLine("Hello, {0}, how's {1}", firstName, city)

[<Example("Tuples 'for' examples")>]
let tuple_examples_6() =
    let people = [|
        ("Ted", "Neward", 38, "Redmond", "WA")
        ("Katie", "Ellison", 30, "Seattle", "WA")
        ("Mark", "Richards", 45, "Boston", "MA")
        ("Rachel", "Reese", 27, "Phoenix", "AZ")
        ("Ken", "Sipe", 43, "St Louis", "MO")
        ("Naomi", "Wilson", 35, "Seattle", "WA")
    |]
    for (firstName, _, age, _, _) in people do
        System.Console.WriteLine("{0} is {1}", firstName, age)
    

// ========= Arrays ==========
[<Example("Array initialization examples")>]
let array_examples_1() =
    let emptyArray = [| |]
    let arrayOfIntegers = [| 1; 2; 3; 4; |]
    let arrayOfStrings = [|
        "Fred"
        "Wilma"
        "Barney"
        "Betty"
    |]
    let arrayOfZeroes = Array.create 10 0
    let arrayOfTeds = Array.create 10 "Ted"
    let rangeOfIntegers = [| 1 .. 10 |]
    let rangeOfIntegers = [| 1 .. 2 .. 10 |]
    let rangeOfFloats = [| 0.0 .. 0.5 .. 10.0 |]
    let forBuiltArray = [| for i in 1 .. 10 -> i*i |]
    let mutableArray = Array.create 10 0
    for i = 0 to 9 do
        mutableArray.[i] <- i*i
    let (arrayOfObjects : obj array) = [| (1 :> obj); ("two" :> obj); (3.0 :> obj) |]
    ()
    
[<Example("Array access examples")>]
let array_examples_2() =
    let people = [|
        ("Ted", "Neward", 38, "Redmond", "WA")
        ("Katie", "Ellison", 30, "Seattle", "WA")
        ("Mark", "Richards", 45, "Boston", "MA")
        ("Rachel", "Reese", 27, "Phoenix", "AZ")
        ("Ken", "Sipe", 43, "St Louis", "MO")
        ("Naomi", "Wilson", 35, "Seattle", "WA")
    |]
    let thirdPerson = people.[2]
    // Happy Birthday, Mark!
    people.[2] <- ("Mark", "Richards", 46, "Boston", "MA")
    ()
    
[<Example("Array iteration examples")>]
let array_examples_3() =
    let array = [| "Ted"; "Katie"; "Mark"; "Rachel"; "Ken"; "Naomi" |]
    for i = 0 to array.Length - 1 do
        System.Console.WriteLine(array.[i])
    for p in array do
        System.Console.WriteLine(p)
    Array.iter (fun it -> System.Console.WriteLine(it.ToString())) array
    ()

[<Example("Array operation examples")>]
let array_examples_4() =
    let people = [|
        new Person("Ted", "Neward", 38)
        new Person("Mark", "Richards", 45)
        new Person("Ken", "Siple", 43)
        new Person("Naomi", "Wilson", 38)
        new Person("Michael", "Neward", 16)
        new Person("Matthew", "Neward", 9)
    |]
    let newardsFound = 
        Array.find (fun (it : Person) -> it.LastName = "Neward") people
    System.Console.WriteLine(newardsFound)
    let drinkers =
        Array.filter (fun (it : Person) -> it.Age > 21) people
    Array.iter (fun (it : Person) -> 
        System.Console.WriteLine("Have a beer, {0}!", it.FirstName) )
        drinkers
    people
        |> Array.filter (fun (it : Person) -> it.Age > 21)
        |> Array.iter (fun (it : Person) -> 
            System.Console.WriteLine("Have a beer, {0}!", it.FirstName))
    let isADrinker (ar : Person array) = 
        Array.filter (fun (p : Person) -> p.Age > 21) ar
    let haveABeer (ar : Person array) = 
        Array.iter (fun (p : Person) -> System.Console.WriteLine("Have a beer, {0}!", p.FirstName) ) ar
    people |> isADrinker |> haveABeer

// ========= Lists ==========
[<Example("List examples 1")>]
let list_examples_1() =
    let emptyList = []
    let listOfIntegers = [ 1; 2; 3; 4; ]
    let listOfStrings = [
        "Fred"      // Flintstone
        "Wilma"     // Flintstone
        "Barney"    // Rubble
        "Betty"     // Rubble
        ]
    let listOfPeopleTuples = [
        ("Ted", "Neward", 38, "Redmond", "WA")
        ("Katie", "Ellison", 30, "Seattle", "WA")
        ("Mark", "Richards", 45, "Boston", "MA")
        ("Rachel", "Reese", 27, "Phoenix", "AZ")
        ("Ken", "Sipe", 43, "St Louis", "MO")
        ("Naomi", "Wilson", 35, "Seattle", "WA")
        ]
    let listOfIntegersToTen = [ 1 .. 10 ]  // [ 1; 2; 3; ... 10; ]
    let listOfEvenIntsToTen = [ 0 .. 2 .. 10 ] 
        // [ 0; 2; 4; ... 10; ]
    let listOfFloatsToTen = [ 0.0 .. 0.5 .. 10.0 ]
    let consedList = 1 :: 2 :: 3 :: 4 :: []
//    let mutable forBuiltList = []
//    for i = 1 to 10 do
//        forBuiltList <- (i * i) :: forBuiltList
    let forBuiltList = [ for i in 1 .. 10 -> i * i]
    let concattedList = listOfIntegers @ consedList
        // [ 1; 2; 3; 4; 1; 2; 3; 4; ]
    listOfStrings

[<Example("List examples 2")>]
let list_examples_2() =
    //let notWorkingList = [ 1; "2"; 3.0; ]
    let (objectList : obj list) = [
        (1 :> obj)
        ("2" :> obj)
        (3.0 :> obj)
    ]
    objectList

[<Example("List access examples")>]
let list_examples_3() =
    let people = [
        ("Ted", "Neward", 38)
        ("Mark", "Richards", 45)
        ("Naomi", "Wilson", 38)
        ("Ken", "Sipe", 43)
    ]
    let peopleHead = people.Head
    System.Console.WriteLine(peopleHead)
    let firstPerson = List.head people
    System.Console.WriteLine(firstPerson)
    let (personOne :: rest) = people
    System.Console.WriteLine(personOne)
    let secondPerson = people.[1]
    System.Console.WriteLine(secondPerson)
    let otherSecondPerson = List.nth people 1
    let noSuchPerson = List.nth people 5
    System.Console.WriteLine("Expecting an exception above")

// ============== Sequences
[<Example("Sequence initializers")>]
let seq_examples_1() =
    let threeNums = seq {0 .. 2}
    let lotsOfNums = seq {0 .. 100000000}
    let people = [
        new Person("Ted", "Neward", 38)
        new Person("Mark", "Richards", 45)
        new Person("Naomi", "Wilson", 38)
        new Person("Ken", "Sipe", 43)
    ]
    let seqPeople = Seq.ofList people
    let newards = Seq.filter (fun (it : Person) -> it.LastName = "Neward") seqPeople
    Seq.iter (fun (it : Person) -> System.Console.WriteLine("Found " + it.ToString())) newards
    let x = seq { for i = 1 to 10 do yield i }
    System.Console.WriteLine(x)
    let y = seq { for i = 1 to 10 do
                    System.Console.WriteLine("Generating {0}", i)
                    yield i }
    let yEnum = y.GetEnumerator()
    if yEnum.MoveNext() then
        System.Console.WriteLine(yEnum.Current)
    let randomNumberGenerator minVal maxVal =
        let randomer = new System.Random(System.DateTime.Now.Millisecond)
        seq { while (true) do yield (randomer.Next(maxVal - minVal) + minVal) }
    let diceRolls = (randomNumberGenerator 3 18) |> Seq.take 6
    Seq.iter (fun (roll : int) -> System.Console.WriteLine("You rolled a " + roll.ToString())) diceRolls

    let squares = seq { for i in 0 .. 10 -> (i, i*i) }
    System.Console.WriteLine(squares)

[<Example("Sequence directory operations")>]
let seq_examples2() =
    let dir d = 
        let di = new System.IO.DirectoryInfo(d)
        seq { for fi in di.GetFileSystemInfos() -> fi }
    let printFileInfo (fi : System.IO.FileSystemInfo) =
        System.Console.WriteLine("{0}", fi.FullName)
    let rootFiles = dir "C:\\"
    for fi in rootFiles do printFileInfo fi
    
    (* Uncomment this if you really want to iterate the
       entire contents of your hard drive :-) *)
    (*
    let rec recursiveDir d =
        let di = new System.IO.DirectoryInfo(d)
        seq {
            for f in di.GetFiles() do yield f
            for sd in di.GetDirectories() do yield! recursiveDir sd.FullName 
        }
    let allFiles = recursiveDir @"C:\"
    for fi in allFiles do printFileInfo fi
    *)
    ()

// ============== Maps
[<Example("Map creation example")>]
let map_creation_example() =
    let nicknames = Map.ofList [ 
                        "Ted", new Person("Ted", "Neward", 38);
                        "Katie", new Person("Katie", "Ellison", 30);
                        "Michael", new Person("Michael", "Neward", 16) 
                    ]
    let moreNicknames = 
        nicknames.Add("Mark", new Person("William", "Richards", 45))
    let numberMappings = dict [ (1, "One"); (2, "Two"); (3, "Three") ]
    let nicknames = 
        dict [ 
            ("Ted", new Person("Theodore", "Neward", 38))
            ("Naomi", new Person("Naomi", "Wilson", 38))
            ("Ken", new Person("Kenneth", "Sipe", 45)) 
        ]
    ()

[<Example("Map access example")>]
let map_access_example() =
    let nicknames = 
        Map.ofList [
            ("Ted", new Person("Theodore", "Neward", 38))
            ("Naomi", new Person("Naomi", "Wilson", 38))
            ("Ken", new Person("Kenneth", "Sipe", 45)) 
        ]
    let ted = nicknames.["Ted"]
    System.Console.WriteLine(ted)
    let ted = Map.find "Ted" nicknames
    System.Console.WriteLine(ted)

    try
        let noone = nicknames.["Katie"]
        System.Console.WriteLine(noone)
    with 
    | ex -> System.Console.WriteLine("Katie not found")
    
    try
        let noone = Map.find "Katie" nicknames
        System.Console.WriteLine(noone)
    with
    | ex -> System.Console.WriteLine("Katie not found")
    
    let notfound = nicknames.TryFind("Katie")
    System.Console.WriteLine(
        if notfound = None then "Not found" 
        else notfound.Value.ToString()
    )
    let notfound = Map.tryFind "Katie" nicknames
    System.Console.WriteLine(
        if notfound = None then "Not found" 
        else notfound.Value.ToString()
    )

// ============== Sets
[<Example("Set examples")>]
let set_examples1() =
    let setOfPeople = Set.ofList [ new Person("Ted", "Neward", 38);
                                   new Person("Ted", "Neward", 38);
                                   new Person("Ted", "Neward", 38); ]
    for p in setOfPeople do
        System.Console.WriteLine(p)

    let setOfNicknames = set [ "Ted"; "Theo"; "Tio"; "Ted"; "Ted"; "Teddy" ]
    for p in setOfNicknames do
        System.Console.WriteLine(p)
        
    ()

