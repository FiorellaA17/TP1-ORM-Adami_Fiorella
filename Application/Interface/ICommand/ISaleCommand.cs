using Domain.Entities;

namespace Application.Interface.ICommand
{
    public interface ISaleCommand
    {
        public void AddSale(Sale sale);
    }
}
