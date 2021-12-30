using StockData.Data;
using StockData.Scraper.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockData.Scraper.Repositories
{
    public interface IStockPriceRepository : IRepository<StockPrice, int>
    {
    }
}
