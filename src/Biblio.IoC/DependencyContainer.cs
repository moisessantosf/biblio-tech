using Biblio.Application.Interfaces;
using Biblio.Application.Services;
using Biblio.Domain.Interfaces;
using Biblio.Infrastructure.Data.Repositories;
using Biblio.Infrastructure.Messaging.Interfaces;
using Biblio.Infrastructure.Messaging.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Biblio.Infrastructure.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IBookRepositoryAsync, BookRepositoryAsync>();

            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ICustomerRepositoryAsync, CustomerRepositoryAsync>();

            services.AddScoped<ILoanService, LoanService>();
            services.AddScoped<ILoanRepositoryAsync, LoanRepositoryAsync>();

            services.AddScoped<IStockService, StockService>();
            services.AddScoped<IStockRepositoryAsync, StockRepositoryAsync>();

            services.AddSingleton<IMessagingService, MockMessagingService>();

        }
    }
}
