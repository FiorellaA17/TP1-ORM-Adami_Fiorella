using Application.Interface.IPrinter;
using Application.Interface.IService;


namespace Presentation.Menu
{
    public class SaleMenu
    {
        private readonly IProductService _productService;
        private readonly ISaleService _saleService;
       

        public SaleMenu(IProductService productService, ISaleService saleService)
        {
            _productService = productService;
            _saleService = saleService;
        }

        public void ShowSaleMenu()
        {
            string option;
            while (true)
            {
                //Console.Clear();
                Console.WriteLine("\n=== Menú de Venta === \n");
                Console.WriteLine("1. Registrar venta");
                Console.WriteLine("2. Volver al menu principal");
                Console.WriteLine("0. Salir\n");
                Console.Write("Seleccione una opción: ");

                option = Console.ReadLine();

                switch (option)
                    {
                        case "1":
                             _saleService.CreateSale(GetProductSelection()); 
                            Console.ReadKey();
                            break;

                        case "2":
                            return;
                        case "0":
                            Console.WriteLine("Saliendo del menú de ventas.");       
                            return;
                        default:
                            Console.WriteLine("Opcion invalida. Intente nuevamente.");
                            break;
                    }
            }
        }

        private List<(Guid productId, int quantity)> GetProductSelection()
        {
            var productIdsAndQuantities = new List<(Guid productId, int quantity)>();
            Console.WriteLine("=====================================");
            Console.WriteLine("   Registrar ventas  ");
            Console.WriteLine("=====================================");

            while (true)
            {
                Console.Write("\nIngrese el ID del producto o '0' para salir: ");
                string productIdInput = Console.ReadLine();

                if (productIdInput.ToUpper() == "0")
                {
                    break;
                }

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

                            Console.Write("Desea ingresar otro producto? s/n: ");
                            string continueInput = Console.ReadLine();

                            if (continueInput.ToUpper() != "S")
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
                        Console.WriteLine("ID de producto no válido.");
                    }
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

