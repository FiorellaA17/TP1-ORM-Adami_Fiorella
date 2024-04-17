using Application.Interface.ICommand;
using Application.Interface.IPrinter;
using Application.Interface.IQuery;
using Application.Interface.IService;
using Application.Service;
using Infraestructure.Command;
using Infraestructure.Controller;
using Infraestructure.Persistence;
using Infraestructure.Query;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Presentation.Menu;
using Presentation.Printers;


class Program
{
    static void Main()
    {
        var builder = new HostBuilder()
            .ConfigureServices((hostContext, services) =>
            {
                services.AddSingleton<StoreDbContext>();
                services.AddTransient<IProductQuery, ProductQuery>();
                services.AddTransient<IProductService, ProductService>();
                services.AddTransient<ISaleService, SaleService>();
                services.AddTransient<ISaleCommand, SaleCommand>();
                services.AddScoped<ISalePrinter, SalePrinter>();
                services.AddScoped<IProductPrinter, ProductPrinter>();
                services.AddScoped<ProductController>();
                services.AddScoped<SaleController>();          
                services.AddScoped<Menu>();      
            });

        var app = builder.Build();
        var menu = app.Services.GetRequiredService<Menu>();
        menu.ShowMenu();
        app.Run();
    }
}
