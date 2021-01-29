using CurrencyQuoteWorkerService.Shared.DTOs;
using System;
using System.Threading;

namespace CurrencyQuoteWorker.Background
{
    class Program
    {
        static void Main(string[] args)
        {
            var worker = new CurrentQuoteWorker();
            var service = new Services.CurrentQuoteService();
            while (true)
            {
                //normalmente para processamentos em background eu uso o hangfire, fiz dessa forma bem simples para ser independente do frammework que estiver utlizando do .net

                try
                {
                    var filters = service.GetNextFilters();

                    //em situações qem que pode lidar com mais de uma thread não é uma boa prática usar o Wait()
                    worker.Proccess(filters).Wait();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                

                Thread.Sleep(120000);
            }
        }
    }
}
