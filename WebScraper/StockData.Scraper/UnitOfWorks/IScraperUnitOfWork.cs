using StockData.Data;
using StockData.Scraper.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockData.Scraper.UnitOfWorks
{
    public interface IScraperUnitOfWork : IUnitOfWork
    {
        ICompanyRepository Companies { get;}
        IStockPriceRepository StockPrices { get;}
    }
}
