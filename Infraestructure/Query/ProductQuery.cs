using Application.Interface.IQuery;
using Domain.Entities;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace Infraestructure.Query
{
    public class ProductQuery : IProductQuery
    {
        private readonly StoreDbContext _context;

        public ProductQuery(StoreDbContext Context)
        {
            _context = Context;
        }

        public List<Product> GetListProducts()
        {
            try
            {
                return _context.Products.Include(x => x.CategoryName).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener la lista de productos: {ex.Message}");
                return null;
            }
            
        }

        public Product GetProductById(Guid id)
        {
            try
            {
                return _context.Products.FirstOrDefault(product => product.ProductId == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener el producto por ID: {ex.Message}");
                return null;
            }

        }
    }
}

