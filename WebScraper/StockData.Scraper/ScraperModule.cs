using Autofac;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using StockData.Scraper.Contexts;
using StockData.Scraper.Repositories;
using StockData.Scraper.Services;
using StockData.Scraper.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockData.Scraper
{
    public class ScraperModule : Module
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;
        private readonly IConfiguration _configuration;
        public static ILifetimeScope AutofacContainer { get; set; }

        public ScraperModule(string connectionStringName, string migrationAssemblyName,
            IConfiguration configuration)
        {
            _connectionString = connectionStringName;
            _migrationAssemblyName = migrationAssemblyName;
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ScraperDbContext>().AsSelf()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<ScraperDbContext>().As<IScraperDbContext>()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<StockPriceRepository>().As<IStockPriceRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<CompanyRepository>().As<ICompanyRepository>()
                .InstancePerLifetimeScope();
            
            builder.RegisterType<ScraperUnitOfWork>().As<IScraperUnitOfWork>()
                .InstancePerLifetimeScope();

            builder.RegisterType<StockPriceService>().As<IStockPriceService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<CompanyService>().As<ICompanyService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<DataScraperService>().As<IDataScraperService>()
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
