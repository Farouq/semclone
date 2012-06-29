module ProFSharp.Economics

let MovingAverage period data =
     Seq.windowed period data
     |> Seq.map Array.average
     
let testdata = [1.0 .. 10.0]
let result = MovingAverage 4 testdata