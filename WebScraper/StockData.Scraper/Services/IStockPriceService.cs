using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockData.Scraper.Services
{
    public interface IStockPriceService
    {
        void AddStockPrice(List<List<string>> priceLists);
        int GetId(string name);
    }
}
