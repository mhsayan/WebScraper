using Microsoft.EntityFrameworkCore;
using StockData.Data;
using StockData.Scraper.Contexts;
using StockData.Scraper.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockData.Scraper.UnitOfWorks
{
    public class ScraperUnitOfWork : UnitOfWork, IScraperUnitOfWork
    {
        public ICompanyRepository Companies { get; private set; }
        public IStockPriceRepository StockPrices { get; private set; }
        public ScraperUnitOfWork(IScraperDbContext context,
            ICompanyRepository companies,
            IStockPriceRepository stockPrices) 
            : base((DbContext)context)
        {
            Companies = companies;
            StockPrices = stockPrices;
        }

        
    }
}
