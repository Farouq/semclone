namespace ProFSharp.Events

open ProFSharp.ChartHelper
open ProFSharp.DataGenerator
open ProFSharp.Economics
open ProFSharp.SeriesData
open System
open System.Windows
open System.Windows.Input
open System.Windows.Controls.DataVisualization.Charting
open System.Windows.Controls

type internal MovingAverageModel(chart:Chart, slider:Slider) =
    // used for animated data bining
    let mutable m_dynamicItemsSource = new System.Collections.Generic.List<SeriesDataPoint>()
    // stores the original data
    let mutable m_data = []
    // stores the moving average range (window size)
    let mutable m_range = 1
    
    member private this.GetMovingAverageSeries movingAverage =
        let title = String.Format("{0} Moving Average", m_range)
        DataConverter.ConvertSequenceToAreaSeries title movingAverage

    member private this.InitSeries =
        chart.Series.Clear()
        // generate moving average using range and data
        let movingAverage = MovingAverage m_range m_data
        // build both AreaSeries sets
        let series1 = DataConverter.ConvertSequenceToAreaSeries "Original Data" m_data
        let series2 = this.GetMovingAverageSeries movingAverage
        chart.Series.Add(series1)
        // generate dynamic item source used for animated data binding
        let array = List.toArray(m_data)
        let max = array.Length - 1
        for i in 0..max do
            m_dynamicItemsSource.Add(new SeriesDataPoint(i, array.[i]))
        series2.ItemsSource <- m_dynamicItemsSource
        chart.Series.Add(series2)
        this.UpdateSeries()
        ()

    member this.Init() =
        //initialize members
        m_dynamicItemsSource <- 
            new System.Collections.Generic.List<SeriesDataPoint>()
        m_range <- (int slider.Value)
        m_data <- List.ofSeq <| GenerateData 200.0 50.0 50
        this.InitSeries

    //naive forcast algorithm based on historical flow
    member internal this.Forcast last index =
        let distance = index - last + 2 
        let pastPoint = m_dynamicItemsSource.Item(last - distance)
        let lastPoint = m_dynamicItemsSource.Item(last - 1)
        let trend = lastPoint.Value - pastPoint.Value 
        lastPoint.Value + trend

    member internal this.UpdateSeries() =
        m_range <- (int slider.Value)
        let movingAverage = MovingAverage m_range m_data |> Seq.toList
        for point in m_dynamicItemsSource do
            if point.Index < movingAverage.Length then
                point.Value <- movingAverage.Item(point.Index)
                System.Diagnostics.Debug.WriteLine("point {0},{1}", point.Index, point.Value)
            else
                point.Value <- this.Forcast movingAverage.Length point.Index
            ()
        let legend = chart.Series.[1].LegendItems.Item(0) :?> LegendItem
        legend.Content <- m_range.ToString() + " Moving Average"
        ()

    member this.UpdateMovingAverage(args:RoutedPropertyChangedEventArgs<float>) =
        let oldVal = int args.OldValue
        let newVal = int args.NewValue
        if oldVal = newVal then
            ()
        elif (Math.Abs(oldVal - newVal) > 4) then
            m_range <- newVal
            this.UpdateSeries()
            ()
        else
            m_range <- newVal
            ()


type SilverlightEvents() =
    static member private WireUpSimpleLineChart (container:UserControl) =
        let chart : Chart = downcast container.FindName("chartSimpleLine")
        let buttonLoadSampleData : Button = downcast container.FindName("buttonLoadSampleData")

        buttonLoadSampleData.Click.Add(fun(_) ->
            let data = SampleDataSet.SampleSeries2
            let series = DataConverter.ConvertDataPointsToLineSeries "Sample Series 2" data
            chart.Series.Add(series)
            )

        let buttonGenerateSampleData : Button = downcast container.FindName("buttonGenerateSampleData")
        buttonGenerateSampleData.Click.Add(fun(_) ->
            let data = GenerateDataPoints 125.0 10.0 10
            let series = DataConverter.ConvertDataPointsToLineSeries "Sample Series 3" data
            chart.Series.Add(series)
            )
        ()

    static member private WireUpMovingAverageChart (container:UserControl) =
        let chart : Chart = downcast container.FindName("chartMovingAverage")
        let slider : Slider = downcast container.FindName("sliderMovingAverage")
        
        let model = new MovingAverageModel(chart, slider)
        model.Init()
        let buttonNewMovingAverageData : Button = downcast container.FindName("buttonNewMovingAverageData")
        buttonNewMovingAverageData.Click.Add(fun(_) -> model.Init())

        slider.MouseLeftButtonUp.Add(fun(_) -> model.UpdateSeries())
        slider.ValueChanged.Add(fun(callback) -> model.UpdateMovingAverage(callback))
        ()

    static member RegisterHandlers(container:UserControl) =
        SilverlightEvents.WireUpSimpleLineChart container
        SilverlightEvents.WireUpMovingAverageChart container
        ()
