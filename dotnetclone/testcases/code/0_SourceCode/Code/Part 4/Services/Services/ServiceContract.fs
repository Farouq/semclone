namespace ProFSharp.Services
open System
open System.ServiceModel
open System.Runtime.Serialization

[<DataContract>]
type Forecast() = 
    let mutable _city : string = String.Empty
    let mutable _averageExpectedTemprature : double = 0.0
    let mutable _recommendedAtture : string = String.Empty
    let mutable _weather : string = String.Empty
    [<DataMember>]
    member public f.City 
      with get() = _city and set(v) = _city <- v
    [<DataMember>]
    member public f.AverageExpectedTemprature 
      with get() = _averageExpectedTemprature and set(v) = _averageExpectedTemprature <- v
    [<DataMember>]
    member public f.RecommendedAttire 
      with get() = _recommendedAtture and set(v) = _recommendedAtture <- v
    [<DataMember>]
    member public f.Weather 
      with get() = _weather and set(v) = _weather <- v

[<ServiceContract>]  
type IWeatherService =
  [<OperationContract>]
  abstract member GetForecastFor: place:string -> Forecast
