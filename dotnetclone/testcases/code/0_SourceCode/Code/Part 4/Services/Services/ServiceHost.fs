module ServiceHost
open System.ServiceModel
open ProFSharp.Services

let startServicing() =
  let host = new ServiceHost( typeof<YahooWeatherService>, Array.empty )
  host.Open()
  printf "Press the 'any' key to stop hosting the service"
  System.Console.ReadKey() |> ignore

startServicing()