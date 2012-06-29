namespace ProFSharp.Services
open FSharpBook.Models
open WeatherRendering

type ForecastServiceRenderer() =
  static member Render (forecast:Forecast) =
    let renderedForecast = new ProFSharp.Services.Forecast()
    renderedForecast.AverageExpectedTemprature <- forecast.AverageTemprature
    renderedForecast.City <- forecast.Location
    renderedForecast.RecommendedAttire <- forecast.ToAttire() |> WeatherRendering.ToAttireString
    renderedForecast.Weather <- forecast.Skies |> WeatherRendering.ToWeatherString
    renderedForecast