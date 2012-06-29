namespace ProFSharp.ChartHelper

open System.Windows.Controls.DataVisualization.Charting
open System.Windows.Data //for data binding

type DataConverter =
    static member ConvertDataPointsToLineSeries title data =
        let series = new LineSeries()
        series.ItemsSource <- data
        series.DependentValueBinding <- new Binding("Value")
        series.IndependentValueBinding <- new Binding("Index")
        series.Title <- title
        series

    static member ConvertDataPointsToAreaSeries title data =
        let series = new AreaSeries()
        series.ItemsSource <- data
        series.DependentValueBinding <- new Binding("Value")
        series.IndependentValueBinding <- new Binding("Index")
        series.Title <- title
        series

    static member ConvertSequenceToAreaSeries title data =
        let makeDataPoint i value = 
            new SeriesDataPoint(i, value)
        let dataPoints = Seq.mapi makeDataPoint data
        let series = DataConverter.ConvertDataPointsToAreaSeries title dataPoints
        series
