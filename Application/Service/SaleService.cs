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

        public void CreateSale(List<(Guid productId, int quantity)> productIdsAndQuantities)
        {
            var sale = GenerateSale(productIdsAndQuantities);

            if (sale.SaleProducts.Count > 0)
            {
                _salePrinter.SaleDetail(sale);

                Console.Write("\n¿Desea registrar y luego imprimir la venta? (S/N): ");
                string confirmation = Console.ReadLine();

                if (confirmation.ToUpper() == "S")
                {
                    _saleCommand.AddSale(sale);

                    _salePrinter.SalePrint(sale);

                    Console.WriteLine("\nVenta registrada e impresa exitosamente.\n");
                    Console.WriteLine("\nPresione una tecla cualquiera para volver al menu...");
                }
                else
                {
                    Console.WriteLine("Venta cancelada.");
                    Console.WriteLine("\nPresione una tecla cualquiera para volver al menu...");
                }
            }
            else
            {
                Console.WriteLine("La venta no contiene ningún producto.");
                Console.WriteLine("\nPresione una tecla cualquiera para volver al menu...");
            }
        }

        public Sale GenerateSale(List<(Guid productId, int quantity)> productIdsAndQuantities)
        {
            var newSale = new Sale
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

                    newSale.SaleProducts.Add(new SaleProduct
                    {
                        Product = product.ProductId,
                        Quantity = quantity,
                        Price = product.Price,
                        Discount = product.Discount
                    });
                }
            }

            newSale.Subtotal = subtotal;
            newSale.TotalDiscount = totalDiscount;
            newSale.Taxes = 1.21m;
            newSale.TotalPay = Math.Round(((subtotal - totalDiscount) * newSale.Taxes),2);

            return newSale;
        }

    }
}
