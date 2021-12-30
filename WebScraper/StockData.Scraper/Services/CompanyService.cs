using StockData.Scraper.BusinessObjects;
using StockData.Scraper.Exceptions;
using StockData.Scraper.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockData.Scraper.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IScraperUnitOfWork _scraperUnitOfWork;

        public CompanyService(IScraperUnitOfWork scraperUnitOfWork)
        {
            _scraperUnitOfWork = scraperUnitOfWork;
        }

        public void AddCompany(List<string> companies)
        {  
            foreach (var company in companies)
            {
                var getEntity = _scraperUnitOfWork.Companies.Get(c => c.TradeCode == company, "").FirstOrDefault();

                if (getEntity == null)
                {
                    _scraperUnitOfWork.Companies.Add(
                            new Entities.Company
                            {
                                TradeCode = company.ToString()
                            });
                }           
            }
            _scraperUnitOfWork.Save();
        }
    }
}