module Generics

open ProFSharp

// ======================== Basics

type Stack<'a>() =
    let mutable data = []
    member this.Push(elem : 'a) =
        data <- elem :: data
    member this.Pop() =
        let temp = data.Head
        data <- data.Tail
        temp
    member this.Length() =
        data.Length

let s1 = new Stack<System.String>()

type TwoArgGeneric<'a, 'b>(a : 'a, b : 'b) =
    let vA = a
    let vB = b
    override tag.ToString() =
        System.String.Format("TwoArgGeneric({0},{1})", a, b)
        
type Reflector<'a>() =
    member r.GetMembers() =
        let ty = typeof<'a>
        ty.GetMembers()

type Reflector2() =
    static member GetMembers<'a>() =
        typeof<'a>.GetMembers()


let stringMembers = Reflector2.GetMembers<System.String>()

[<Interface>]
type IDoSomething =
    abstract DoSomething : unit -> unit
    
type OOInterestingType(data : IDoSomething) =
    member it.DoIt() =
        data.DoSomething()

type InterestingType<'a when 'a :> IDoSomething>(data : 'a) =
    member it.DoIt() =
        data.DoSomething()

type MustBeEquallable<'a when 'a : equality>(data : 'a) =
    member it.Equal(other : 'a) =
        data = other
        
type MustBeComparable<'a when 'a : comparison>(data : 'a) =
    member it.Greater(other : 'a) =
        data > other
    member it.Lesser(other : 'a) =
        data < other

type MustBeNullable<'a when 'a : null>(data : 'a) =
    class
    end

type MustBeDoItable<'a when 'a : (member DoIt : unit -> 'a)>() =
    class
    end

type MustBeConstructible<'a when 'a : (new : unit -> 'a)> =
    member it.NewIt() =
        new 'a()

type MustBeStruct<'a when 'a : struct>() =
    class
    end
type MustBeClass<'a when 'a : not struct>() =
    class
    end
    