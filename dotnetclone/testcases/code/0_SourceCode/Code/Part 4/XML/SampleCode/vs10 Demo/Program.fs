open System
open System.IO
open System.Linq
open System.Xml
open System.Xml.Linq

let getYahooWeather id =

  let weatherXml = sprintf "http://weather.yahooapis.com/forecastrss?w=%d" id |> XDocument.Load
  
  let readTheElements =
    weatherXml.Elements() |> Seq.iter ( fun(e) -> do printfn "%s" e.Value )

  let byForecastElements (e:XElement) = e.Name.LocalName = "forecast"
  let elementToAttributes (e:XElement) = e.Attributes()
  let byHighAndLowAttributes (a:XAttribute) = a.Name.LocalName = "high" || a.Name.LocalName = "low"
  let attributeValueToDouble (a:XAttribute) = a.Value |> Double.Parse
  let averageForecastTemp = 
    weatherXml.Descendants() 
    |> Seq.filter byForecastElements
    |> Seq.map elementToAttributes
    |> Seq.concat
    |> Seq.filter byHighAndLowAttributes
    |> Seq.map attributeValueToDouble
    |> Seq.average
  averageForecastTemp

let averageRomeovilleTemp = 2484280 |> getYahooWeather
printfn "Average Forecast Temprature for Romeoville, IL is %g" averageRomeovilleTemp


let getYahooWeatherDocument id =
  sprintf "http://weather.yahooapis.com/forecastrss?w=%d" id |> XDocument.Load

let makeCommunityInfoElement (s:string) =
  new XElement(XName.Get("communityinfo"),s)

let insertCommunityInfo (document:XDocument) (communityInfo:XElement) =
  let last sequence =
    sequence
    |> Seq.skip( (sequence |> Seq.length) - 1 )
    |> Seq.head
  let lastForecast = 
     document.Descendants() 
     |> Seq.filter (fun(e) -> e.Name.LocalName = "forecast") 
     |> last
  do communityInfo |> lastForecast.AddAfterSelf

let townDoc = 2484280 |> getYahooWeatherDocument
let townInfo = "Fine Community with Two Sushi Bars" |> makeCommunityInfoElement

do insertCommunityInfo townDoc townInfo

let xmlString = townDoc.Root.ToString()

let writeToXml (doc:XDocument) (file:string) =
  use xmlWriter = file |> XmlWriter.Create
  do doc.WriteTo xmlWriter

do writeToXml townDoc "yourOutput.xml"

let writeToMemory (doc:XDocument) =
  use stream = new MemoryStream()
  use xmlWriter = stream |> XmlWriter.Create
  do doc.WriteTo xmlWriter
  //do something with the memory stream

do writeToMemory townDoc

let getYahooWeatherDOM id =
  use xmlReader = sprintf "http://weather.yahooapis.com/forecastrss?w=%d" id |> XmlReader.Create 
  let xmlDoc = new XmlDocument()
  do xmlReader |> xmlDoc.Load
  xmlDoc

let weatherDom = 2484280 |> getYahooWeatherDOM

do weatherDom.SelectNodes("*") |> Seq.cast<XmlNode> |> Seq.iter ( fun(e) -> do printfn "%s" e.Value )

let allLowOrHighAttributes = weatherDom.SelectNodes("//@low | //@high")
let nodeValueToDouble (n:XmlNode) = n.Value |> Double.Parse
let averageTemp = 
  allLowOrHighAttributes 
  |> Seq.cast<XmlNode> 
  |> Seq.map nodeValueToDouble 
  |> Seq.average

printfn "The average expected temprature is %g" averageTemp

let communityInfo = weatherDom.CreateElement("communityinfo")
do communityInfo.InnerText <- "Fine Community with Two Sushi Bars"
let insertCommunityInfoDom (document:XmlDocument) (communitnyInfo:XmlNode) =
  let last sequence =
    sequence
    |> Seq.skip( (sequence |> Seq.length) - 1 )
    |> Seq.head
  let lastForecast = 
     document.SelectNodes("//*[local-name()='forecast']") 
     |> Seq.cast<XmlNode>
     |> last
  do lastForecast.ParentNode.InsertAfter(communityInfo,lastForecast) |> ignore
do insertCommunityInfoDom weatherDom communityInfo

weatherDom.Save("yourOutput.xml")


    
    
