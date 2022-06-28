using HtmlAgilityPack;
using StockData.Scraper.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockData.Scraper.Services
{
    public class DataScraperService : IDataScraperService
    {
        private ICompanyService _companyService;
        private IStockPriceService _stockPriceService;

        public DataScraperService(ICompanyService companyService, IStockPriceService stockPriceService)
        {
            _companyService = companyService;
            _stockPriceService = stockPriceService;
        }

        public void DataCollector()
        {
            List<List<string>> allTexts = new List<List<string>>();
            var html = @"https://www.dse.com.bd/latest_share_price_scroll_l.php";

            HtmlWeb web = new HtmlWeb();
            var htmlDoc = web.Load(html);

            var tableNodes = htmlDoc.DocumentNode.Descendants("table")
                .Where(node => node.GetAttributeValue("class", "").Contains("shares-table")).ToList();            

            foreach (var item in tableNodes)
            {
                HtmlNodeCollection trNodes = item.ChildNodes;
                foreach (var trNode in trNodes)
                {
                    if (trNode.NodeType == HtmlNodeType.Element)
                    {
                        HtmlNodeCollection tdNodes = trNode.ChildNodes;
                        List<string> text = new List<string>();
                        foreach (var tdNode in tdNodes)
                        {
                            if (tdNode.NodeType == HtmlNodeType.Element)
                            {
                                string tempText = tdNode.InnerText;
                                string[] lineParts = tempText.Split('\t', '\r', '\n');

                                foreach (var again in lineParts)
                                {
                                    if (!string.IsNullOrEmpty(again))
                                    {
                                        if (again == "--")
                                        {
                                            text.Add("0");
                                        }
                                        else
                                        {
                                            text.Add(again);
                                        }
                                    }
                                }
                            }
                        }
                        allTexts.Add(text);
                    }
                }
            }
            DataSeparator(allTexts);
        }

        public void DataSeparator(List<List<string>> allTexts)
        {
            List<string> companies = new List<string>();
            List<List<string>> StockData = new List<List<string>>();

            for (int i = 0; i < allTexts.Count; i++)
            {
                List<string> text = new List<string>();
                if (i != 0)
                {
                    for (int j = 0; j < allTexts[i].Count; j++)
                    {
                        if (j == 0)
                        {
                            continue;
                        }
                        else if (j == 1)
                        {
                            companies.Add(allTexts[i][j]);
                            text.Add(allTexts[i][j]);
                        }
                        else
                        {
                            text.Add(allTexts[i][j]);
                        }
                    }
                }
                StockData.Add(text);
            }

            PostToCompany(companies);
            PostToStockPrice(StockData);
        }

        public void PostToCompany(List<string> companies)
        {
            _companyService.AddCompany(companies);
        }

        public void PostToStockPrice(List<List<string>> StockData)
        {
            _stockPriceService.AddStockPrice(StockData);
        }

        public string MarketStatus()
        {
            List<List<string>> allTexts = new List<List<string>>();
            var html = @"https://www.dse.com.bd/latest_share_price_scroll_l.php";

            HtmlWeb web = new HtmlWeb();
            var htmlDoc = web.Load(html);

            var marketStatus = htmlDoc.DocumentNode
                .SelectSingleNode("//html//body//div//div//div//header/div//span//span//b").InnerText;

            return marketStatus;
        }
    }
}