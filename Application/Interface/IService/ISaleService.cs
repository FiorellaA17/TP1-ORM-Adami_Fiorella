using Domain.Entities;

namespace Application.Interface.IService
{
    public interface ISaleService
    {
        public bool GenerateSale(List<(Guid productId, int quantity)> productIdsAndQuantities);
        public Sale CalculateSale(List<(Guid productId, int quantity)> productIdsAndQuantities);
    }
}
