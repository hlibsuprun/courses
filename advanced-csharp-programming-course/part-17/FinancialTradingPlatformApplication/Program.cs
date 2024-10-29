﻿namespace FinancialTradingPlatformApplication;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine($"Method name: Main, ThreadId {Thread.CurrentThread.ManagedThreadId}");

        var stockMarketTechnicalAnalysisData =
            new StockMarketTechnicalAnalysisData("STKZA", new DateTime(2010, 1, 1), new DateTime(2020, 1, 1));

        var dateTimeBefore = DateTime.Now;

        //Call methods synchronously
        /*
        decimal[] data1 = stockMarketTechnicalAnalysisData.GetOpeningPrices();
        decimal[] data2 = stockMarketTechnicalAnalysisData.GetClosingPrices();
        decimal[] data3 = stockMarketTechnicalAnalysisData.GetPriceHighs();
        decimal[] data4 = stockMarketTechnicalAnalysisData.GetPriceLows();
        decimal[] data5 = stockMarketTechnicalAnalysisData.CalculateStockastics();
        decimal[] data6 = stockMarketTechnicalAnalysisData.CalculateFastMovingAverage();
        decimal[] data7 = stockMarketTechnicalAnalysisData.CalculateSlowMovingAverage();
        decimal[] data8 = stockMarketTechnicalAnalysisData.CalculateUpperBoundBollingerBand();
        decimal[] data9 = stockMarketTechnicalAnalysisData.CalculateLowerBoundBollingerBand();*/

        //Call methods asynchronously

        var tasks = new List<Task<decimal[]>>();

        var getOpeningPricesTask = Task.Run(() => stockMarketTechnicalAnalysisData.GetOpeningPrices());
        var getClosingPricesTask = Task.Run(() => stockMarketTechnicalAnalysisData.GetClosingPrices());
        var getPriceHighsTask = Task.Run(() => stockMarketTechnicalAnalysisData.GetPriceHighs());
        var getPriceLowsTask = Task.Run(() => stockMarketTechnicalAnalysisData.GetPriceLows());
        var calculateStockasticsTask = Task.Run(() => stockMarketTechnicalAnalysisData.CalculateStockastics());
        var calculateFastMovingAverageTask =
            Task.Run(() => stockMarketTechnicalAnalysisData.CalculateFastMovingAverage());
        var calculateSlowMovingAverageTask =
            Task.Run(() => stockMarketTechnicalAnalysisData.CalculateSlowMovingAverage());
        var calculateUpperBollingerBandTask =
            Task.Run(() => stockMarketTechnicalAnalysisData.CalculateUpperBoundBollingerBand());
        var calculateLowerBollingerBandTask =
            Task.Run(() => stockMarketTechnicalAnalysisData.CalculateLowerBoundBollingerBand());

        tasks.Add(getOpeningPricesTask);
        tasks.Add(getClosingPricesTask);
        tasks.Add(getPriceHighsTask);
        tasks.Add(getPriceLowsTask);
        tasks.Add(calculateStockasticsTask);
        tasks.Add(calculateFastMovingAverageTask);
        tasks.Add(calculateSlowMovingAverageTask);
        tasks.Add(calculateUpperBollingerBandTask);
        tasks.Add(calculateLowerBollingerBandTask);

        Task.WaitAll(tasks.ToArray());

        var data1 = tasks[0].Result;
        var data2 = tasks[1].Result;
        var data3 = tasks[2].Result;
        var data4 = tasks[3].Result;
        var data5 = tasks[4].Result;
        var data6 = tasks[5].Result;
        var data7 = tasks[6].Result;
        var data8 = tasks[7].Result;
        var data9 = tasks[8].Result;

        var dateTimeAfter = DateTime.Now;

        var timeSpan = dateTimeAfter.Subtract(dateTimeBefore);

        Console.WriteLine(
            $"Total time for operations to complete took {timeSpan.Seconds} {(timeSpan.Seconds > 1 ? "seconds" : "second")}");

        DisplayDataOnChart(data1, data2, data3, data4, data5, data6, data7, data8, data9);

        Console.ReadKey();
    }

    public static void DisplayDataOnChart(decimal[] data1, decimal[] data2, decimal[] data3, decimal[] data4,
        decimal[] data5, decimal[] data6, decimal[] data7, decimal[] data8, decimal[] data9)
    {
        //Code goes here to add data to the chart
        Console.WriteLine("Data is displayed on the chart.");
    }
}

public class StockMarketTechnicalAnalysisData
{
    public StockMarketTechnicalAnalysisData(string stockSymbol, DateTime dateTimeStart, DateTime dateTimeEnd)
    {
        //Code here gets stock market data from remote server
    }

    public decimal[] GetOpeningPrices()
    {
        decimal[] data;

        Console.WriteLine(
            $"Method name: {nameof(GetOpeningPrices)}, ThreadId: {Thread.CurrentThread.ManagedThreadId} ");

        Thread.Sleep(1000); //Represents the time it takes for the operation to complete

        data = new decimal[] { }; //In the real world the 'data' variable would contain decimal data

        return data;
    }

    public decimal[] GetClosingPrices()
    {
        decimal[] data;

        Console.WriteLine(
            $"Method name: {nameof(GetClosingPrices)}, ThreadId: {Thread.CurrentThread.ManagedThreadId} ");

        Thread.Sleep(1000); //Represents the time it takes for the operation to complete

        data = new decimal[] { }; //In the real world the 'data' variable would contain decimal data

        return data;
    }

    public decimal[] GetPriceHighs()
    {
        decimal[] data;

        Console.WriteLine($"Method name: {nameof(GetPriceHighs)}, ThreadId: {Thread.CurrentThread.ManagedThreadId} ");

        Thread.Sleep(1000); //Represents the time it takes for the operation to complete

        data = new decimal[] { }; //In the real world the 'data' variable would contain decimal data

        return data;
    }

    public decimal[] GetPriceLows()
    {
        decimal[] data;

        Console.WriteLine($"Method name: {nameof(GetPriceLows)}, ThreadId: {Thread.CurrentThread.ManagedThreadId} ");

        Thread.Sleep(1000); //Represents the time it takes for the operation to complete

        data = new decimal[] { }; //In the real world the 'data' variable would contain decimal data

        return data;
    }

    public decimal[] CalculateStockastics()
    {
        decimal[] data;

        Console.WriteLine(
            $"Method name: {nameof(CalculateStockastics)}, ThreadId: {Thread.CurrentThread.ManagedThreadId} ");

        Thread.Sleep(10000); //Represents the time it takes for the operation to complete

        data = new decimal[] { }; //In the real world the 'data' variable would contain decimal data

        return data;
    }

    public decimal[] CalculateFastMovingAverage()
    {
        decimal[] data;

        Console.WriteLine(
            $"Method name: {nameof(CalculateFastMovingAverage)}, ThreadId: {Thread.CurrentThread.ManagedThreadId} ");

        Thread.Sleep(6000); //Represents the time it takes for the operation to complete

        data = new decimal[] { }; //In the real world the 'data' variable would contain decimal data

        return data;
    }

    public decimal[] CalculateSlowMovingAverage()
    {
        decimal[] data;

        Console.WriteLine(
            $"Method name: {nameof(CalculateSlowMovingAverage)}, ThreadId: {Thread.CurrentThread.ManagedThreadId} ");

        Thread.Sleep(7000); //Represents the time it takes for the operation to complete

        data = new decimal[] { }; //In the real world the 'data' variable would contain decimal data

        return data;
    }

    public decimal[] CalculateUpperBoundBollingerBand()
    {
        decimal[] data;

        Console.WriteLine(
            $"Method name: {nameof(CalculateUpperBoundBollingerBand)}, ThreadId: {Thread.CurrentThread.ManagedThreadId} ");

        Thread.Sleep(5000); //Represents the time it takes for the operation to complete

        data = new decimal[] { }; //In the real world the 'data' variable would contain decimal data

        return data;
    }

    public decimal[] CalculateLowerBoundBollingerBand()
    {
        decimal[] data;

        Console.WriteLine(
            $"Method name: {nameof(CalculateLowerBoundBollingerBand)}, ThreadId: {Thread.CurrentThread.ManagedThreadId} ");

        Thread.Sleep(5000); //Represents the time it takes for the operation to complete

        data = new decimal[] { }; //In the real world the 'data' variable would contain decimal data

        return data;
    }
}