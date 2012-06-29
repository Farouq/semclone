#light
    // Note that #light will be on by default when F# ships

module LexicalStructure

(* This is
a multi-line
comment *)

(* (* (* Notice how multi-line comments nest well,
since the first star-right-parens combination will 
not end the multi-line comment *)
Only after we match up the number of multi-line 
comment pairs *) 
will the comment really be closed. *)
let x = 5

// This is a single-line comment

/// This is a documentation comment. Note that if you are
/// looking at this file inside of Visual Studio, this
/// documentation comment will be bound against the first
/// identifier in the code snippets that follow, which is
/// the identifier "y".

// These are legitimate identifiers
//
let y = 1
let aReallyLongIdentifierName = 2
let _underscores_are_OK_too = 3
let soAreNumbers123AfterALetter = 4
/// This is not what you think--it will compile, but it
/// actually defines two different identifiers (abc and
/// foo) with the same value (5).
let abc&foo = 5
//let can'tincludeotherpunctuation,moron = 6

// These are not (uncomment to see for sure)
//
// let 123abcNumbersCantComeFirst = 100
// let abc@foo = 100

// Demonstration of significant whitespace
//
let outer =
    let x = 2
    if x = 1 then
        System.Console.WriteLine("Hello, F#")
        if x = 1 then
            System.Console.WriteLine("Again!")
      else
        System.Console.WriteLine("Uh... how did this happen?")
