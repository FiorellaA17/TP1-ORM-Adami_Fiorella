using Domain.Entities;

namespace Application.Interface.IService
{
    public interface ISaleService
    {
        public Sale GenerateSale(List<(Guid productId, int quantity)> productIdsAndQuantities);
        public void CreateSale(List<(Guid productId, int quantity)> productIdsAndQuantities);
    }
}
