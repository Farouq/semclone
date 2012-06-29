#light

module PatternMatching

open ProFSharp
open HelperTypes

[<Example("Basic pattern matching")>]
let pat_example() =
    let x = 12
    match x with
    | 12 -> System.Console.WriteLine("It's 12")
    | _ -> System.Console.WriteLine("It's not 12")
    
    let y = match x with
            | 12 -> 24
            | _ -> 36
            
    
    let people = [
        ("Ted", "Neward", 38)
        ("Mark", "Richards", 45)
        ("Naomi", "Wilson", 38)
        ("Ken", "Sipe", 43)
    ]
    List.iter
        (fun (p) -> match p with
                     | (fn, ln, _) -> System.Console.WriteLine("{0} {1}", fn, ln)
                     | _ -> failwith "Unexpected value"
        )
        people

    let p = new Person("Ken", "Sipe", 45)
    let lastName = match (p.FirstName, p.LastName, p.Age) with
                    | ("Ken", "Sipe", _) -> p.LastName
                    | _ -> ""

    let persons = [
        new Person("Ted", "Neward", 38)
        new Person("Ken", "Sipe", 43)
        new Person("Michael", "Neward", 16)
        new Person("Matthew", "Neward", 9)
        new Person("Mark", "Richards", 45)
        new Person("Naomi", "Wilson", 38)
        new Person("Amanda", "Sipe", 18)
        ]
    List.iter
        (fun (p : Person) -> 
            match (p.FirstName, p.LastName) with
            | (fn, "Sipe") ->
                System.Console.WriteLine("Hello, {0}!", fn)
            | (fn, "Neward") ->
                System.Console.WriteLine("Go away, {0}!", fn)
            | _ ->
                System.Console.WriteLine("Who the heck are you?")
        )
        persons
    ()
    
[<Example("Constant pattern matching")>]
let const_pat_example() =
    let r = (new System.Random()).Next(5)
    let message = match r with
                    | 0 -> "zero"
                    | 1 -> "one"
                    | 2 -> "two"
                    | 3 -> "three"
                    | 4 -> "four"
                    | 5 -> "five"
                    | _ -> "Unknown: " + r.ToString()
    System.Console.WriteLine("We got {0}", message)
    
    let x = (new System.Random()).Next(2)
    let y = (new System.Random()).Next(2)
    let quadrant = match x, y with
                    | 0, 0 -> "(0,0)"
                    | 0, 1 -> "(0,1)"
                    | 0, 2 -> "(0,2)"
                    | 1, 0 -> "(1,0)"
                    | 1, 1 -> "(1,1)"
                    | 1, 2 -> "(1,2)"
                    | 2, 0 -> "(2,0)"
                    | 2, 1 -> "(2,1)"
                    | 2, 2 -> "(2,2)"
    System.Console.WriteLine("We got {0}", message)

[<Example("Named pattern example")>]
let named_pat_example() =
    let p = new Person("Rachel", "Reese", 25)
    let message = match p.FirstName with
                    | fn -> "Hello, " + fn
    System.Console.WriteLine("We got {0}", message)

    let p = new Person("Rachel", "Appel", 25)
    let message = match p.FirstName with
                    | "Rachel" -> "It's one of the Rachii!!"
                    | fn -> "Alas, you are not a Rachii, " + fn
    System.Console.WriteLine("We got {0}", message)

[<Example("AND/OR pattern example")>]
let andor_pat_example() =
    let p = new Person("Rachel", "Reese", 25)
    let message = match p.FirstName with
                    | ("Rachel" | "Scott") -> "Hello, " + p.FirstName
                    | _ -> "Who are you, again?"
    System.Console.WriteLine("We got {0}", message)


[<Literal>]
let rachel = "Rachel"

[<Example("Literals pattern example")>]
let literal_pat_example() =
    let p = new Person("Rachel", "Reese", 25)
    let message = match p.FirstName with
                    | rachel -> "Howdy, Rachel!"
                    | _ -> "Howdy, whoever you are!"
    System.Console.WriteLine("We got {0}", message)

[<Example("Tuple pattern maching example")>]
let tuple_pat_example() =
    let p = ("Aaron", "Erickson", 35)
    let message = match p with
                    | (first, last, age) -> 
                        "Howdy " + first + " " + last +
                        ", I'm glad to hear you're " + age.ToString() + "!"
    System.Console.WriteLine("We got {0}", message)

[<Example("List pattern matching example")>]
let list_pat_example() =
    let numbers = [1; 2; 3; 4; 5]
    let rec sumList ns = match ns with
                            | [] -> 0
                            | head :: tail -> head + sumList tail
    let sum = sumList numbers
    System.Console.WriteLine("Sum of numbers = {0}", sum)
    
    let message = match numbers with
                    | [] -> "List is empty!"
                    | [one] -> 
                        "List has one item: " + one.ToString()
                    | [one; two] ->
                        "List has two items: " + 
                        one.ToString() + " " + two.ToString()
                    | _ -> "List has more than two items"
    System.Console.WriteLine("We got {0}", message)
    
[<Example("Array pattern matching example")>]
let array_pat_example() =
    let numbers = [|1; 2; 3; 4; 5|]
    let message = match numbers with
                    | [| |] -> "Array is empty!"
                    | [| one |] ->
                        "Array has one item: " + one.ToString()
                    | [| one; two |] ->
                        "Array has two items: " +
                        one.ToString() + " " + two.ToString()
                    | _ -> "Array has more than two items"
    System.Console.WriteLine("We got {0}", message)

[<Example("As-pattern pattern maching example")>]    
let as_pat_example() =
    let t1 = (1, 2)
    let message = match t1 with
                    | (x,y) as t2 ->
                        x.ToString() + " " +
                        y.ToString() + " " +
                        t2.ToString()
    System.Console.WriteLine("We got {0}", message)

(*    
let pat_example90() =
    let rec buildMessage (l : obj list) =
        match l with
            | [(n : string) :: t] -> 
                System.String.Format("Greetings {0}", n) + buildMessage t
            | [(a : int) :: t] -> 
                System.String.Format("You're {0}", a) + buildMessage t
            | _ -> "."
    let message = buildMessage ["Fred"; 38]
    System.Console.WriteLine(message)
*)    

[<Example("When pattern matching example")>]
let when_pat_example() =
    let p = new Person("Rick", "Minerich", 35)
    let message = match (p.FirstName) with
                    | _ when p.Age > 30 ->
                        "Never found"
                    | "Minerich" when p.FirstName <> "Rick" ->
                        "Also never found"
                    | "Minerich" ->
                        "Hiya, Rick!"
                    | _ ->
                        "Who are you?"
    System.Console.WriteLine("We got {0}", message)
    
    let isOldFogey (person : Person) =
        match person with
        | _ when person.Age > 35 || 
            (person.FirstName = "Ted" && 
                person.LastName = "Neward") ||
            (person.FirstName = "Aaron" && 
                person.LastName = "Erickson") ->
            true
        | _ -> false
    System.Console.WriteLine("{0} is an old fogey: {1}", p, isOldFogey p)
            
    let isOldFogey' (person : Person) =
        let isOld (p : Person) = 
            p.Age > 35
        let isTed (p : Person) = 
            p.FirstName = "Ted" && p.LastName = "Neward"
        let isAaron (p : Person) = 
            p.FirstName = "Aaron" && p.LastName = "Erickson"
        match p with
            | _ when isOld p || isTed p || isAaron p -> 
                true
            | _ -> 
                false
    System.Console.WriteLine("{0} is an old fogey: {1}",
        p, isOldFogey' p)
        
    let isOldFogey'' (p : Person) =
        match p.Age, p.FirstName, p.LastName with
        | _, "Ted", "Neward" -> true
        | _, "Aaron", "Erickson" -> true
        | a, _, _ when a > 35 -> true
        | _ -> false
    System.Console.WriteLine("{0} is an old fogey: {1}",
        p, isOldFogey'' p)
    
open System.IO

[<Example("Active pattern matching example")>]
let active_simple_pat_example() =
    let inputData = "This is some <script>alert()</script> data"
    let contains (srchStr : string) (inStr : string) =
        inStr.Contains srchStr
    let goodInput inStr =
        contains "<script>" inStr ||
        contains "<object>" inStr ||
        contains "<embed>" inStr ||
        contains "<applet>" inStr
    System.Console.WriteLine("Does the text contain bad data? " +
        (goodInput inputData).ToString())
        
    let (|Contains|) srchStr (inStr : string) =
        inStr.Contains srchStr
    match inputData with
    | Contains "<script>" true -> false
    | Contains "<object>" true -> false
    | Contains "<embed>" true -> false
    | Contains "<applet>" true -> false
    | _ -> true

[<Example("Partial-case active pattern example")>]
let active_partial_pat_example() =
    let inputData = "This is some <script>alert()</script> data"

    let (|Contains|_|) pat inStr =
        let results = 
            (System.Text.RegularExpressions.Regex.Matches(inStr, pat))
        if results.Count > 0 
            then Some [ for m in results -> m.Value ] 
            else None

    match inputData with
    | Contains "http://S+" _ -> "Contains urls"
    | Contains "[^@]@[^.]+\.\W+" _ -> "Contains emails"
    | Contains "\<script\>" _ -> "Found <script>"
    | Contains "\<object\>" _ -> "Found <object>"
    | _ -> "Didn't find what we were looking for"

open System.Reflection
[<Example("Partial-case Reflection active pattern example")>]
let active_reflect_pat_example() =
    let AllBindingFlags = 
        BindingFlags.NonPublic ||| BindingFlags.Public |||
        BindingFlags.Instance ||| BindingFlags.Static
    let (|Field|_|) name (t : System.Type) =
        let fi = t.GetField(name, AllBindingFlags) 
        if fi <> null then Some(fi) else None
    let (|Method|_|) name (t : System.Type) =
        let fi = t.GetMethod(name, AllBindingFlags) 
        if fi <> null then Some(fi) else None
    let (|Property|_|) name (t : System.Type) =
        let fi = t.GetProperty(name, AllBindingFlags) 
        if fi <> null then Some(fi) else None
    let pt = typeof<Person>
    let message = 
        match pt with
        | Property "FirstName" pi -> 
            "Found property " + pi.ToString()
        | Property "LastName" pi -> 
            "Found property " + pi.ToString()
        | _ -> "There's other stuff, but who cares?"
    System.Console.WriteLine(message)

[<Example("Partial-case Reflection duck typing active pattern")>]
let activepat_ducktyping_example() =
    let AllBindingFlags = 
        BindingFlags.NonPublic ||| BindingFlags.Public |||
        BindingFlags.Instance ||| BindingFlags.Static
    let (|Field|_|) name (inst : obj) =
        let fi = inst.GetType().GetField(name, AllBindingFlags)
        if fi <> null 
        then Some(fi.GetValue(inst)) 
        else None
    let (|Method|_|) name (inst : obj) =
        let fi = inst.GetType().GetMethod(name, AllBindingFlags)
        if fi <> null 
        then Some(fi) 
        else None
    let (|Property|_|) name (inst : obj) =
        let fi = inst.GetType().GetProperty(name, AllBindingFlags)
        if fi <> null 
        then Some(fi.GetValue(inst, null)) 
        else None

    let rm = new Person("Rick", "Minerich", 29)
    // Does it have a first name? Get the value if it does
    let message = match rm with
                    | Property "FirstName" value ->
                        "FirstName = " + value.ToString()
                    | _ -> "No FirstName to be found"
    System.Console.WriteLine(message)

    let rm = new Person("Rick", "Minerich", 29)
    // Does it have a first name AND a last name?
    let message = match rm with
                    | Property "FirstName" fnval &
                      Property "LastName" lnval ->
                        "Full name = " + fnval.ToString() +
                        " " + lnval.ToString()
                    | Property "FirstName" value ->
                        "Name = " + value.ToString()
                    | Property "LastName" value ->
                        "Name = " + value.ToString()
                    | _ -> 
                        "No name to be found"
    System.Console.WriteLine(message)

[<Example("Multi-case active pattern example")>]
let active_pat_multicase_example() =
    let (|Property|Method|Field|Constructor|) (mi : MemberInfo) =
        if (mi :? FieldInfo) then 
            Field(mi.Name, (mi :?> FieldInfo).FieldType)
        elif (mi :? MethodInfo) then
            let mthi = (mi :?> MethodInfo)
            Method(mi.Name, mthi.ReturnType, mthi.GetParameters())
        elif (mi :? PropertyInfo) then
            let pi = (mi :?> PropertyInfo)
            Property(pi.Name, pi.PropertyType)
        elif (mi :? ConstructorInfo) then
            let ci = (mi :?> ConstructorInfo)
            Constructor(ci.GetParameters())
        else
            failwith "Unrecognized Reflection type"
    let pt = typeof<Person>
    let AllBindingFlags = 
        BindingFlags.NonPublic ||| BindingFlags.Public |||
        BindingFlags.Instance ||| BindingFlags.Static
    for p in pt.GetMembers(AllBindingFlags) do
        match p with
        | Property(nm, ty) -> 
            System.Console.WriteLine(
                "Found prop {1} {0}", nm, ty)
        | Field(nm, ty) ->
            System.Console.WriteLine(
                "Found fld {1} {0}", nm, ty)
        | Method(nm, rt, parms) ->
            System.Console.WriteLine(
                "Found mth {1} {0}(...)", nm, rt)
        | Constructor(parms) ->
            System.Console.WriteLine("Found ctor")

let active_pat_example() =
    let (|FileFullName|) (f:FileInfo) = f.FullName
    let (|FileName|) (f:FileInfo) = f.Name
    let (|FileExt|) (f:FileInfo) = f.Extension
    let (|FileTuple|) (f:FileInfo) = (f.FullName, f.Name, f.Extension)
    let cmd = new FileInfo(@"C:\Windows\System32\cmd.exe")
    match cmd with
//    | FileTuple(fn, n, ".exe") -> "Found " + fn.ToString()
    | FileFullName(fn) & FileName("cmd") & FileExt(".exe") ->
        "Found " + fn.ToString()
    | _ -> "No idea"
