using Autofac;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using StockData.Scraper.Contexts;
using StockData.Scraper.Repositories;
using StockData.Scraper.Services;
using StockData.Scraper.UnitOfWorks;
using StockData.Worker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockData.Worker
{
    public class WorkerModule : Module
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;
        private readonly IConfiguration _configuration;
        public static ILifetimeScope AutofacContainer { get; set; }

        public WorkerModule(string connectionStringName, string migrationAssemblyName,
            IConfiguration configuration)
        {
            _connectionString = connectionStringName;
            _migrationAssemblyName = migrationAssemblyName;
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {            
            builder.RegisterType<DataScraperModel>().InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
