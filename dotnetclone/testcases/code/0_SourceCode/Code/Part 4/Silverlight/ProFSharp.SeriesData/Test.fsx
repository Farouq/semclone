open System

let GenerateData offset variance count = 
    let rnd = new Random()
    let randomDouble variance = rnd.NextDouble() * variance
    let indexes = Seq.ofList [0..(count-1)]
    let genValue i = 
        let value = float i + offset + randomDouble variance
        value
    Seq.map genValue indexes

let MovingAverage period data =
     Seq.windowed period data
     |> Seq.map Array.average

let dataGenerator = GenerateData 50.0 100.0 200

let data1 = List.ofSeq dataGenerator
let data2 = List.ofSeq <| MovingAverage 10 data1