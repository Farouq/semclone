module ProFSharp.DataGenerator

open ProFSharp.ChartHelper
open System

let GenerateDataPoints offset variance count = 
    let rnd = new Random()
    let randomDouble variance = rnd.NextDouble() * variance
    let indexes = Seq.ofList [0..(count-1)]
    let genValue i = 
        let value = float i + offset + randomDouble variance
        new SeriesDataPoint(i, value)
    Seq.map genValue indexes

let GenerateData offset variance count = 
    let rnd = new Random()
    let randomDouble variance = rnd.NextDouble() * variance
    let indexes = Seq.ofList [0..(count-1)]
    let genValue i = 
        let value = float i + offset + randomDouble variance
        value
    Seq.map genValue indexes

let GenSampleGraphData = GenerateDataPoints 50.0 100.0
let test = GenSampleGraphData 100
