module Inheritance

open ProFSharp

// ======================== Basics

[<Interface>]
type IDrinker =
    abstract Drink : unit -> unit
    abstract FavoriteDrink : string
    
[<Interface>]
type IEater =
    abstract Eat : unit -> unit
    abstract FavoriteFood : string

[<Interface>]
type IGlutton =
    inherit IDrinker
    inherit IEater
    abstract EatAndDrink : unit -> unit

[<AbstractClass>]
type Person(fn : string, ln : string, a : int) =
    inherit System.Object()
    override this.GetHashCode() =
        hash (fn, ln, a)
    override this.Equals(other) =
        compare this (other :?> Person) = 0
    interface System.IFormattable with
        member this.ToString(s : string, fp : System.IFormatProvider) : string =
            "Not implemented yet"
    interface System.ICloneable with
        member this.Clone() : obj =
            this.DoTheCloneThing()
    abstract DoTheCloneThing : unit -> obj
    member p.FirstName = fn
    member p.LastName = ln
    member p.Age = a
    interface System.IComparable with
        member this.CompareTo(other) =
            let other = other :?> Person
            let ln = this.LastName.CompareTo(other.LastName)
            if ln <> 0 then
                let fn = this.FirstName.CompareTo(other.FirstName)
                if fn <> 0 then
                    this.Age.CompareTo(other.Age)
                else
                    fn
            else
                ln
    member p.Greet(other : Person) =
        System.Console.WriteLine("Howdy, {0}, from {1}!",
            other.FirstName, p.FirstName)
    override p.ToString() =
        let typename = base.ToString()
        System.String.Format("[{3}: {0} {1} {2}]",
            fn, ln, a, "")
    abstract Work : unit -> unit
    default p.Work() =
        System.Console.WriteLine("Working!")
    abstract Salary : int32 with get, set
    default p.Salary
        with get() = 0
    default p.Salary 
        with set(v : int32) = System.Console.WriteLine(v)
    static member op_Equality (l, r) = (compare l r) = 0
    static member op_LessThan (l, r) = (compare l r) < 0
    static member op_GreaterThan (l, r) = (compare l r) > 0

[<Class>]
type Student(fn, ln, a, sub, sch) =
    inherit Person(fn, ln, a)
    let gpa = 0.0
    do System.Console.WriteLine("Whoo-hoo! College!")
    new() = Student("", "", 0, "", "")
    new(fn, ln, a) = Student(fn, ln, a, "", "")
    interface IDrinker with
        member this.Drink() =
            System.Console.WriteLine("Chug! Chug! Chug!")
        member this.FavoriteDrink = "Keystone Light"
    member s.FormalName = s.FirstName + " " + " of " + sch
    member s.Subject = sub
    member s.School = sch
    override s.Work() =
        System.Console.WriteLine("Studying!")
    override s.Salary
        with set(v) = System.Console.WriteLine(v)
    override s.DoTheCloneThing() =
        new Student(fn, ln, a, sub, sch) :> obj


[<Class>]
type Person2(fn, ln, a) =
    let firstName = fn
    let lastName = ln
    let age = a
    new() = Person2("", "", 0)
    member p.FirstName = firstName
    member p.LastName = lastName
    member p.Age = age

[<Class>]
type Student2 =
    inherit Person2
    val subject : string
    val school : string
    new(fn, ln, a, subj, sch) = 
        { inherit Person2(fn, ln, a); subject = subj; school = sch}
    new() =
        { inherit Person2(); subject = ""; school = "" }
    member s.Subject = s.subject
    member s.School = s.school

[<Example("Inheritance basics")>]
let inheritance_examples() =
    let s = new Student("Ted", "Pattison", 50, "Beer", "DevelopMentor")
    System.Console.WriteLine("{0} {1} attends {2} and is studying {3}",
        s.FirstName, s.LastName, s.School, s.Subject)

    let p = new Student("Ted", "Neward", 38)
    p.Greet(s)
    ()

type Printer() =
    member this.PrintName(p : #Person) =
        System.Console.WriteLine("{0}", p.FirstName)
    member this.GenericPrintName(p : 'a when 'a :> Person) =
        System.Console.WriteLine("{0}", p.FirstName)
    
[<Example("Overriding examples")>]    
let overriding_examples() =
    let p = new Student("Ken", "Sipe", 40)
    let p_str = p.ToString()
    System.Console.WriteLine(p_str)
        // prints "Inheritance+Person"
    ()

[<Example("Casting examples")>]
let casting_examples() =
    // This will NOT compile
    //let p : Person = new Student("Ken", "Sipe", 40)
    
    let p : Person = new Student("Ken", "Sipe", 40) :> Person
    let s = p :?> Student
    let pToStudent = p :? Student

    let printer = new Printer()
    printer.PrintName(p)
    printer.PrintName(s)
    
    let p2 : Person = upcast new Student("Ken", "Sipe", 40)
    let s2 : Student = downcast p2
    
    let oi = box 42
    System.Console.WriteLine("oi's type is {0}", oi.GetType())
    let i : int32 = unbox oi
    System.Console.WriteLine("i's type is {0}", i.GetType())

    ()

[<Example("Equality and comparison examples")>]
let eqcom_examples() =
    let t1 = (1, 1)
    let t2 = (1, 1)
    let t3 = (1, 2)
    System.Console.WriteLine("{0}", (t1 = t2)) // true
    System.Console.WriteLine("{0}", (t1 < t3)) // true
    System.Console.WriteLine("{0}", (t3 < t1)) // false
    ()

[<Example("Interface examples")>]
let interface_examples() =
    let p = new Student("Rachel", "Reese", 28, "Silverlight", "Agilitrain")
    let pclone = (p :> System.ICloneable).Clone()

    ()

[<Example("Object expression examples")>]
let obj_expr_examples() =
    let p = { new IDrinker with
                member this.Drink() =
                    System.Console.WriteLine("Sip")
                member this.FavoriteDrink =
                    "Macallan 25" }
    p.Drink()
    
    let p2 = { new Person("Ted", "Neward", 38) with
                member this.DoTheCloneThing() = null
                member this.Work() =
                    System.Console.WriteLine("Writing a book!") }
    p2.Work()

    ()