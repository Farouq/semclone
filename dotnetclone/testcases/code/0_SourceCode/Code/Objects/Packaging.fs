module Packaging

open ProFSharp

open System

[<Example("Open examples")>]
let open_examples() =
    Console.WriteLine("Much shorter, thank you")
    
    ()
    
module Examples =

    open System

    type Person(fn : string, ln : string, a : int) =
        member this.FirstName = fn
        member this.LastName = ln
        member this.Age = a
        override this.ToString() =
            String.Format("{0} {1} is {2} years old",
                this.FirstName, this.LastName, this.Age)

module MoreExamples =

    type Student() =
        override this.ToString() = "Student"

module FunctionalExample =
    let doSomething() =
        Console.WriteLine("I did something!")
    let aValue = 5
