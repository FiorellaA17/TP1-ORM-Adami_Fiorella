using Domain.Entities;

namespace Application.Interface.IPrinter
{
    public interface IProductPrinter
    {
        public void ListProductDetail(List<Product> products);
        public void PrintProduct(Product product);
    }
}
