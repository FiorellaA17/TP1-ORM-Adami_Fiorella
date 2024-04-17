using Application.Interface.ICommand;
using Domain.Entities;
using Infraestructure.Persistence;


namespace Infraestructure.Command
{
    public class SaleCommand : ISaleCommand
    {
        private readonly StoreDbContext _dbContext;

        public SaleCommand(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddSale(Sale sale)
        {
            try
            {
                _dbContext.Sales.Add(sale);
                _dbContext.SaveChanges();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Se produjo un error al guardar la venta: " + ex.Message);
            }           
        }
    }
}
