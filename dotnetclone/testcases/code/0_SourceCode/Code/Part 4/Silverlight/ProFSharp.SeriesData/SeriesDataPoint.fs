namespace ProFSharp.ChartHelper

open System.ComponentModel

type SeriesDataPoint(index:int, value:float) =
    let mutable v = value 
    let propertyChanged = Event<_, _>()

    member this.Index 
        with get() = index
    member this.Value 
        with get() = v
        and set(value) = 
            v <- value
            propertyChanged.Trigger(this, 
                PropertyChangedEventArgs("Value")) 
    interface INotifyPropertyChanged with
        [<CLIEvent>]
        member this.PropertyChanged = propertyChanged.Publish