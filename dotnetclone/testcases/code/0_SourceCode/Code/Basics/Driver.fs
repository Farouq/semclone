#light

module BasicsDriver

open System
open System.Reflection
open ProFSharp

[<EntryPoint>]
let Main args =
    let writeInColor color msg =
        let oldColor = Console.ForegroundColor
        Console.ForegroundColor <- color
        Console.WriteLine(msg.ToString())
        Console.ForegroundColor <- oldColor

    writeInColor ConsoleColor.Green "::::::::::: Executing Pro F# Basics samples :::::::::::"

    let SIPNP = 
        BindingFlags.Static ||| BindingFlags.Instance ||| 
        BindingFlags.Public ||| BindingFlags.NonPublic
    Assembly.GetExecutingAssembly().GetTypes() |> Array.iter (fun typ ->
            typ.GetMethods(SIPNP) |> Array.iter (fun meth ->
                let customAttrs = meth.GetCustomAttributes(false)
                try
                    let result = customAttrs |> Array.find (fun attr -> attr :? ExampleAttribute)
                    if result :? ExampleAttribute then
                        writeInColor ConsoleColor.Blue ("==========> Executing " + (result :?> ExampleAttribute).Value)
                        try
                            meth.Invoke(null, [| |]) |> ignore
                        with
                        | ex -> 
                            writeInColor ConsoleColor.Red ("Exception: " + ex.InnerException.Message)
                            writeInColor ConsoleColor.Yellow (ex.StackTrace)
                    else
                        ()
                with
                |   ex -> ()
            )
        )
    0
