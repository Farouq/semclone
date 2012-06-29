namespace FSharpBook.Controllers
open System.Web.Mvc
open FSharpBook.Models
open FSharpBook.Repositories
open System.Web.Routing

[<HandleError>]
type ForecastController() =
  inherit Controller()

  member forecastController.Index() =
    forecastController.View()

  member forecastController.DoQuery(locationQuery:string) =
    let rvd = new RouteValueDictionary()
    rvd.Add("locationQuery",locationQuery)
    forecastController.RedirectToAction("for","forecast",rvd)

  member forecastController.For(locationQuery:string) =
    //TODO: Introduce Interface and do a real repo implementation
    //      DI would be nice, but want to just work first.
    locationQuery |> YahooForecastRepository.GetForecastByLocation |> forecastController.View