module Class

open ProFSharp

// ======================== Class
[<Class>]
type Person(fn : string, ln : string, a : int) = 
    let mutable age = a
    let fullName = fn + " " + ln
    let constructionDate = System.DateTime.Now
    let leapYearBaby =
        if constructionDate.Month = 2 &&
           constructionDate.Day = 29 
        then true
        else false
    do
        if constructionDate = System.DateTime.Now then age <- age + 1
    new () as this = Person("", "",0) then
        System.Console.WriteLine(this)
    new (fn,ln) as this = Person(fn, ln, 0) then
        System.Console.WriteLine(this)
    member p.IsPerson = true
    member p.FirstName = fn
    member p.LastName = ln
    member p.Age
        with get() = age
        and set(newAge) = 
            match newAge with
            | newAge when newAge > 0 ->
                age <- newAge
            | _ ->
                failwith "Age cannot be 0 or less"
    member p.FullName with get() = fullName
    member p.NameAndAge 
        with get() = 
            System.String.Format("{0} ({1} years old)",
                p.FullName, p.Age)
    member p.AgeGracefully() =
        System.Console.WriteLine("I feel wiser!")
        p.Age <- p.Age + 1
    member p.Item 
        with get(organ) = 
            match organ with
            | "Heart" -> "Ba-dump"
            | "Stomach" -> "Growl"
            | "Mouth" -> "Chomp chomp swallow"
            | "Brain" -> "Crackle crackle"
            | _ -> ""
    member p.Organ
        with get(name) =
            match name with
            | "Heart" -> "Ba-dump"
            | "Stomach" -> "Growl"
            | "Mouth" -> "Chomp chomp swallow"
            | "Brain" -> "Crackle crackle"
            | _ -> ""
    member p.Organ
        with get(id) =
            match id with
            | 1 -> "Ba-dump"
            | 2 -> "Growl"
            | 3 -> "Chomp chomp swallow"
            | 4 -> "Crackle crackle"
            | _ -> ""
    member p.Greet(otherPerson : Person, message) =
        System.Console.WriteLine("{0} says {1} to {2}",
            p.FullName, message, otherPerson.FullName)
    member p.Greet(otherPerson : Person) =
        System.Console.WriteLine("{0} says 'Howdy!' to {1}",
            p.FullName, otherPerson.FullName)
    member p.CurriedGreet target message =
        System.Console.WriteLine("{0} says {1} to {2}",
            p.FullName, message, target)
    member p.CreateGreeting(otherPerson : Person, message) =
        System.String.Format("{0} says {1} to {2}",
            p.FullName, message, otherPerson.FullName)
    member p.WhoWhatWhereWhenWhy(what, 
                                 where, 
                                 whenn : System.DateTime, 
                                 why) =
        System.String.Format("{0} is doing {1} at {2} on {3} " +
            "because {4}",
            p.FullName, what, where, whenn, why)
    member p.Alibi(?what : string,
                   ?where : string,
                   ?whenn : System.DateTime,
                   ?why : string) =
        match (what, where, whenn, why) with
        | (Some(wht), Some(whr), Some(whn), Some(why)) ->
            System.String.Format("{0} did {1} {2} because {3}",
                p.FullName, wht, whr, why)
        | (None, None, None, None) ->
            System.String.Format("{0} has no alibi at all",
                p.FullName)
        | (_, _, _, _) ->
            System.String.Format("{0} has no alibi at all",
                p.FullName)
    member p.AnotherAlibi(?what : string, ?where : string,
                          ?whenn : System.DateTime,
                          ?why : string) =
        let defaultArg x y = match x with None -> y | Some(v) -> v
        let wht = defaultArg what "nothing"
        let whr = defaultArg where "noplace"
        let whn = defaultArg whenn System.DateTime.Now
        let why = defaultArg why "of no reason"
        System.String.Format("{0} did '{1}' '{2}' because '{3}'",
            p.FullName, wht, whr, why)
    static member (<==>) (lhs : Person, rhs: Person) =
        match (lhs.FullName.CompareTo(rhs.FullName)) with
        | x when x > 0 || x < 0 -> x
        | _ -> lhs.Age.CompareTo(rhs.Age)
            

[<Class>]
type FlexiPerson(fn, ln, a) =
    let mutable firstName = fn
    let mutable lastName = ln 
    let mutable age = a
    new() = FlexiPerson("", "", 0)
    member fp.FirstName 
        with get() = firstName and set(n) = firstName <- n
    member fp.LastName 
        with get() = lastName and set(n) = lastName <- n
    member fp.Age 
        with get() = age and set(n) = age <- n


[<Class>]
type OptiPerson(?firstName, ?lastName, ?age) =
    member fp.FirstName 
        with get() = firstName
    member fp.LastName 
        with get() = lastName
    member fp.Age 
        with get() = age


[<Class>]
type Skynet() =
    [<DefaultValue>]
    static val mutable private terminatorsBuilt : int64
    static member CreateTerminator() = 
        Skynet.terminatorsBuilt <- Skynet.terminatorsBuilt+1L
        new Person("T", "800", 0)
    static member AfterJudgmentDay 
        with get() = 
            let jd = new System.DateTime(1997, 8, 29)
            System.DateTime.Now.ToBinary() > jd.ToBinary()
    static let humansKilled = 3000000000L



[<Class>]
type Complex(r : int32, i : int32) =
    member c.R = r
    member c.I = i
    static member (+) (c1 : Complex, c2 : Complex) =
        new Complex(c1.R + c2.R, c1.I + c2.I)
    static member (-) (c1 : Complex, c2 : Complex) =
        new Complex(c1.R - c2.R, c1.I - c2.I)
    static member (~-) (c : Complex) =
        new Complex(-(c.R), c.I)

[<Class>]
type PrivatePerson private(fn, ln, a) =
    private new() = PrivatePerson("", "", 0)
    static member Create(fn, ln, a) = new PrivatePerson(fn, ln, a)
    static member Create() = new PrivatePerson()



type Location =
    | Headgear
    | Footwear
    | Armor
    | OneHanded
    | TwoHanded
    | None

[<Class>]
type Item(name : string, loc : Location, 
                  bonus : int32, GPValue : int32) =
    member mi.Name = name
    member mi.Bonus = bonus
    member mi.Location = loc
    override mi.ToString() =
        System.String.Format("{0} ({1})", mi.Name, mi.Bonus)
    
[<Class>]
type Munchkin(level : int32, items : Item list) =
    let mutable armor : Item = 
        new Item("Clothes of Ineptitude", Armor, 0, 0)
    let mutable headgear : Item =
        new Item("Hair", Headgear, 0, 0)
    let mutable footwear : Item =
        new Item("Bare Feet", Footwear, 0, 0)
    let mutable miscItems : Item list = []
    do
        List.iter
            (fun (it : Item) ->
                match it.Location with
                | Armor -> armor <- it
                | Headgear -> headgear <- it
                | Footwear -> footwear <- it
                | None -> miscItems <- it :: miscItems
                | _ -> failwith "E_NOTIMPL"
            )
            items
    new() = Munchkin(1, [])
    new(level : int32) = Munchkin(level, [])
    member m.Level
        with get() = level
    member m.Items
        with get() = [ armor; headgear; footwear ] @ miscItems
    member m.TotalBonus =
        level + (List.sumBy (fun (it : Item) -> it.Bonus) m.Items)
    static member (<<==) (m : Munchkin, mi : Item) =
        new Munchkin( m.Level, mi :: m.Items )
    static member (~+) (m: Munchkin) =
        new Munchkin( m.Level + 1, m.Items)


[<Example("Basic class usage")>]
let class_usage() =
    let p1 = new Person("Ted", "Neward", 38)
    let p2 = new Person("Aaron", "Erickson")
    let p3 = new Person()
    for p in [p1; p2; p3] do
        System.Console.WriteLine("{0} is {1} years old",
            p.FullName, p.Age)
    p1.Age <- p1.Age+1
    let p4 = new Person("Ted", "Neward", Age = 38)
    
    let p5 = new FlexiPerson()
    let p6 = new FlexiPerson(FirstName="Ted")
    let p7 = new FlexiPerson(LastName="Neward", Age=38)
    let op5 = new OptiPerson()
    let op6 = new OptiPerson(firstName="Ted")
    let op7 = new OptiPerson(firstName="Neward", age=38)
    
    System.Console.WriteLine("{0}'s heart says {1}",
        p1.FullName, p1.["Heart"])
    
    System.Console.WriteLine("{0}'s heart says {1}",
        p1.FullName, p1.Organ("Heart"))
    
    p1.Greet(p2, "Howdy!")
    let greeting = p1.CreateGreeting(p2, "Howdy!")
    System.Console.WriteLine(greeting)
    p1.Greet(p2)
    
    let wwwww = 
        p1.WhoWhatWhereWhenWhy(
            whenn=System.DateTime.Now,
            where="in the sitting room",
            what="relaxing",
            why="because I'm tired")
    System.Console.WriteLine(wwwww)
    
    System.Console.WriteLine(arg=[||], format="This is a message")
    
    let alibi1 = p1.Alibi("relaxing", "in the sitting room",
                    System.DateTime.Now, "because I'm tired")
    let alibi2 = p1.Alibi("relaxing", "in the sitting room",
                    why="because I'm tired",
                    whenn=System.DateTime.Now)
    let alibi3 = p1.Alibi("relaxing", "in the sitting room",
                    ?why=Some("because I'm tired"),
                    whenn=System.DateTime.Now)
    
    let T800 = 
        if Skynet.AfterJudgmentDay then
            Skynet.CreateTerminator()
        else
            new Person("Arnold", "Schwarzenegger", 50)
            
    let ted = new Munchkin(1)
    let coolArmor = new Item("Functional Plate", Armor, 5, 0)
    let ted = ted <<== coolArmor
    
    let compare = p1 <==> p2

    ()


[<Class>]
type Student(name : string, subject : string) =
    member s.Name = name
    member s.Subject = subject

type Student with
    new() = Student("", "")
    new(name, subject, school) = 
        Student(name, subject)
    member s.FullDescription = s.Name + " " + s.Subject

type System.String with
    member s.IsUpper =
        s.ToUpper() = s


[<Example("Type extension usage")>]
let typeExtensionExample() =
    let ted = 
        new Student("Ted", "International Relations")

    ()


// ======================== Access Modifiers

[<Class>]
type private Sport(name) =
    member private p.Rules
        with get() = ""

[<Class>]
type (* public *) ExampleClass(field1 : string) =
    [<DefaultValue>]
    val mutable (* private *) valField : string

    // Always private
    let mutable mutField2 = "Changeable"
    let helper = field1 + ", helped"

    (* public *)
    new () =
        ExampleClass("")

    member (* public *) e.Property 
        with (* public *) get() = field1
    member (* public *) e.ReadWriteProp
        with (* public *) get() = mutField2
        and (* public *) set(value) = mutField2 <- value


// ======================== Delegates and Events

type Watcher() =
    static member GoingAway(args : System.EventArgs) =
        System.Console.WriteLine("Going away now....")

type Notify = delegate of string -> string

type Child() =
    member this.Respond(msg : string) =
        System.Console.WriteLine("You want me to {0}? No!")
        "No!"

type CurriedDelegate = delegate of int * int -> int
type TupledDelegate = delegate of (int * int) -> int
type DelegateTarget() =
    member this.CurriedAdd (x : int) (y : int) = x + y
    member this.TupledAdd (x : int, y : int) = x + y

type ConcertEventArgs(city : string) =
    inherit System.EventArgs()
    member cea.City = city
    override cea.ToString() =
        System.String.Format("city:{0}", city)
    
type RockBand(name : string) =
    let concertEvent = new DelegateEvent<System.EventHandler>()

    member rb.Name = name

    [<CLIEvent>]
    member rb.OnConcert = concertEvent.Publish
    member rb.HoldConcert(city : string) =
        concertEvent.Trigger([| rb; 
            new ConcertEventArgs(city) |])
        System.Console.WriteLine("Rockin' {0}!")

type Fan(home : string, favBand : RockBand) as f =
    do
        favBand.OnConcert.AddHandler(
            System.EventHandler(f.FavoriteBandComingToTown))
    member f.FavoriteBandComingToTown 
            (_ : obj) 
            (args : System.EventArgs) =
        let cea = args :?> ConcertEventArgs
        if home = cea.City then
            System.Console.WriteLine("I'm SO going!")
        else
            System.Console.WriteLine("Darn")

[<Example("")>]
let events_examples() =
    let ad = System.AppDomain.CurrentDomain
    ad.ProcessExit.Add(Watcher.GoingAway)

    let c = new Child()
    let np = new Notify(c.Respond)
    let response = np.Invoke("Clean your room!")
    System.Console.WriteLine(response)
    
    let dt = new DelegateTarget()
    let cd1 = new CurriedDelegate(dt.CurriedAdd)
    //let cd2 = new CurriedDelegate(dt.TupledAdd) // will not compile
    let td1 = new TupledDelegate(dt.TupledAdd)
    //let td2 = new TupledDelegate(dt.CurriedAdd) // will not compile
    
    let rb = new RockBand("The Functional Ya-Yas")
    rb.OnConcert 
        |> Event.filter 
            (fun evArgs ->
                let cea = evArgs :?> ConcertEventArgs
                if cea.City = "Sacramento" then false
                    // Nobody wants to tour in Sacramento
                else true)
        |> Event.add
            (fun evArgs ->
                let cea = evArgs :?> ConcertEventArgs
                System.Console.WriteLine("{0} is rockin' {1}",
                    rb.Name, cea.City))
    let f1 = new Fan("Detroit", rb)
    let f2 = new Fan("Cleveland", rb)
    let f3 = new Fan("Detroit", rb)
    rb.HoldConcert("Detroit")

    ()





// ======================== Experiments; not for publication

type Experiment() =
    member e.TestMethod( [<System.ParamArray>] args : obj array) =
        for o in args do
            System.Console.WriteLine(o.ToString())

let e = new Experiment()
e.TestMethod("one", 2, 3.0)
e.TestMethod("This is just one argument")
e.TestMethod() // No arguments, empty array

let varargsFunction([<System.ParamArray>] args : obj array) =
    for o in args do
        System.Console.WriteLine(o.ToString())

let private privateFunction() =
    printfn "You can't call me!"
        
//varargsFunction("one", 2, 3.0)
// error: This expression was expected to have type obj array
// but here has type 'a * 'b * 'c

let f x = x + x

let inline fi x = x + x

let r = fi "Hello"
