module WeatherRendering
open FSharpBook.Models

let ToWeatherString sky =
 match sky with
   | Sunny -> "Sunny"
   | Overcast -> "Overcast"
   | PartlyCloudy -> "Partly Cloudy"
   | Snow -> "Snow"
   | Rain -> "Rain"
   | Hurricane -> "Hurricane"
   | Zombies -> "Oh NO! ZOMBIES!!!"

let ToAttireString attire =
  match attire with
    | Bikini -> "Bikini"
    | Normal -> "Normal"
    | LightJacket -> "Light Jacket"
    | Coat -> "Coat"
    | Raincoat -> "Raincoat"
    | Parka -> "Parka"
    | BodyArmor -> "Body Armor"