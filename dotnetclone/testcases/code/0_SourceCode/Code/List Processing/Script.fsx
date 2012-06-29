// This file is a script that can be executed with the F# Interactive.  
// It can be used to explore and test the library project.
// Note that script files will not be part of the project build.

#load "Module1.fs"
open Module1
open System

//filter
let names = ["Sally"; "Donny"; "Johnny"; "Josephine"; "Jose"]
let joNames = 
    List.filter 
        (fun (name: string) -> "Jo" = name.Substring(0,2) ) 
        names

//partition
let numbers = [1 .. 10]
let even, odd = List.partition (fun x -> x % 2 = 0) numbers

//map
let friends = ["Sally"; "Donny"; "Jay"; "Josephine"]
let lowercaseFriends = 
    List.map 
        (fun (str: string) -> str.ToLower()) 
        friends

//map2
let birthdays = ["August 20th"; "April 10th"; "December 31st"; "October 3rd"]
let friendsWithBirthdays = 
    List.map2 
        (fun name birthday -> sprintf "%s was born on %s" name birthday) 
        friends birthdays

//map3
let places = ["Hartford, CT"; "Los Angeles, CA"; "Tokyo, Japan"; "Munich, Germany"]
let friendsBirthdaysAndLocation = 
    List.map3 
        (fun name birthday loc -> 
            sprintf "%s was born on %s and lives in %s" name birthday loc) 
        friends birthdays places

//mapi
let users = ["Sally"; "Donny"; "Johnny"; "Josephine"; "Jose"]
let usersWithUniqueNumber = List.mapi (fun i user -> (i, user)) users 

//mapi2 
let lastnames = ["Struthers"; "Osmond"; "Depp"; 
                 "de Beauharnais"; "Canseco"]
let usersWithLastnamesAndUniqueNumbers = 
    List.mapi2 
        (fun i first last -> (i, sprintf "%s %s" first last)) 
        users lastnames

//choose    
let friends = ["Sally"; "Donny"; "Jay"; "Johnny"; 
               "Josephine"; "Jose"]
let lowercaseShortNames = 
    List.choose 
        (fun (x: string) -> 
            match x with
            | x when x.Length > 5 -> None
            | x -> Some(x.ToLower()))
        friends

//collect
let partiallyParsedNames = ["Thomas; Richard"; "Derk; Kant; Kafka"; 
                            "Captain Crunch; Mister Rogers"]
let parsedNames = 
    List.collect 
        (fun (field: string) -> 
            Array.toList (field.Split( [|"; "|], StringSplitOptions.None ))) 
        partiallyParsedNames

//collect as choose
let friends = ["Sally"; "Donny"; "Jay"; "Johnny"; 
               "Josephine"; "Jose"]
let lowercaseShortNames = 
    List.collect 
        (fun (x: string) -> 
            match x with
            | x when x.Length > 5 -> []
            | x -> [ x.ToLower() ])
        friends
        
//reduce 
let friends = ["Sally"; "Donny"; "Jay"; "Johnny"; 
               "Josephine"; "Jose"]
let longestName = 
    List.reduce 
        (fun (longest: string) (this:string) -> 
            if longest.Length >= this.Length then longest else this) 
        friends

//reduceBack
let longestName = 
    List.reduceBack 
        (fun (this: string) (longest:string) ->
            if longest.Length >= this.Length then longest else this) 
        friends

//fold
let strings = ["The"; "quick"; "brown"; "fox"; "jumps"; 
               "over"; "the"; "lazy"; "dog"]
let totalLength = 
    List.fold 
        (fun acc (str: string) -> acc + str.Length)
        0
        strings
   
//foldBack
let spacedStrings = 
    List.foldBack
        (fun str acc -> 
            if List.isEmpty acc then [str] @ acc
            else [str] @ [" "] @ acc)
        strings
        []
        
//fold2
let punctuation = [" "; " "; " "; " "; " ";
                   " "; " "; " "; "."]
let totalLengthWithPunctuation =
    List.fold2 
        (fun acc (str: string) (punc:string) -> 
            acc + str.Length + punc.Length)
        0
        strings
        punctuation
        
        
//scan
let wordOffset = 
    List.scan 
        (fun acc (str: string) -> acc + str.Length)
        0
        strings
        
//scanBack
let backwardWordOffset = 
    List.scanBack
        (fun (str: string) acc -> acc + str.Length)
        strings
        0
