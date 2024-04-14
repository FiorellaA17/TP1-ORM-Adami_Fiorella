using Domain.Entities;

namespace Application.Interface.IService
{
    public interface IProductService
    {
        public List<Product> GetListProducts();
        public Product GetProductById(Guid id);
     
    }
}
