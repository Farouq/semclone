namespace ProFSharp.SeriesData

open ProFSharp.ChartHelper
open System.Windows.Controls

type SampleDataSet() =
    static member SampleSeries1 =
        let data = new ObjectCollection()
        data.Add(new SeriesDataPoint(0, 124.1))
        data.Add(new SeriesDataPoint(1, 124.3))
        data.Add(new SeriesDataPoint(2, 125.7))
        data.Add(new SeriesDataPoint(3, 115.4))
        data.Add(new SeriesDataPoint(4, 115.9))
        data.Add(new SeriesDataPoint(5, 125.0))
        data.Add(new SeriesDataPoint(6, 133.6))
        data.Add(new SeriesDataPoint(7, 131.9))
        data.Add(new SeriesDataPoint(8, 127.3))
        data.Add(new SeriesDataPoint(9, 137.3))
        data
    static member SampleSeries2 =
        let data = new ObjectCollection()
        data.Add(new SeriesDataPoint(0, 122.7))
        data.Add(new SeriesDataPoint(1, 123.2))
        data.Add(new SeriesDataPoint(2, 125.7))
        data.Add(new SeriesDataPoint(3, 125.4))
        data.Add(new SeriesDataPoint(4, 135.9))
        data.Add(new SeriesDataPoint(5, 135.0))
        data.Add(new SeriesDataPoint(6, 123.6))
        data.Add(new SeriesDataPoint(7, 135.9))
        data.Add(new SeriesDataPoint(8, 139.2))
        data.Add(new SeriesDataPoint(9, 147.3))
        data


