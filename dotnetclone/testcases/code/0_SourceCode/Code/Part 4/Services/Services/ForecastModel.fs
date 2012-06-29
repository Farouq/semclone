namespace FSharpBook.Models
type SkyType =
   | Sunny
   | Overcast
   | PartlyCloudy
   | Snow
   | Rain
   | Hurricane
   | Zombies

type Attire =
  | Bikini
  | Normal
  | LightJacket
  | Coat
  | Raincoat
  | Parka
  | BodyArmor

type Forecast = { Location:string; AverageTemprature:double; Skies:SkyType }

type Forecast with
  member forecast.ToAttire() =
    match forecast.AverageTemprature,forecast.Skies with
      | temp,sky when temp > 75.0 && sky = Sunny || sky = PartlyCloudy -> Bikini
      | temp,sky when temp > 65.0 && sky = Sunny || sky = PartlyCloudy -> Normal
      | temp,sky when temp > 45.0 && sky = Sunny || sky = PartlyCloudy -> LightJacket
      | _,Rain -> Raincoat
      | temp,sky when temp < 20.0 -> Parka
      | _,Hurricane -> Coat
      | _,Snow -> Parka
      | _,Zombies -> BodyArmor
      | _ -> Coat //when in doubt, wear a coat...
  