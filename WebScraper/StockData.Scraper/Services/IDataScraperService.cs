using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockData.Scraper.Services
{
    public interface IDataScraperService
    {
        void DataCollector();
        void DataSeparator(List<List<string>> allTexts);
        void PostToCompany(List<string> companies);
        void PostToStockPrice(List<List<string>> StockData);
        string MarketStatus();
    }
}