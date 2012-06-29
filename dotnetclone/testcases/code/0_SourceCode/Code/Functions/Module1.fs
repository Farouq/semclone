// Learn more about F# at http://fsharp.net

module Module1

type MultHelper1 =
    static member multiply (x: int) (y: int) = x * y

type MultHelper2 =
    static member multiply (x: int, y: int) = x * y
    static member multiply (x: double, y: double) = x * y;; 

let inline addInline a b = a + b

let addWithInline (a:int) (b:int) = addInline a b

let add a b = a + b