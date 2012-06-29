// This file is a script that can be executed with the F# Interactive.  
// It can be used to explore and test the library project.
// Note that script files will not be part of the project build.

#load "Module1.fs"
open Module1

//
//Automatic Generalization and Restriction
//

let min arg1 arg2 = if arg1 < arg2 then arg1 else arg2
let add a b = a + b

let add a b = a + b
add 2.0 3.0
add 2 3;;

//
//The inline Keyword
//

let inline add a b = a + b;;
add 2.0 3.0;;
add 2 3;;

//
//Type Annotations
//

let plusOne (x: int) = x + 1
let plusOne x : int = x + 1
let plus x (y: double) = x + y

//
//Generics and Constraints
//

let areEqual arg1 arg2 = 
    arg1 = arg2

let areEqual<'a when 'a : equality> (arg1: 'a) (arg2: 'a) = 
    arg1 = arg2 

let isNull<'a when 'a : equality and 'a : null> (arg: 'a) = 
    arg = null

//Constraints table
open System
let func<'a when 'a :> Object> (x: 'a) = x //Type
let func<'a when 'a : null> (x: 'a) = x //Nullable
let func<'a when 'a : ( new: unit -> 'a )> (x: 'a) = x //New()
let func<'a when 'a : struct> (x: 'a) = x //Value Type
let func<'a when 'a : not struct> (x: 'a) = x //Reference Type
let func<'a when 'a : comparison> (x: 'a) = x //Comparison
let func<'a when 'a : equality> (x: 'a) = x //Equality

//
//Partial Application
//

let add x y = x + y
let plusOne = add 1;;

plusOne 5;;

//
//Currying
//

let explicitCurryAddTwoNumbers x = 
    function y -> x + y 
    
let plusOne = explicitCurryAddTwoNumbers 1;;

plusOne 2;;

let addTwoNumbers x y = x + y
let plusOne = addTwoNumbers 1;;

plusOne 2;;

let explicitCurryAddThreeNumbers x = 
    function y -> function z -> (x + y + z)
    
let addThreeNumbers x y z = x + y + z

//
//Restrictions on Curried Functions and Methods
//

let square (x: int) = x * x
let square (x: double) = x * x;;

type SquareHelper = 
    static member square (x: int) = x * x
    static member square (x: double) = x * x;;

type BadMultHelper =
    static member multiply (x: int) (y: int) = x * y
    static member multiply (x: double) (y: double) = x * y;;

type GoodMultHelper =
    static member multiply (x: int, y: int) = x * y
    static member multiply (x: double, y: double) = x * y;; 

//
// Higher Order Functions 
//

//-Takes a function
let square x = x * x
let performAndAddOne func = func() + 1;;

performAndAddOne (fun () -> square 2);;

//ForEach Recursive

let rec forEach (func: 'a -> unit) (collection: list<'a>) = 
    if (Seq.isEmpty collection) then 
        ()
    else 
        func (List.head collection)
        forEach func (List.tail collection)

let printNum num = printfn "%i" num

let testList = [ 0; 1; 2; 3; 4 ];;

forEach printNum testList

//
//Storing Functions
//

let getDataTransform () = function input -> input

let processEachDatasetWithCurrentTransform (data1, data2, data3) = 
    let transform = getDataTransform()
    let newData1 = transform data1
    let newData2 = transform data2
    let newData3 = transform data3
    (newData1, newData2, newData3)

let getDataTransforms () = [function input -> input]

let processDatasetWithCurrentTransforms data = 
    let transforms = getDataTransforms()
    let rec applyTransforms data transforms = 
        match transforms with
        | [] -> data
        | thisTransform :: otherTransforms -> applyTransforms (thisTransform data) otherTransforms
    applyTransforms data transforms

//
//Creating Functions at Runtime
//

//Closures Example

let calcApproxDerivative f dx x = (f(x + dx) - f(x - dx))  / 2.0 * dx;;

let approxDerivative f dx = 
    let derivative x = (f(x + dx) - f(x - dx)) / 2.0 * dx in
    derivative

//Lambda Expression Example 

let lamdbaDerivative f dx = fun x -> (f(x + dx) - f(x - dx)) / 2.0 * dx

//Partial Application Example

let calcApproxDerivative f dx x = (f(x + dx) - f(x - dx)) / 2.0 * dx

let square (x: float) = x * x
let derivativeOfSquare = calcApproxDerivative square 0.001;;