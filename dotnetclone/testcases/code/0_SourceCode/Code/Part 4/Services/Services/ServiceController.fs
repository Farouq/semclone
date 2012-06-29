namespace ProFSharp.Services
open FSharpBook.Repositories

type ForecastServiceController() =
  member forecastController.For(locationQuery:string) =
    //TODO: Introduce Interface and do a real repo implementation
    //      DI would be nice, but want to just work first.
    locationQuery |> YahooForecastRepository.GetForecastByLocation |> ForecastServiceRenderer.Render