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
            return _context.Products.Include(x => x.CategoryName).ToList();
        }
        
        public Product GetProductById(Guid id)
        {
           return _context.Products.FirstOrDefault(product => product.ProductId == id);
        }
    }
}

