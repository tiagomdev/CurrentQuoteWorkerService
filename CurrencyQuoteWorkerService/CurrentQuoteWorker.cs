using CurrencyQuoteWorker.Background.Models;
using CurrencyQuoteWorkerService.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyQuoteWorker.Background
{
    public class CurrentQuoteWorker
    {
        async Task<string> SaveResult(List<CurrencyResult> currencyResults)
        {
            string path = $"{Directory.GetCurrentDirectory()}\\Files\\Results";

            if (Directory.Exists(path) is false)
                Directory.CreateDirectory(path);

            var fileName = $"{DateTime.Now.ToString("yyyyMMdd_hhmmss")}.csv";
            var fullPath = $"{path}\\{fileName}";

            using (StreamWriter sw = File.CreateText(fullPath))
            {
                await sw.WriteLineAsync("ID_MOEDA;DATA_REF;VL_COTACAO");
                foreach (var currencyResult in currencyResults)
                {
                    await sw.WriteLineAsync($"{currencyResult.Id};{currencyResult.Date.ToString("yyyy-MM-dd")};{currencyResult.Value.ToString().Replace('.', ',')}");
                }
                await sw.FlushAsync();
            }

            return fullPath;
        }

        async Task<int> GetQuoteCodeBy(string currencyId)
        {
            var quoteCodePath = $"{Directory.GetCurrentDirectory()}\\Files\\DadosMoedaCode.csv";
            var recordQuoteCodeLines = await File.ReadAllLinesAsync(quoteCodePath);

            foreach (var line in recordQuoteCodeLines)
            {
                var records = line.Split(';');
                if (records[0].Equals(currencyId))
                    return int.Parse(records[1]);
            }

            return default;
        }

        internal async Task Proccess(CurrencyByDateDTO[] currencyByDateList)
        {
            Console.WriteLine("Start proccess!");
            var stopWatch = new Stopwatch();

            var quotePath = $"{Directory.GetCurrentDirectory()}\\Files\\DadosCotacao.csv";
            var currencyPath = $"{Directory.GetCurrentDirectory()}\\Files\\DadosMoeda.csv";

            stopWatch.Start();

            var quotes = new List<Quote>();
            var coins = new List<Currency>();

            var recordQuoteLines = await File.ReadAllLinesAsync(quotePath);
            for (int i = 1; i < recordQuoteLines.Count(); i++)
            {
                quotes.Add(new Quote(recordQuoteLines[i]));
            }

            var recordCurrencyLines = await File.ReadAllLinesAsync(currencyPath);
            for (int i = 1; i < recordCurrencyLines.Count(); i++)
            {
                coins.Add(new Currency(recordCurrencyLines[i]));
            }

            Console.WriteLine($"Converted {quotes.Count} quotes and {coins.Count} coins in {stopWatch.ElapsedMilliseconds} mm");

            var currentResults = new List<CurrencyResult>();
            foreach (var currencyByDate in currencyByDateList)
            {
                var coinsByDateCurrent = coins.Where(c => c.Id == currencyByDate.Currency && 
                c.Date >= currencyByDate.StartDate && c.Date <= currencyByDate.EndDate);

                if (coinsByDateCurrent.Count() == 0)
                    continue;

                var quoteCode = await GetQuoteCodeBy(currencyByDate.Currency);
                foreach (var coinCurrent in coinsByDateCurrent)
                {
                    var currencyResult = new CurrencyResult() { Id = coinCurrent.Id, Date = coinCurrent.Date };
                    var quote = quotes.FirstOrDefault(q => q.Code == quoteCode && q.Date.Date == coinCurrent.Date.Date);
                    if(quote != null)
                        currencyResult.Value = quote.Value;

                    currentResults.Add(currencyResult);
                }
            }

            Console.WriteLine($"Found {currentResults.Count} to save in {stopWatch.ElapsedMilliseconds} mm");

            var resultPath = await SaveResult(currentResults);

            Console.WriteLine($"Saved result on path={resultPath} in {stopWatch.ElapsedMilliseconds} mm");

            stopWatch.Stop();
        }
    }
}
