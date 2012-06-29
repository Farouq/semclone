namespace ProFSharp.Services
open System.ServiceModel
[<ServiceBehavior(ConfigurationName="Weather")>]    
type YahooWeatherService() =
  interface IWeatherService with
    member s.GetForecastFor place =
      (new ForecastServiceController()).For(place)

