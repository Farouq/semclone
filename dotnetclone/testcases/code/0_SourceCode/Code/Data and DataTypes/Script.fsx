// This file is a script that can be executed with the F# Interactive.  
// It can be used to explore and test the library project.
// Note that script files will not be part of the project build.

#load "DataAndDataTypes.fs"
open DataAndDataTypes


//State Safety - Concurrent Object Access Example
open System
open System.Threading

//type UnsafeAccessClass() = 
//    let mutable i = 0
//    member this.IncrementTenAndGetResult() =
//        i <- i + 10
//        i

type UnsafeAccessClass() = 
    let mutable i = 0
    member this.IncrementTen() = 
        i <- i + 10
        this
    member this.GetResult() = i

type SafeAccessClass(i) =
    new() = SafeAccessClass (0)
    member this.IncrementTen() = new SafeAccessClass(i + 10)
    member this.GetResult() = i
    
let asyncExecuteClass (ac: UnsafeAccessClass) (firstSleep: int, secondSleep: int) = 
    async {
        Thread.Sleep( firstSleep*firstSleep*20 )
        let incremented = ac.IncrementTen()
        Thread.Sleep( secondSleep*secondSleep*20 )
        let result = incremented.GetResult()
        return result, DateTime.Now.Ticks
    }

let compareClassExecution firstThreadSleeps secondThreadSleeps = 
    let ac = new UnsafeAccessClass()
    let threadWork = [ asyncExecuteClass ac firstThreadSleeps; 
                       asyncExecuteClass ac secondThreadSleeps]
    let results = Async.RunSynchronously (Async.Parallel threadWork)
    if snd results.[0] < snd results.[1] then 
        [ "1st", (fst results.[0]); "2nd", (fst results.[1]) ]
    else 
        [ "2nd", (fst results.[1]); "1st", (fst results.[0]) ]
    
//Execution Order: 1,1,2,2
let case1 = compareClassExecution (1, 2) (3, 4)
//Execution Order: 2,2,1,1
let case2 = compareClassExecution (3, 4) (1, 2)
//Execution Order: 1,2,1,2
let case3 = compareClassExecution (1, 3) (2, 4)
//Execution Order: 2,1,2,1
let case4 = compareClassExecution (2, 4) (1, 3)
//Execution Order: 1,2,2,1
let case5 = compareClassExecution (1, 4) (2, 3)
//Execution Order: 2,1,1,2
let case6 = compareClassExecution (2, 3) (1, 4)

//
//--State Safety
//

//<Bad Ordering Example>
//Initialize()
//Perform()

open System

exception NotInitialized

type InitializeClass() =
  let mutable initialized = false;
  member this.Initialize() = 
    initialized <- true
  member this.Perform() =
    if not initialized then raise NotInitialized

//<Unexpected Side Effect Example>
//Manager Class -> 2 Children
type ListGenerationManager() = 
    let mutable GenerateInstances = 0
    member this.InstancesToGenerate
        with get () = GenerateInstances
        and set (value) = GenerateInstances <- value
    member this.GetIntegerGenerator() = new IntegerListGenerator(this)
    member this.GetFloatGenerator() = new FloatListGenerator(this)
and IntegerListGenerator(manager: ListGenerationManager) =
    member this.GenerateInstances() = List.init manager.InstancesToGenerate (fun i -> int i)
and FloatListGenerator(manager: ListGenerationManager) =
    member this.GenerateInstances() = List.init manager.InstancesToGenerate (fun i -> float i)
    
//--Data Safety
open System.Collections.Generic
open System

//<List<T> Example>
//Create List, Reorder
type ListData =
    struct 
      val mutable Number: int
      val mutable Name: string
      new( number, name ) = { Number = number; Name = name }
      override this.ToString() = String.Format( "Number: {0}, Name: {1}", this.Number, this.Name )
    end

let clrList = new List<ListData>( [| new ListData(1, "Johnny"); 
                                     new ListData(5, "Lisa"); 
                                     new ListData(3, "Mark") |]);
clrList.ToArray()

let sortedClrList = 
    let ListDataComparison (d1:ListData) (d2:ListData) = 
        d1.Number - d2.Number in
        clrList.Sort( ListDataComparison )
    clrList

sortedClrList.ToArray()
clrList.ToArray()

let reversedClrList =
    clrList.Reverse()
    clrList

reversedClrList.ToArray()
sortedClrList.ToArray()
clrList.ToArray()

//<member mutation won't compile example>

let incrementClrList = 
   clrList.ForEach( (fun data -> data.Number <- data.Number + 1) )
   clrList
   
//<With Clone()>
//Create List of Lists, Reorder
        
let clrList = new List<ListData>( [| new ListData(1, "Johnny"); 
                                     new ListData(5, "Lisa"); 
                                     new ListData(3, "Mark") |]);
let sortedClrList = 
    let newList = new List<ListData>(clrList)
    let ListDataComparison (d1:ListData) (d2:ListData) = 
        d1.Number - d2.Number in
        newList.Sort( ListDataComparison )
    newList

sortedClrList.ToArray()
clrList.ToArray()
  
//<Deep Clone()>
let deepCloneList (list: List<ListData>) =
    let newList = new List<ListData>()
    list.ForEach( (fun data -> 
        newList.Add( new ListData( data.Number, data.Name ) ) ) )
    newList

//<F# List>
let initialList = [ new ListData(1, "Johnny"); 
                    new ListData(5, "Lisa"); 
                    new ListData(3, "Mark") ];

let sortedList = 
    let numberSelector (d: ListData) = d.Number in 
        List.sortBy numberSelector initialList

let reversedList = List.rev initialList

//--Avoiding Mutation
//<Mutable Example>
//Loop, changing data structure over iteration.
let leastFactor n = 
    let max = int (sqrt (float n))
    let mutable lastTried = 1
    let mutable keepLooping = true
    while keepLooping do
        lastTried <- lastTried + 1
        if n % lastTried = 0 || lastTried > max
        then keepLooping <- false
    if lastTried > max then n else lastTried      
        
let bucketByLFs numbers =
    let mutable buckets = Array.create (1 + List.max numbers) []
    for number in numbers do
        let lf = leastFactor number
        buckets.[lf] <- [number] @ buckets.[lf]
    buckets

let printFactorBuckets factorBuckets = 
    for i in 0 .. (Array.length factorBuckets) - 1 do
        let factoredList = factorBuckets.[i]
        if not (List.isEmpty factoredList) then
            printfn "%d -> %A" i factoredList

let factorBuckets = bucketByLFs [ 0 .. 25 ]
printFactorBuckets factorBuckets

//<Nomutable Example>
//Recurse, Passing value back up the call stack and using it in the next iteration.

let leastFactor n = 
    let maxFactor = int (sqrt (float n))
    let rec factorTest = function
        | i when i > maxFactor -> n
        | i when n % i = 0 -> i 
        | i -> factorTest (i + 1)
    factorTest 2
    
let bucketByLeastFactors (numbers: int list) = 
    let rec addToBucket bucketIndex n bucketsList =
        let neededBuckets = bucketIndex - List.length bucketsList 
        if neededBuckets >= 0 then            
            addToBucket bucketIndex n
                (bucketsList @ [ for i in 0 .. neededBuckets -> [] ])
        else 
            List.mapi 
                (fun i bucket -> 
                    if bucketIndex = i then ([n] @ bucket) 
                    else bucket) 
                bucketsList
    List.fold 
        (fun bucketsList n -> (addToBucket (leastFactor n) n bucketsList)) 
        List.empty 
        numbers
   
let printFactorBuckets factorBuckets = 
    List.iteri 
        (fun i factored -> 
            if List.length factored > 0 
            then printfn "%d -> %A" i factored) 
        factorBuckets 

let factorBuckets = bucketByLeastFactors [ 0 .. 25 ]
printFactorBuckets factorBuckets

//--Bubble and Swap
//Get number, index and swap

let simulationIsRunning() = true
let getInitialWorldStateArray() = Array.init 100 (fun i -> Array.init 100 (fun i -> None) )
let getWorldStateChanges worldState = Array.empty
let updateUI worldState = ()
//Above is just here to make this sample compile

let simulationLoop () = 
    let worldState = getInitialWorldStateArray()
    while simulationIsRunning() do
        let changes = getWorldStateChanges(worldState)
        for change in changes do
            let i, j, newValue = change
            worldState.[i].[j] <- newValue
        updateUI(worldState)
     
//<ref keyword>
//Pass back up the stack and swap values close by

let refInt = ref 10
refInt := 20
refInt
let normalInt = ! refInt


//<example of ref keyword use>
let updateGameStateFromUserInput gameIsRunning worldState = worldState |> ignore
let updateGameStateFromActors worldState = worldState |> ignore
//Above is just here to make this sample compile

let gameLoop actors display input =
    let mutable gameIsRunning = true
    let worldState = ref (getInitialWorldStateArray())
    let applyChanges changes = 
        for (i,j,value) in changes do
            (!worldState).[i].[j] <- value
    while gameIsRunning do
        for actor in actors do
            applyChanges (actor.getChanges !worldState)
        applyChanges (input.getChanges !worldState) 
        display.update !worldState
        gameIsRunning <- not input.quit

//<Example of bad use of mutable data - Swap Example>
//Global variables, swapping in many places


let gameIsRunning = ref true
let worldState: option< option<int> array array > ref = ref None

let initializeWorldStateArray () =
    worldState := Some(getInitialWorldStateArray())
    
let updateGameStateFromActors () = 
    for actor in !actors do
        actor.updateWorldState worldState

let updateGameStateFromUserInput () =
    userInput.updateWorldState worldState
    gameIsRunning := not userInput.quit

let updateUI () = 
    ui.update worldState

let gameLoop () = 
    initializeWorldStateArray()
    while !gameIsRunning do
        updateGameStateFromActors()
        updateUI()
        updateGameStateFromUserInput()


//<byref keyword>
let mutable number = 10

let doubleNumber (number: int byref) =
    number <- 2 * number

doubleNumber (&number)

number

//<byref example>
let gameLoop actors display input =
    let mutable gameIsRunning = true
    let mutable worldState = getInitialWorldStateArray()
    let applyChanges changes (worldState: 'a option [] [] byref) = 
        List.iter 
            (fun (i,j,value) -> (worldState).[i].[j] <- value) 
            changes    
    while gameIsRunning do
        for actor in actors do 
            applyChanges (actor.getChanges worldState) (&worldState)
        applyChanges (input.getChanges worldState) (&worldState)
        display.update worldState
        gameIsRunning <- not input.quit 

//--Performance Considerations
//<Very Slow List Reverse Example>

let rec badReverse list = 
    if List.isEmpty list then [] 
    else badReverse list.Tail @ [list.Head]

badReverse [0 .. 100000];;

//<Fairly fast append List Reverse Example
let appendReverse list = 
    let rec recReverse rest reversed =
        if List.isEmpty rest then reversed
        else recReverse rest.Tail ([rest.Head] @ reversed)
    recReverse list []

appendReverse [0 .. 100000];;

//<Fast List Reverse Cons Example
let consReverse list = 
    let rec recReverse rest reversed =
        if List.isEmpty rest then reversed
        else recReverse rest.Tail (rest.Head :: reversed)
    recReverse list []

let list = [0 .. 1000000];;

appendReverse list;;
consReverse list;;

//<Fast Pattern Matching Reverse Example>
let patternMatchingReverse list = 
    let rec recReverse rest reversed = 
        match rest with 
        | [] -> reversed
        | head::tail -> recReverse tail (head::reversed)
    recReverse list []
    
patternMatchingReverse list;;
    
//<List Module Reverse Example>
List.rev [0 .. 100000];;

//<Seq Iteration>
//Construct a sequence via a comprehension to return numbers
//Process numbers with a couple of other functions
let simpleSeq = seq { for i in 0 .. 1000000 do yield i }
for i in 0 .. 10 do Seq.iter (fun i -> ()) simpleSeq


//<Seq -> List -> Iteration>
let listFromSeq = Seq.toList simpleSeq
for i in 0 .. 10 do List.iter (fun i -> ()) listFromSeq

//<Recursively process Record and Struct>
//Simple math example, perhaps a polygonF

type StructData =
    struct
      val R: byte
      val G: byte
      val B: byte
      new( r, g, b ) = { R = r; G = g; B = b }
    end

//type RecordData = { R: byte; G: byte; B: byte }

let pixels = 1920 * 1200

let structArray = [| for i in 0 .. pixels do 
                        yield new StructData( 0uy, 0uy, 0uy )|]

//let recordArray = [| for i in 0 .. pixels do 
//                        yield { R = 0uy; G = 0uy; B = 0uy }|]

//<Mutable vs Immutable Field Iteration>

let pixels = 3600 * 5400 //20MP Camera

let structArray = [| for i in 0 .. pixels do 
                        yield new StructData( 1uy, 1uy, 1uy )|]
                        
let averageMagnitudeOfStruct array = 
    let total = Array.fold (fun acc (pixel: StructData) -> 
        acc + (uint64 pixel.R) + (uint64 pixel.G) + (uint64 pixel.B)) 0UL array
    total / uint64 (array.Length * 3)

averageMagnitudeOfStruct structArray

type MutableStructData =
    struct
      val mutable R: byte
      val mutable G: byte
      val mutable B: byte
      new( r, g, b ) = { R = r; G = g; B = b }
    end

let mutableStructArray = [| for i in 0 .. pixels do 
                            yield new MutableStructData( 1uy, 1uy, 1uy )|]

let averageMagnitudeOfMutableStruct array = 
    let total = Array.fold (fun acc (pixel: MutableStructData) -> 
        acc + (uint64 pixel.R) + (uint64 pixel.G) + (uint64 pixel.B)) 0UL array
    total / uint64 (array.Length * 3)

averageMagnitudeOfMutableStruct mutableStructArray

//--Specificity
//<The safety of specificity demonstrated>
//Pass an ordered list into a function which changes its order
//then pass it into another function which requires it
//..This condition is expesnive to check for.  

let rec nLargestNumbers n orderedNumbers =
    let rec nFirstElements n list =
        match n with
        | 0 -> []
        | _ -> match list with
               | [] -> []
               | h::t -> [h] @ nFirstElements (n - 1) t
    nFirstElements n orderedNumbers
        
//--Option as an Example

let formatData data = Some(data)
let getWebData url = Some("")

let getFormattedWebData url = 
    let webData = getWebData url
    match webData with
    | Some(data) -> formatData data
    | None -> None

//--Encapsulating State in Types
//<Example of O(n) or O(n^2)>
//Make sorted list, Remove Duplicates

let removeDupesFromOrdered orderedList = 
    List.foldBack (fun element noDupes ->
        match noDupes with
        | [] -> [element]
        | (h::_) -> if element = h then noDupes
                    else [element] @ noDupes)         
        orderedList []

removeDupesFromOrdered [0 .. 100000];;


//<F# Sorted Example>
//Define a sorted list and a method which takes a normal list and returns a sorted one

type OrderedList<'a> = | OrderedList of 'a list
    
let ToOrderedList list = OrderedList(List.sort list)

let ToList (orderedList: OrderedList<'a>) = 
    match orderedList with
    | OrderedList list -> list
    
let removeDuplicatesFromOrderedList (orderedList: OrderedList<'a>) =
    let removeDupes list = 
        List.foldBack (fun element noDupes ->
            match noDupes with
            | [] -> [element]
            | (h::_) -> if element = h then noDupes
                        else element :: noDupes)      
            list []
    match orderedList with
    | OrderedList list -> OrderedList(removeDupes list)

let orderedList = ToOrderedList [0; 1; 1; 2; 2; 3]
removeDuplicatesFromOrderedList orderedList

type PropertyList<'a> = 
    | Normal of 'a list 
    | Ordered of 'a list
    | NoDuplicates of 'a list
    
let CreateNoDuplicatesListFromPropertyList list = 
    function
    | NoDuplicates(_) -> list
    | Normal(noProperty) -> 
        NoDuplicates (noProperty |> Set.ofList |> List.ofSeq)
    | Ordered(ordered) -> 
        NoDuplicates (List.foldBack (fun element noDupes ->
            match noDupes with
            | [] -> [element]
            | (h::_) -> if element = h then noDupes
                        else element :: noDupes) ordered [])
                                        
//--Avoiding Exceptions
//<Normal Exception vs Exceptional case encoded in type>
//Throw exceptions on failed call or bad inputs, vs 
//Discriminated union returning a message

open System
open System.Net
open System.Text

type UriOutput =
    | Uri of Uri
    | Malformed of string

let buildUri stringUri =
    try Uri( new Uri(stringUri) )
    with | _ -> Malformed(stringUri)


//--Data and State Flow
//Cases for each state
//<Handling return states>
//explicit match
//<Handling return states with wildcards>
//match handling only cases we care about

open System
open System.Net
open System.Text

type WebClientInput =
    | StringInput of String
    | UriInput of Uri
    
type WebClientOutput =
    | MalformedUri of string
    | TextContent of string 
    | BinaryContent of byte []
    | NoContent
    | WebClientException of WebException
    
let downloadWithWebClient (inputUri: WebClientInput) =
    let downloadFromUri (uri: Uri) =    
        try 
            use client = new WebClient()
            let dlData = client.DownloadData(uri)
            if dlData.Length = 0 then NoContent
            else if (client.ResponseHeaders.["content-type"]
                           .StartsWith(@"text/")) 
                then
                    let dlText = Encoding.Default.GetString(dlData)
                    TextContent(dlText)
                else
                    BinaryContent(dlData)
        with
           | :? WebException as e -> WebClientException(e)
    match inputUri with
    | UriInput(classUri) -> downloadFromUri classUri
    | StringInput(stringUri) ->
        match buildUri stringUri with
        | Uri(s) -> downloadFromUri s
        | Malformed(s) -> MalformedUri(s)

let printWebClientOutput clientOutput =
    match clientOutput with
    | MalformedUri(uri) -> printfn "Input Uri was malformed: %s" uri
    | TextContent(content) -> printfn "Page Content: %s" content
    | BinaryContent(content) -> printfn "Binary Data: %d" content.Length
    | NoContent -> printfn "No content was found."
    | WebClientException(e) -> printfn "Exception: %s" (e.ToString())

open System.IO

let downloadToFile (inputUri: WebClientInput) outputLocation =
    match downloadWithWebClient inputUri with 
    | TextContent(text) -> File.WriteAllText( outputLocation, text )
    | BinaryContent(binary) -> File.WriteAllBytes( outputLocation, binary )
    | _ -> printfn "Download Failed"

//--Recursively Defined Data Types

type BinaryTree<'a> =
    | Node of BinaryTree<'a> * BinaryTree<'a>
    | Leaf of 'a
    
//Define binary tree
let binaryTree = 
    Node(
        Node( Leaf(1), Leaf(2) ),
        Node( Leaf(3), Node( Leaf(4), Leaf(5) ) ) )

//write depth first search in f#

let rec dfs tree leafData = 
    match tree with
    | Leaf(l) -> if l = leafData then Some(l) else None
    | Node(a,b) -> let dfsA = dfs a leafData in
                   if Option.isSome dfsA then dfsA
                   else dfs b leafData

dfs binaryTree 5;;

dfs binaryTree 33;