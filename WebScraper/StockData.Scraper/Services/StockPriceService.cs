using StockData.Scraper.BusinessObjects;
using StockData.Scraper.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockData.Scraper.Services
{
    public class StockPriceService : IStockPriceService
    {
        private readonly IScraperUnitOfWork _scraperUnitOfWork;

        public StockPriceService(IScraperUnitOfWork scraperUnitOfWork)
        {
            _scraperUnitOfWork = scraperUnitOfWork;
        }

        public int GetId(string name)
        {
            var getEntity = _scraperUnitOfWork.Companies.Get(c => c.TradeCode == name, "").FirstOrDefault();

            var entity = new Company
            {
                Id = getEntity.Id,
                TradeCode = getEntity.TradeCode
            };

            return entity.Id;
        }

        public void AddStockPrice(List<List<string>> priceLists)
        {
            for (int i = 1; i < priceLists.Count; i++)
            {
                var companyId = GetId(priceLists[i][0]);

                _scraperUnitOfWork.StockPrices.Add(
                     new Entities.StockPrice
                    {
                        CompanyId = companyId,
                        LastTradingPrice = Convert.ToDouble(priceLists[i][1]),
                        High = Convert.ToDouble(priceLists[i][2]),
                        Low = Convert.ToDouble(priceLists[i][3]),
                        ClosePrice = Convert.ToDouble(priceLists[i][4]),
                        YesterdayClosePrice = Convert.ToDouble(priceLists[i][5]),
                        Change = Convert.ToDouble(priceLists[i][6]),
                        Trade = Convert.ToDouble(priceLists[i][7]),
                        Value = Convert.ToDouble(priceLists[i][8]),
                        Volume = Convert.ToDouble(priceLists[i][9])

                    });
            }
            _scraperUnitOfWork.Save();
        }
    }
}
