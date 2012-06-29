open System
open System.Reflection

type Person(fn : string, ln: string, a : int) =
    let mutable age = a
    member p.FirstName = fn
    member p.LastName = ln
    member p.Age with get() = age and set(v) = age <- v
    member p.WaxEloquently (length : int)=
            Console.WriteLine(
                "I speak for " + length.ToString() + "mins")
    member p.WaxEloquently ([<ParamArray>] subject : string array) =
        Array.iter
            (fun (subj : string) -> 
                Console.WriteLine("I speak on " + subj))
            subject            
    override p.ToString() =
        String.Format("[Person: {0} {1} {2}]",
            p.FirstName, p.LastName, p.Age)

type Pet(n: string, a : int) =
    let mutable age = a
    member p.Name = n
    member p.Age with get() = age and set(v) = age <- v
    override p.ToString() =
        String.Format("[Pet: {0} {1}]", p.Name, p.Age)

[<EntryPoint>]
let main(args : string array) =
    let p = new Person("Katie", "Ellison", 30)
    let pt1 = p.GetType()
    let pt2 = Type.GetType("System.Object")
    let pt3 = typeof<Person>
    pt1.GetMembers() |>
        Array.filter 
            (fun (mi : MemberInfo) -> 
                mi.MemberType = MemberTypes.Property) |>
        Array.iter 
            (fun (pi : MemberInfo) -> 
                Console.WriteLine("Found {0}", pi.Name))
    Console.WriteLine("Done")
    0

// Dynamic programming examples

(* Matt's example: accesses op[]

let inline (?) this key =
  ( ^a : (member get_Item : ^b -> ^c) (this,key))
 
let inline (?<-) this key value =
  ( ^a : (member set_Item : ^b * ^c -> ^d) (this,key,value))

*)


//open System.Reflection
//open Microsoft.FSharp.Reflection
//
//
//type dynamic(value : obj) =
//    let ty = value.GetType()
//    member d.Type = ty
//    member d.Val with get() = value
//    member d.Cast<'a>() = value :?> 'a
//
//    override d.ToString() =
//        System.String.Format("[dynamic: {0}]", d.Val.ToString())
//
//    member d.Exec(str, [<System.ParamArray>] args : obj array) =
//        dynamic (ty.InvokeMember(str, , null, value, args))
// 
//    member d.Item 
//        with get str = d.Exec(str)
//
//
//        
//    
//let (?) this mem args =
//    let flags = BindingFlags.GetProperty |||
//                BindingFlags.InvokeMethod
//    let args =
//        if box args = null then
//            [| |]
//        elif FSharpType.IsTuple (args.GetType()) then
//            FSharpValue.GetTupleFields args
//        else
//            [| args |]
//    this.GetType().InvokeMember(mem, flags, null, this, args)
//
//let (?<-) this prop newval =
//    this.GetType().InvokeMember(prop, BindingFlags.SetProperty, null, this, [|newval|])
//
//[<Example("dynamic examples")>]
//let dynamic_examples() =
//    let p = new Person("Ken", "Sipe", 40)
//    let p2 = new Pet("Fluffy", 1)
//    
////    let d = dynamic p
////    System.Console.WriteLine(d)
////    System.Console.WriteLine(d.["FirstName"])
////    let d2 = dynamic (Pet("Fluffy", 2))
////    System.Console.WriteLine(d2.Val)
////    d.Exec("WaxEloquently", "Java", "C#", "F#") |> ignore
//    
//    let o = p :> obj
//    System.Console.WriteLine("Ken is {0}", (o?Age).ToString())
//    o?WaxEloquently("Java", "C#", "F#")
//    let o2 = p2 :> obj
//    System.Console.WriteLine("Fluffy is {0}", (o2?Age).ToString())
//    o2?Age <- 2
//    System.Console.WriteLine("Fluffy is now {0}", (o2?Age).ToString())
//    
//    ()
