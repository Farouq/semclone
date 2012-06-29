namespace FSharpBook.Repositories
open System
open System.Xml.Linq
open FSharpBook.Models

type YahooForecastRepository() =
  
  static member GetWhereOnEarthIdsByLocation locationName =
    //dont disclose the appId
    let appId = "XTZAckLV34E8ggB5EWKNAGya3emTC89OMsKVm9vQAkDnaaqCCq_UDqqx6zFAWe0sZA--"
    let yahooUrlLocationLookup = sprintf "http://where.yahooapis.com/v1/places.q('%s')?appid=%s" locationName appId
    let locationsDoc = yahooUrlLocationLookup |> XDocument.Load
    let yahooWhereOnEarthIds = 
      locationsDoc.Descendants()
      |> Seq.filter (fun(e) -> e.Name.LocalName = "woeid" )
      |> Seq.map (fun(e) -> e.Value |> Int32.Parse )
    yahooWhereOnEarthIds

  static member private ForecastTextToSkyType (text:string) =
    match text with
    | t when t.Contains "Partly Cloudy" -> SkyType.PartlyCloudy
    | t when t.Contains "Rain" -> SkyType.Rain
    | t when t.Contains "Snow" -> SkyType.Snow
    | t when t.Contains "Hurricane" || t.Contains "Tropical Storm" -> SkyType.Hurricane
    | t when t.Contains "Overcast" -> SkyType.Overcast
    | _ -> SkyType.Zombies

  static member GetForecastByLocation locationName =
    let yahooWhereOnEarthIds = YahooForecastRepository.GetWhereOnEarthIdsByLocation locationName
    let getForecastDoc id = 
      let yahooWeatherRSSUrl = sprintf "http://weather.yahooapis.com/forecastrss?w=%d" id
      yahooWeatherRSSUrl |> XDocument.Load
    let weatherDoc = yahooWhereOnEarthIds |> Seq.head |> getForecastDoc
    let firstLocationElement (elems:seq<XElement>) =
      elems |> Seq.filter ( fun(e) -> e.Name.LocalName = "location" ) |> Seq.head
    let locationNameFromElement (elem:XElement) =
      XmlHelpers.getAttr elem "city" 
        + ", " + XmlHelpers.getAttr elem "region" 
        + ", " + XmlHelpers.getAttr elem "country"
    let currentConditions (elems:seq<XElement>) =
      let findFirstConditionElement (elems:seq<XElement>) =
        elems |>
        Seq.filter (fun(e) -> e.Name.LocalName = "condition") |> Seq.head
      let forecastText = XmlHelpers.getAttr ( elems |> findFirstConditionElement) "text" 
      forecastText |> YahooForecastRepository.ForecastTextToSkyType
    let averageTempratures (elems:seq<XElement>) =
      elems 
        |> Seq.map (fun(e) -> e.Attributes()) 
        |> Seq.concat
        |> Seq.filter (fun(a) -> a.Name.LocalName = "low" || a.Name.LocalName = "high")
        |> Seq.map (fun(a) -> a.Value |> Double.Parse)
        |> Seq.average
    let convertDocToForecastModel (doc:XDocument) =
      {
        Location = doc.Descendants() |> firstLocationElement |> locationNameFromElement;
        AverageTemprature = doc.Descendants() |> averageTempratures;
        Skies = doc.Descendants() |> currentConditions
      }  
    weatherDoc |> convertDocToForecastModel