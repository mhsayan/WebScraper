using Autofac;
using HtmlAgilityPack;
using StockData.Scraper.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockData.Worker.Model
{
    public class DataScraperModel
    {
        private IDataScraperService _dataScraperService;

        public DataScraperModel(IDataScraperService dataScraperService)
        {
            _dataScraperService = dataScraperService;
        }

        public string GetMarketStatus()
        {
            return _dataScraperService.MarketStatus();
        }

        public void StartProcessing()
        {
            _dataScraperService.DataCollector();
        }
    }
}
