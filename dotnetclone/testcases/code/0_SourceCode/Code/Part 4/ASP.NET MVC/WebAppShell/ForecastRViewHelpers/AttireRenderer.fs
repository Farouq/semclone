module AttireRenderer
open FSharpBook.Models

let ToAttireString attire =
  match attire with
    | Bikini -> "Bikini"
    | Normal -> "Normal"
    | LightJacket -> "Light Jacket"
    | Coat -> "Coat"
    | Raincoat -> "Raincoat"
    | Parka -> "Parka"
    | BodyArmor -> "Body Armor"

