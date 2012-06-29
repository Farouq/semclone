#light

module public ProFSharp

open System

[<AttributeUsage(AttributeTargets.Method,AllowMultiple=false)>]
type public ExampleAttribute(name:string) =
    inherit Attribute()
    member x.Value = name
