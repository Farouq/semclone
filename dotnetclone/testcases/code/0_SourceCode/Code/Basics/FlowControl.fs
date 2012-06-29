#light

module FlowControl

open ProFSharp

[<Example("If examples")>]
let if_examples() =
    let x = 12
    if x = 12 then
        System.Console.WriteLine("Yes, x is 12")
        
[<Example("If-else examples")>]
let if_else_examples() =
    let x = 12
    if x = 12 then
        System.Console.WriteLine("Yes, x is 12")
    else
        System.Console.WriteLine("Nope, it's not 12")

[<Example("If-else-expression examples")>]
let if_else_expression() =
    let x = 12
    let msg = if x = 12 then "Yes, x is 12" else "Nope, it's not 12"
    System.Console.WriteLine(msg)
    
[<Example("If-elif examples")>]
let if_elif_expression() =
    let x = 12
    if x = 12 then
        System.Console.WriteLine("Yes, x is 12")
    elif x = 24 then
        System.Console.WriteLine("Well, now x is 24")
    else
        System.Console.WriteLine("I have no clue what x is")

//let if_else_badexpression =
//    let x = 12
//    let msg = if x = 12 then "Yes" else false
//    System.Console.WriteLine(msg)

//let if_int_example =
//    let x = 12
//    if x then
//        System.Console.WriteLine("Yep, x")
        
[<Example("While and for examples")>]
let while_and_for_example() =
    // Uncomment the while loop if it's close to the top of the hour,
    // or else be prepared to wait for a while. ;-)
    (*
    while (System.DateTime.Now.Minute <> 0) do
        System.Console.WriteLine("Not yet the top of the hour...")
    *)

    for i = 1 to System.DateTime.Now.Hour do
        System.Console.Write("Cuckoo! ")

    System.Console.WriteLine()
    
[<Example("For downto examples")>]
let for_downto_example() =
    for i = 10 downto 1 do
        System.Console.WriteLine("i = {0}", i)

[<Example("Exception-handling examples")>]
let exception_handling_example() =
    let results = 
        try
            let req = System.Net.WebRequest.Create(
                        "Not a legitimate URL")
            let resp = req.GetResponse()
            let stream = resp.GetResponseStream()
            let reader = new System.IO.StreamReader(stream)
            let html = reader.ReadToEnd()
            html
        with
            | :? System.UriFormatException -> 
                "You gave a bad URL"
            | :? System.Net.WebException as webEx -> 
                "Some other exception: " + webEx.Message
            | ex -> "We got an exception: " + ex.Message
    System.Console.WriteLine(results)

[<Example("Exception-finally examples")>]
let exception_handling_example2() =
    // This will throw an exception, since finally doesn't handle the
    // exception itself--thus the yellow stack trace in the console
    // is expected
    let results =
        try
            (12 / 0) 
        finally
            System.Console.WriteLine("In finally block")
    ()

[<Example("Exception-raising examples")>]
let exception_handling_example3() =
    // This will throw an exception, since finally doesn't handle the
    // exception itself--thus the yellow stack trace in the console
    // is expected
    try
        raise (new System.Exception("I don't wanna!"))
    finally
        System.Console.WriteLine("In finally block")
    ()

exception MyException

[<Example("New exception type examples")>]
let exception_handling_example4() =
    try
        raise MyException
    with
        | _ -> System.Console.WriteLine("In finally block")
    ()

