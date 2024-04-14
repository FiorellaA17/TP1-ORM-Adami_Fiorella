using Application.Interface.IPrinter;
using Domain.Entities;


namespace Presentation.Printers
{
    public class SalePrinter : ISalePrinter
    {
        public void SalePrint(Sale sale)
        {
            Console.WriteLine("\n----- Imprimiendo Venta -----\n");
            Console.WriteLine($"Fecha: {sale.Date}");

            Console.WriteLine("Productos:");
            foreach (var saleProduct in sale.SaleProducts)
            {
                Console.WriteLine($"- {saleProduct.Quantity} unidades de {saleProduct.ProductName.Name}: ${saleProduct.Price} (descuento: {saleProduct.Discount}%)");
            }

            SaleDetail(sale);
            Console.WriteLine("\n----- Fin Impresión -----\n");
        }

        public void SaleDetail(Sale sale)
        {
            Console.WriteLine("\n= Detalle de venta =\n");
            Console.WriteLine($"Subtotal: $ {sale.Subtotal}");
            Console.WriteLine($"Descuento total: $ {sale.TotalDiscount}");
            Console.WriteLine($"Impuestos IVA {(sale.Taxes -1)* 100}%");
            Console.WriteLine($"Total a pagar: $ {sale.TotalPay}");

        }
    }
}
