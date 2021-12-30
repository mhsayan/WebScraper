using Microsoft.EntityFrameworkCore;
using StockData.Data;
using StockData.Scraper.Contexts;
using StockData.Scraper.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockData.Scraper.Repositories
{
    public class StockPriceRepository : Repository<StockPrice, int>, IStockPriceRepository
    {
        public StockPriceRepository(IScraperDbContext context)
            : base((DbContext)context)
        {
        }
    }
}
