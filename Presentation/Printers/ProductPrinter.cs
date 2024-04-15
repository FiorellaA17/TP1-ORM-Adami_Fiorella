using Application.Interface.IPrinter;
using Domain.Entities;


namespace Presentation.Printers
{
    public class ProductPrinter : IProductPrinter
    {
        public void ListProductDetail(List<Product> products)
        {
            Console.WriteLine("\n=====================================");
            Console.WriteLine("          Lista de Productos         ");
            Console.WriteLine("=====================================\n");
            try
            {
                if (products.Count > 0)
                { 
                    foreach (var product in products)
                    {
                        PrintProduct(product);
                    }
                }

                else
                {
                    Console.WriteLine("\nActualmente no hay productos en lista.\n");
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error al listar los productos: {ex.Message}");
            }

        }
        public void PrintProduct(Product product)
        {
            Console.WriteLine($"ID: {product.ProductId}");
            Console.WriteLine($"Nombre: {product.Name}");
            Console.WriteLine($"Descripción: {product.Description}");
            Console.WriteLine($"Precio: {product.Price}");
            Console.WriteLine($"Categoría: {product.CategoryName.Name}");
            Console.WriteLine($"Descuento: {product.Discount} %");
            Console.WriteLine("\n--------------------------------------------------------------\n");
        }
    }
}
