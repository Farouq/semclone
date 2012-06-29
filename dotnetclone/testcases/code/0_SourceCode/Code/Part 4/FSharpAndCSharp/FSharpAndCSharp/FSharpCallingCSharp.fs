module FSharpCallingCSharp
open System
open CSharpLibrary
//typical call to C#
let newCSharpObject = new CSharpLibraryUsedWithFSharp()
let someResult = newCSharpObject.SomeFunction(42,69)
let anotherResult = (42,69) |> newCSharpObject.SomeFunction

let someFSharpFunction a b = a + b
let curriedFunction = 42 |> someFSharpFunction
let actualResult = 69 |> curriedFunction

//construction
let someCSharpObject = new CSharpLibraryUsedWithFSharp()
//using parameters, initializers, or both
let paramsOnly = new CSharpLibraryUsedWithFSharp(11,22)
let initializersOnly = new CSharpLibraryUsedWithFSharp(SomeMutableProperty = 55, AnotherMutableProperty = 56)
let paramsAndInitializers = new CSharpLibraryUsedWithFSharp(11,22,SomeMutableProperty = 55, AnotherMutableProperty = 56)


//this will work
let thisShouldReturnTrue = System.String.IsNullOrEmpty(null)

//as F# programmers, we want this to work, as null isn't something
//we deal with, with a preference for options.  However, when
//calling the framework, None isnt the same thing as null
//let thisShouldAlsoReturnTrueButNeitherCompilesNorWorks = System.String.IsNullOrEmpty(None)

//pretend this is your normal null spewing C# function
let someRandomStrangeFunctionThatMightReturnNull = 
  match DateTime.Now.Day with
  | 31 -> "This is a 31st Day in a 31 Day Month, Yipee"  
  | _ -> null

//this is how you will protect against such functions in your F# code
let wontBeNull = 
  match someRandomStrangeFunctionThatMightReturnNull with
    | null -> None
    | v -> Some(v)

//example of passing a delegate
let delegateExample = new CSharpLibraryUsedWithFSharp(SomeMutableProperty = 55, AnotherMutableProperty = 56)
let addNumbersAsDelegate = new CSharpLibraryUsedWithFSharp.BinaryIntegerMathOperation(fun x y -> x + y)
let subNumbersAsDelegate = new CSharpLibraryUsedWithFSharp.BinaryIntegerMathOperation(fun x y -> x - y)
let shouldBe111 = delegateExample.PerformAMathOperationOnMyProperties(addNumbersAsDelegate)
let shouldBeMinusOne = delegateExample.PerformAMathOperationOnMyProperties(subNumbersAsDelegate)
let simplerForm = delegateExample.PerformAMathOperationOnMyProperties(fun x y -> x + y)
let subSimplerForm = delegateExample.PerformAMathOperationOnMyProperties(fun x y -> x - y)

//event example
let eventExample = new CSharpLibraryUsedWithFSharp()
eventExample.add_FinishedABeer( fun s -> printfn "Drank a %s" s ) 
do eventExample.HaveADrinkingBinge()
//wont remove the event, since it is a different function
eventExample.remove_FinishedABeer( fun s -> printfn "Drank a %s" s )

let anotherEventExample = new CSharpLibraryUsedWithFSharp()
let handleMyBeer = new CSharpLibraryUsedWithFSharp.BeerFinishingHandler( fun s -> printfn "Drank a %s" s )
anotherEventExample.add_FinishedABeer( handleMyBeer )
//this remove will work, since it is the same delegate instance
anotherEventExample.remove_FinishedABeer( handleMyBeer )