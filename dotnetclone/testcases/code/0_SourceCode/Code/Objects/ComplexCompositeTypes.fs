#light

module ComplexCompositeTypes

open ProFSharp

// ======================== Type abbreviations
// Open question: Does hanging a custom attribute off a type abbreviation
// have any effect? Should it? Or should it be a compiler bug?

type MenuItem =
    string * string * float

type RestaurantMenu =
    MenuItem list

type ui32 = System.UInt32

[<Example("Type abbreviations examples")>]
let typeabbrev_usage() =
    let diner : RestaurantMenu = [
        ("Grand Slam", "Two eggs, two bacon, three pancakes", 2.99);
        ("Chicken strips", "Five strips and sauce", 3.99) 
    ]
    for (name, desc, price) in diner do
        System.Console.WriteLine("{0} costs {1}", name, price)



// ======================== Enumerated types
type Soda =
    | Coke = 1
    | DietCoke = 2
    | SevenUp = 3

[<System.Flags>]
type SuicideSoda =
    | Coke = 0x0001
    | DietCoke = 0x0002
    | SevenUp = 0x0004
    | Grenadine = 0x0008

[<Example("Enum usage example")>]
let enum_usage() =
    let drink = Soda.DietCoke
    let message = 
        match drink with
        | Coke -> "Ah, so refreshing!"
        | DietCoke -> "Just one calorie!"
        | _ -> "Bleah"
    System.Console.WriteLine(message)    

    let message = 
        match drink with
        | Soda.Coke -> "Ah, so refreshing!"
        | Soda.DietCoke -> "Just one calorie!"
        | _ -> "Bleah"
    System.Console.WriteLine(message)    
    
    let rawValue = int Soda.DietCoke

    let rawInt = 20
    let unknownDrink = enum<Soda>(rawInt)
    let message = 
        match drink with
        | Soda.Coke -> "Ah, so refreshing!"
        | Soda.DietCoke -> "Just one calorie!"
        | Soda.SevenUp -> "Clear soda!"
        | _ -> failwith "This should never happen!"
    System.Console.WriteLine(message)    

    let perfectDrink = SuicideSoda.Coke &&& SuicideSoda.Grenadine
    System.Console.WriteLine("It contains Coke? {0}",
        (if perfectDrink &&& SuicideSoda.Coke = SuicideSoda.Coke 
            then "true" else "false"))
    System.Console.WriteLine("It contains DietCoke? {0}",
        (if perfectDrink &&& SuicideSoda.DietCoke = 
            SuicideSoda.Coke then "true" else "false"))

    let enumNames = System.Enum.GetNames(typeof<Soda>)
    let enumValues = System.Enum.GetValues(typeof<Soda>)
    (* This doesn't compile; not sure if I can make it do so *)
    (*
    let isFlagSet (enum : #System.Enum) (flag : #System.Enum) =
        let enumType = enum.GetType()
        if System.Enum.IsDefined(enumType, flag) then
            if (int enum) &&& (int flag) = (int flag) then
                true
            else
                false
        else
            false
    *)
    ()

    

// ======================== Discriminated unions
type Color =
    | RGB of int * int * int
    | CMYK of int * int * int * int
    | Black
    | Blue
    | Green
    | Red
    | White
    | Cyan
    | Gray
    member this.RGBValue =
        match this with
        | RGB(r,g,b) -> (r, g, b)
        | Red -> (255, 0, 0)
        | Green -> (0, 255, 0)
        | Blue -> (0, 0, 255)
        | Black -> (0, 0, 0)
        | White -> (255, 255, 255)
        | Cyan -> (64, 128, 128)
        | Gray -> (192, 192, 192)
        | CMYK(c,m,y,k) ->
            failwith "I have no idea how to do that"

[<Example("Discriminated union examples")>]
let discriminatedunion_examples() =
    let c = Black
    System.Console.WriteLine(c)
    let message =
        match c with
        | Black -> "Black"
        | _ -> "Not black"
    System.Console.WriteLine(message)
    let black = RGB(0,0,0)
    System.Console.WriteLine(c)
    let colorStr =
        match c with
        | Black | RGB(0,0,0) -> "Black"
        | White | RGB(255, 255, 255) -> "White"
        | Red | RGB(255, 0, 0) -> "Red"
        | Blue | RGB(0, 0, 255) -> "Blue"
        | Cyan | RGB(64, 128, 128) -> "Cyan"
        | Gray | RGB(192, 192, 192) -> "Gray"
        | Green | RGB(0, 255, 0) -> "Green"
        | RGB(r,g,b) ->
            System.String.Format("({0},{1},{2})",
                r, g, b)
        | CMYK(c,m,y,k) ->
            System.String.Format("[{0},{1},{2},{3}]",
                c,m,y,k)
    System.Console.WriteLine(colorStr)

type BinaryTree =
    | Node of obj * BinaryTree * BinaryTree
    | Empty
    member bt.Contents =
        match bt with
        | Empty -> ""
        | Node(data, left, right) ->
            "(" + left.Contents + ")" +
            ":" + data.ToString() + ":" +
            "(" + right.Contents + ")"
    member bt.iter (fn : (obj) -> unit ) =
        match bt with
        | Empty -> ()
        | Node(data, left, right) ->
            left.iter(fn)
            fn(data)
            right.iter(fn)

[<Example("More discriminated union (tree) examples")>]
let tree_examples() =
    let tree =
        Node("Ted",
            Node("Aaron",
                Empty,
                Empty),
            Node("Talbott",
                Node("Rick", Empty, Empty),
                Empty))
    System.Console.WriteLine("Contents = {0}", tree.Contents)
    tree.iter(System.Console.WriteLine)

type Employee =
    | Grunt of string
    | Manager of string * Employee list
    member e.Name =
        match e with
        | Grunt(n) -> n
        | Manager(n, _) -> n
    member e.Subordinates =
        match e with
        | Grunt(_) -> []
        | Manager(_, es) -> es
    member e.Empire =
        match e with
        | Grunt(_) -> []
        | Manager(_, es) ->
            List.collect 
                (fun (e : Employee) -> e.Empire) es

[<Example("More discriminated union examples")>]
let employeetree_examples() =
    let aaron = Grunt("Coder")
    let CEO = Manager("CEO", [ aaron ])
    let CFO = Manager("CFO", [ aaron ])
    let VP_RD = Manager("VP, R&D", [aaron])
    ()

type State =
    | New
    | Opened
    | Closed
    member s.Open() =
        match s with
        | New -> Opened
        | Opened -> 
            failwith "Error to Open an Opened state"
        | Closed ->
            Opened
    member s.Close() =
        match s with
        | New ->
            failwith "Error to Close a New state"
        | Opened -> Closed
        | Closed -> Closed



// ======================== Structs

[<Struct>]
type Point(x : int, y : int) =
    member pt.X = x
    member pt.Y = y
    override pt.ToString() =
        System.String.Format("({0},{1})", x, y)

type AnotherPoint(x : int, y: int) =
    struct
        member pt.X = x
        member pt.Y = y
    end

[<Struct>]
type MutPoint =
    val mutable X : int
    val mutable Y : int
    
[<Example("Struct examples")>]
let struct_examples() =
    let origin = new Point()
    System.Console.WriteLine("Point = {0},{1}", origin.X, origin.Y)
    let notOrigin = new Point(12, 12)
    System.Console.WriteLine("Point = {0},{1}", 
        notOrigin.X, notOrigin.Y)
        
    let newPoint = new Point((notOrigin.X - 6), (notOrigin.Y) - 6)
    System.Console.WriteLine("Point = {0},{1}", 
        newPoint.X, newPoint.Y)

    let mutable mutPt = new MutPoint()
    mutPt.X <- 10
    mutPt.Y <- 10
    System.Console.WriteLine("Point = {0},{1}", 
        mutPt.X, mutPt.Y)
    
    let a = new Point(12, 12)
    let b = new Point(12, 12)
    System.Console.WriteLine("a = b? {0}",
        if (a = b) then "yes" else "no")
    System.Console.WriteLine("a.Equals(b)? {0}",
        if (a.Equals(b)) then "yes" else "no")
    System.Console.WriteLine("a <> b? {0}",
        if (a <> b) then "yes" else "no")
    let c = new Point(6, 12)
    let d = new Point(18, 12)
    let e = new Point(12, 6)
    let f = new Point(12, 18)
    System.Console.WriteLine("a > c? {0}",
        if (a > c) then "yes" else "no") // yes
    System.Console.WriteLine("a > d? {0}",
        if (a > d) then "yes" else "no") // no
    System.Console.WriteLine("a > e? {0}",
        if (a > e) then "yes" else "no") // yes
    System.Console.WriteLine("a > f? {0}",
        if (a > f) then "yes" else "no") // no
    System.Console.WriteLine("a < c? {0}",
        if (a < c) then "yes" else "no") // no
    System.Console.WriteLine("a < d? {0}",
        if (a < d) then "yes" else "no") // yes
    System.Console.WriteLine("a < e? {0}",
        if (a < e) then "yes" else "no") // no
    System.Console.WriteLine("a < f? {0}",
        if (a < f) then "yes" else "no") // yes
    System.Console.WriteLine("a.GetHashCode() = {0}",
        a.GetHashCode())
    System.Console.WriteLine("b.GetHashCode() = {0}",
        b.GetHashCode())
    System.Console.WriteLine("a hash = b hash? {0}",
        a.GetHashCode() = b.GetHashCode())

    let message =
        match (a.X, a.Y) with
        | (0, 0) -> "You're at the origin!"
        | (12, 12) -> "You're at 12, 12!"
        | (_, _) -> "Who knows where you are?"
    System.Console.WriteLine(message)
    
    let (|Point|) (x : int, y : int) (inPt : Point) =
        inPt.X = x && inPt.Y = y
    let message =
        match newPoint with
        | Point(0, 0) true -> "You're at the origin!"
        | Point(12, 12) true -> "You're at 12,12!"
        | _ -> "Who knows where you are?"
    System.Console.WriteLine(message)



// ======================== Record types

type Author =
    | Author of string * string * int
    member a.FirstName =
        match a with
        | Author(first, _, _) -> first
    member a.LastName =
        match a with
        | Author(_, last, _) -> last
    member a.Age =
        match a with
        | Author(_, _, age) -> age

type AuthorRecord = {
    FirstName : string
    LastName : string
    Age : int }

type ProgrammingLanguage = {
    Name : string
    YearsInUse : int }
type SpokenLanguage = {
    Name : string
    YearsInUse : int }

type Person = 
    { FirstName : string
      LastName : string
      FavoriteColor : string }
    member p.FullName =
        System.String.Format("{0} {1}",
            p.FirstName, p.LastName)

[<Example("Record type examples")>]
let record_examples() =
    let ted = ("Ted", "Neward", 38)
    let aaron = ("Aaron", "Erickson")
    let rick = ("Rick", "Minerich", "I'd rather not say")
    
    let ted = Author("Ted", "Neward", 38)
    let aaron = Author("Aaron", "Erickson", 35)
    let rick = Author("Rick", "Minerich", 0)
    let talbott = Author("Crowell", "Talbott", 35)
    
    let authors = [
        Author("Ted", "Neward", 38);
        Author("Aaron", "Erickson", 35);
        Author("Rick", "Minerich", 0);
        Author("Crowell", "Talbott", 35)
    ]
    for a in authors do
        match a with
        | Author(first, last, age) -> 
            System.Console.WriteLine("Hello, {0}", first)
    
    let ted = { FirstName = "Ted"; LastName = "Neward"; Age = 38 }
    System.Console.WriteLine("Hello, {0}", ted.FirstName)
    
    let talbott = { LastName = "Crowell"; FirstName = "Talbott"; Age = 35 }
    
    let english = { Name = "English"; YearsInUse = 1000 }
    System.Console.WriteLine("english IS-A {0}", 
        english.GetType()) // SpokenLanguage
    let fsharp = { Name = "F#"; YearsInUse = 5 }
    System.Console.WriteLine("fsharp IS-A {0}", 
        fsharp.GetType()) // SpokenLanguage
    
    let english = { 
        SpokenLanguage.Name = "English"; 
        SpokenLanguage.YearsInUse = 1000 }
    System.Console.WriteLine("english IS-A {0}", 
        english.GetType()) // SpokenLanguage
    let fsharp = { 
        ProgrammingLanguage.Name = "F#"; 
        ProgrammingLanguage.YearsInUse = 5 }
    System.Console.WriteLine("fsharp IS-A {0}", 
        fsharp.GetType()) // ProgrammingLanguage
    
    let ted = { FirstName = "Ted"; LastName = "Neward"; FavoriteColor = "Black" }
    let michael = { ted with FirstName = "Michael" }
    let matthew = { ted with FirstName = "Matthew" }
    let people = [ 
        ted; michael; matthew;
        { FirstName="Aaron"; LastName="Erickson"; 
            FavoriteColor="White" }
        { FirstName="Rick"; LastName="Minerich";
            FavoriteColor="Blue" }
        { FirstName="Talbott"; LastName="Crowell";
            FavoriteColor="Red" }
    ]

    for n in people do    
        match n with
        | { Person.LastName = "Neward" } ->
            System.Console.WriteLine("Hi, {0}!", n.FirstName)
        | _ ->
            System.Console.WriteLine("Who are you, {0}?", 
                n.FullName)

    let a = { FirstName="Ted"; LastName="Neward"; Age=38 }
    let b = { FirstName="Ted"; LastName="Neward"; Age=38 }
    System.Console.WriteLine("a = b? {0}",
        if a = b then "yes" else "no")
    System.Console.WriteLine("a.Equals(b)? {0}",
        if a.Equals(b) then "yes" else "no")
    System.Console.WriteLine("a <> b? {0}",
        if a <> b then "yes" else "no")
    ()    
    