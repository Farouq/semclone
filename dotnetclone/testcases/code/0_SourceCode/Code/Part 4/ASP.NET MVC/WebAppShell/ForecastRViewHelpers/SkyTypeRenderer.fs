module SkyTypeRenderer
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
let ToImageFileName sky =
 match sky with
   | PartlyCloudy -> "PartlyCloudy"
   | Zombies -> "Zombies"
   | _ -> ToWeatherString sky