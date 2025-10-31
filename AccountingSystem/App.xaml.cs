using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AccountingSystem.Data;
using AccountingSystem.Interfaces;
using AccountingSystem.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AccountingSystem
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IServiceProvider? ServiceProvider { get; private set; }

        public App()
        {
            var services = new ServiceCollection();

            // Register DbContext
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(@"Server=DESKTOP-Q4HRE44;Database=AccountingDB;Trusted_Connection=True;TrustServerCertificate=True;"));

            // Register repositories
            services.AddScoped<ITransactionRepository, TransactionRepository>();

            // Register your ViewModels or other services here
            // services.AddTransient<MainViewModel>();

            ServiceProvider = services.BuildServiceProvider();
        }
    }
}
