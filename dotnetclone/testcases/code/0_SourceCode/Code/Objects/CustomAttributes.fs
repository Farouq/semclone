module CustomAttributes

open ProFSharp

open System
open System.Diagnostics
open System.Reflection


// ============= Basic syntax

[<Serializable>]
/// The Person
type Person(FirstName : string, LastName : string, Age : int) =
    override p.ToString() =
        String.Format("[Person: {0} {1} {2}",
            FirstName, LastName, Age)


// ============= Obsolete

type ObsoleteExperiment() =
    [<Obsolete>]
    member e.TestMethod() =
        System.Console.WriteLine("Don't use this!")
    [<Obsolete("This method really just sucks")>]
    member e.AnotherMethod() =
        null.ToString()

[<Example("Obsolete usage")>]
let obsoleteUsage() =
    let e = new ObsoleteExperiment()
    e.TestMethod()


// ============= Conditional
type ConditionalDemo(data : string, count : int) =
    [<Conditional("DEBUG")>]
    member c.DumpInternals() =
        Console.WriteLine("data: {0}, count: {1}",
            data, count)
    override c.ToString() =
        String.Format("ConditionalDemo()")

[<Example("Conditional usage")>]
let conditionalUsage() =
    let cd = new ConditionalDemo("password", 5)
    Console.WriteLine(cd.ToString())
    cd.DumpInternals()
    

// ============= ParamArray

type ParamArrayExperiment() =
    member e.TestMethod( [<System.ParamArray>] args : obj array) =
        for o in args do
            System.Console.WriteLine(o.ToString())

let varargsFunction([<System.ParamArray>] args : obj array) =
    for o in args do
        System.Console.WriteLine(o.ToString())

[<Example("ParamArray usage")>]
let paramArrayUsage() =
    let e = new ParamArrayExperiment()
    e.TestMethod("one", 2, 3.0)
    e.TestMethod("This is just one argument")
    e.TestMethod() // No arguments, empty array

    //varargsFunction("one", 2, 3.0)
    // error: This expression was expected to have type obj array
    // but here has type 'a * 'b * 'c

    ()        


// ============= Blame example
[<AttributeUsage(AttributeTargets.Assembly |||
                 AttributeTargets.Class |||
                 AttributeTargets.Constructor |||
                 AttributeTargets.Enum |||
                 AttributeTargets.Field |||
                 AttributeTargets.Interface |||
                 AttributeTargets.Method |||
                 AttributeTargets.Module |||
                 AttributeTargets.Struct)>]
type BlameAttribute(owner : string) =
    inherit Attribute()
    
    let mutable reason = ""
    
    member public b.Owner 
        with get() = owner
    member public b.Reason
        with get() = reason
        and set(value) = reason <- value
    
    override b.ToString() =
        String.Format("Blame {0}{1}",
            b.Owner, 
            if b.Reason = "" 
                then ", just because!" 
                else ", because " + b.Reason)

[<Blame("Aaron Erickson", Reason="I told you not to use this!")>]
let faultyMethod() =
    null.ToString()

[<Example("Custom attribute creation and consumption example")>]
let blameExample() =
    try
        faultyMethod() |> ignore
    with
    | ex -> 
        let target = ex.TargetSite
        let custAttrs = 
            target.GetCustomAttributes(typeof<BlameAttribute>, true)
        if custAttrs.Length > 0 then
            let blame = (custAttrs.[0]) :?> BlameAttribute
            Console.WriteLine("Aha! {0} did it!", blame.Owner)
        else
            Console.WriteLine("Nobody to blame, sorry!")
    ()


// ============= Attributes

[<assembly:AssemblyVersion("1.0.0.0")>]
[<assembly:AssemblyCopyright("(c) 2010 Neward & Associates")>]
do
    ()


