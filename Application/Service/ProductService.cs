using Application.Interface.IQuery;
using Application.Interface.IService;
using Domain.Entities;

namespace Application.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductQuery _QueryProduct;

        public ProductService(IProductQuery QueryProduct)
        {
            _QueryProduct = QueryProduct;
        }

        public List<Product> GetListProducts()
        {
            return _QueryProduct.GetListProducts();
        }

        public Product GetProductById(Guid id)
        {
            return _QueryProduct.GetListProducts().FirstOrDefault(product => product.ProductId == id);
        }
    }
}
