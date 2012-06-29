#light

module PrimitiveTypes

open ProFSharp

[<Example("Strings")>]
let string_examples() =
    let s = "Howdy, world!"
    let sLength = s.Length
    ()

/// Bitwise operations
[<Example("Bitwise operations")>]
let bitwise_operations() =
    let bitwiseAND = 0b1111 &&& 0b1100
    let bitwiseOR = 0b1111 ||| 0b1100
    let bitwiseXOR = 0b1111 ^^^ 0b1100
    let bitwiseNEG = ~~~ 0b1111
    let bitwiseSHL = 0b1111 <<< 1
    let bitwiseSHR = 0b1111 >>> 1
    
    System.Console.WriteLine("bitwiseAND = {0}", (bitwiseAND = 0b1100))
    System.Console.WriteLine("bitwiseOR = {0}", (bitwiseOR = 0b1111))
    System.Console.WriteLine("bitwiseXOR = {0}", (bitwiseXOR = 0b0011))
    //System.Console.WriteLine("bitwiseNEG = {0:B}", (bitwiseNEG))
    System.Console.WriteLine("bitwiseSHL = {0}", (bitwiseSHL = 0b11110))
    System.Console.WriteLine("bitwiseSHR = {0}", (bitwiseSHR = 0b0111))


[<Measure>] type usd
[<Measure>] type euro

[<Example("Bitwise operations")>]
let uom_calculations =
    let usdRoyaltyCheck = 15000.00<usd>
    let usdToEuro (dollars : float<usd>) =
        dollars * 1.5<euro/usd>
    System.Console.WriteLine("Royalties in Euro: {0}", usdToEuro usdRoyaltyCheck)

let s = "Hello world!"

[<Literal>]
let s2 = "Hello world again!"

let literals =
    System.Console.WriteLine(s2)
    s2