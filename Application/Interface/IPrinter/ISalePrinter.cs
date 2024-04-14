using Domain.Entities;

namespace Application.Interface.IPrinter
{
    public interface ISalePrinter
    {
        public void SalePrint(Sale sale);
        public void SaleDetail(Sale sale);
    }
}
