using Finik.StockAndCompany.Core.Interfaces;
using Finik.StockAndCompany.Core.Models;
using System.Collections.Concurrent;
using System.Xml;

namespace Finik.StocksAndCompanies.WebApi.HostedServices
{
    public class Grabber : BackgroundService
    {
        private readonly ICompanyManager _companyManager;
        private readonly IStockManager _stockManager;

        private const Int32 pageSize = 10;
        private const Int32 maxCount = 200;

        private Int32 page;

        private readonly Object lockPage = new Object();

        private BlockingCollection<(Company company, Stock stock)> values = new BlockingCollection<(Company company, Stock stock)>(maxCount);

        public Grabber(ICompanyManager companyManager, IStockManager stockManager)
        {
            _companyManager = companyManager;
            _stockManager = stockManager;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                var tasks = new List<Task>
                {
                    Task.Run(async () => await GetDataFromSource(stoppingToken)),
                    Task.Run(async () => await ChekAndSaveData(stoppingToken)),
                    Task.Run(async () => await GetDataFromSource(stoppingToken)),
                    Task.Run(async () => await GetDataFromSource(stoppingToken))
                };
                await Task.WhenAll(tasks);
            }
            catch (OperationCanceledException ex)
            { 
            }

        }

        private async Task<XmlDocument> LoadDocument(String url)
        {
            using var client = new HttpClient();
            var xmlStr = await client.GetStringAsync(url);
            var document = new XmlDocument();
            document.LoadXml(xmlStr);
            return document;
        }

        private async Task GetDataFromSource(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    if (values.Count + pageSize < maxCount)
                    {
                        Monitor.Enter(lockPage);
                        var currentPage = page;
                        page++;
                        Monitor.Exit(lockPage);
                        var doc = await LoadDocument(GetFullUrl(currentPage));
                        var rows = doc.GetElementsByTagName("row");
                        if (rows.Count == 0)
                        {
                            Monitor.Enter(lockPage);
                            page = 0;
                            Monitor.Exit(lockPage);
                            continue;
                        }
                        foreach (XmlNode row in rows)
                        {
                            if (String.IsNullOrEmpty(row.Attributes["emitent_id"].Value))
                                continue;
                            var company = new Company
                            {
                                Id = Int32.Parse(row.Attributes["emitent_id"].Value),
                                NickName = row.Attributes["shortname"].Value,
                                FullName = row.Attributes["emitent_title"].Value ?? row.Attributes["shortname"].Value,
                                Inn = row.Attributes["emitent_inn"].Value
                            };
                            var stock = new Stock
                            {
                                Id = Int32.Parse(row.Attributes["id"].Value),
                                Type = row.Attributes["type"].Value,
                                Isin = row.Attributes["isin"].Value,
                                TradeCode = row.Attributes["regnumber"].Value,
                                Category = row.Attributes["group"].Value,
                                CompanyId = company.Id,
                                Company = company
                            };
                            values.Add((company, stock));
                        }
                    }
                }
                catch (Exception ex)
                { 
                }
                Thread.Sleep(3000);
            }
        }

        private async Task ChekAndSaveData(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    if (values.Count > 0)
                    {
                        var data = values.Take();
                        if (await _companyManager.GetCompany(data.company.Id) == null)
                            await _companyManager.CreateCompany(data.company);
                        if (await _stockManager.GetStock(data.stock.Id) == null)
                            await _stockManager.CreateStock(data.stock);
                    }
                }
                catch (Exception ex)
                {
                }
                Thread.Sleep(500);
            }
        }

        private String GetFullUrl(Int32 page)
            => $"https://iss.moex.com/iss/securities?engine=stock&start={page*pageSize}&limit={pageSize}";
    }
}
