// Learn more about F# at http://fsharp.net

module DataAndDataTypes

type OrderedPropertyList<'a> = 
    | OrderedList of 'a list
    
let CreateOrderedList list = OrderedList(List.sort list)

let removeDuplicates (orderedList: OrderedPropertyList<'a>) =
    let removeDupes list = 
        List.foldBack (fun element noDupes ->
            match noDupes with
            | [] -> [element]
            | (h::_) -> if element = h then noDupes
                        else element :: noDupes)      
            list []
    match orderedList with
    | OrderedList list -> OrderedList(removeDupes list)

