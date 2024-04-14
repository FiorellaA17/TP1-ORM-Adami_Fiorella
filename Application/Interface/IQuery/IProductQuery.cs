using Domain.Entities;

namespace Application.Interface.IQuery
{
    public interface IProductQuery
    {
        public List<Product> GetListProducts();
        public Product GetProductById(Guid id);
    }
}
