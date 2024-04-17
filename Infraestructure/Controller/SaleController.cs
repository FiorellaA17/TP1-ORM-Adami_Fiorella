using Application.Interface.IPrinter;
using Application.Interface.IService;


namespace Infraestructure.Controller
{
    public class SaleController
    {
        private readonly ISaleService _saleService;
        private readonly ISalePrinter _salePrinter;
        private readonly IProductService _productService;

        public SaleController(ISaleService saleService, ISalePrinter salePrinter, IProductService productService)
        {
            _saleService = saleService;
            _salePrinter = salePrinter;
            _productService = productService;
        }

        public void CreateSale()
        {
            var productIdsAndQuantities = GetProductSelection();

            if (productIdsAndQuantities.Count > 0)
            {
                var sale = _saleService.CalculateSale(productIdsAndQuantities);
                _salePrinter.SaleDetail(sale);

                Console.Write("\n¿Desea registrar y luego imprimir la venta? (S/N): ");
                string confirmation = Console.ReadLine().ToUpper();

                switch (confirmation)
                {
                    case "S":
                        bool saleCreatedAndPrinted = _saleService.GenerateSale(productIdsAndQuantities);

                        if (saleCreatedAndPrinted)
                        {
                            Console.WriteLine("\nVenta registrada e impresa exitosamente.\n");
                        }
                        else
                        {
                            Console.WriteLine("Error al registrar o imprimir la venta. Consulte el log de errores para más detalles.");
                        }
                        break;

                    case "N":
                        Console.WriteLine("Venta cancelada.");
                        break;

                    default:
                        Console.WriteLine("Opción no válida. Ingrese 'S' para registrar y imprimir, o 'N' para cancelar.");
                        Console.WriteLine("\nPresione una tecla cualquiera para volver al menu...");
                        break;
                }
            }
            else
            {
                Console.WriteLine("La venta no contiene ningún producto.");
                Console.WriteLine("\nVolviendo al menu...");
            }
        }
        public List<(Guid productId, int quantity)> GetProductSelection()
        {
            var productIdsAndQuantities = new List<(Guid productId, int quantity)>();

            while (true)
            {
                Console.Write("\nIngrese el ID del producto: ");
                String productIdInput = Console.ReadLine();

                try
                {
                    Guid productId = Guid.Parse(productIdInput.Trim());
                    var product = _productService.GetProductById(productId);

                    if (product != null)
                    {
                        Console.Write($"\nProducto seleccionado: {product.Name}, Precio: {product.Price} Descuento: {product.Discount}% \nIngrese la cantidad: ");
                        string quantityInput = Console.ReadLine();

                        try
                        {
                            int quantity = int.Parse(quantityInput.Trim());
                            if (quantity <= 0)
                            {
                                throw new Exception("La cantidad debe ser un número entero positivo.");
                            }

                            Console.WriteLine($"\nProducto agregado: {product.Name}({quantity} unidades)\n");
                            productIdsAndQuantities.Add((productId, quantity));

                            Console.Write("\n¿Desea ingresar otro producto? s/n: ");
                            string continueInput = Console.ReadLine().ToUpper();
                            if (continueInput == "N")
                            {
                                break;
                            }
                        }
                        catch (FormatException ex)
                        {
                            Console.WriteLine($"Error al ingresar la cantidad: {ex.Message}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("ID de producto no válido.");                    }
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"Error al ingresar el ID del producto: {ex.Message}");
                }
            }
            return productIdsAndQuantities;
        }
    }
}
