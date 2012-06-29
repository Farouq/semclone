open ProFSharp.Services

let getWeatherFor place = 
    use myWeatherService = new WeatherServiceClient()
    let theWeather = place |> myWeatherService.GetForecastFor

    printf "The weather in %s should average %g degrees with %s, wear %s!" 
      theWeather.City 
      theWeather.AverageExpectedTemprature 
      theWeather.Weather 
      theWeather.RecommendedAttire
    printf "\n\nPress any key to continue"
    System.Console.ReadKey() |> ignore 

getWeatherFor "Romeoville, IL"