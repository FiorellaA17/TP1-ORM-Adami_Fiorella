using Application.Interface.ICommand;
using Application.Interface.IPrinter;
using Application.Interface.IService;
using Domain.Entities;

namespace Application.Service
{
    public class SaleService : ISaleService
    {
        private readonly ISaleCommand _saleCommand;
        private readonly IProductService _product;
        private readonly ISalePrinter _salePrinter;

        public SaleService(ISaleCommand saleRepository, IProductService product, ISalePrinter printer)
        {
            _saleCommand = saleRepository;
            _product = product;
            _salePrinter = printer;
        }
        public bool GenerateSale(List<(Guid productId, int quantity)> productIdsAndQuantities)
        {
            try
            {
                var sale = CalculateSale(productIdsAndQuantities);

                _saleCommand.AddSale(sale); 
                _salePrinter.SalePrint(sale); 
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al crear la venta: " + ex.Message);
                return false;
            }
        }
        public Sale CalculateSale(List<(Guid productId, int quantity)> productIdsAndQuantities)
        {
            var sale = new Sale 
            {
                Date = DateTime.Now,
                SaleProducts = new List<SaleProduct>()
            };

            decimal subtotal = 0;
            decimal totalDiscount = 0;

            foreach (var (productId, quantity) in productIdsAndQuantities)
            {
                var product = _product.GetProductById(productId);
                if (product != null)
                {
                    decimal discountedPrice = product.Price - (product.Price * (product.Discount / 100.0m));
                    subtotal += product.Price * quantity;
                    totalDiscount += Math.Round((product.Price * quantity) - (discountedPrice * quantity), 2);

                    sale.SaleProducts.Add(new SaleProduct
                    {
                        Product = product.ProductId,
                        Quantity = quantity,
                        Price = product.Price,
                        Discount = product.Discount
                    });
                }
            }

            sale.Subtotal = subtotal;
            sale.TotalDiscount = totalDiscount;
            sale.Taxes = 1.21m;
            sale.TotalPay = Math.Round(((subtotal - totalDiscount) * sale.Taxes), 2);

            return sale;
        }
    }
}

