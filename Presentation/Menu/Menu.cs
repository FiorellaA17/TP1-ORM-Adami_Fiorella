using Application.Interface.IPrinter;
using Application.Interface.IService;
using Infraestructure.Controller;


namespace Presentation.Menu
{
    public class Menu
    {
        private readonly ProductController _productController;
        private readonly SaleController _saleController;

        public Menu(ProductController productController, SaleController saleController)
        {
            _productController = productController;
            _saleController = saleController;
        }
        public void ShowMenu()
        {
            string option;
            while (true)
            {  
                Console.WriteLine("=====================================");
                Console.WriteLine("             OctopuStore             ");
                Console.WriteLine("=====================================\n");
                Console.WriteLine("¡Bienvenido!\n");
                Console.WriteLine("1. Listar productos");
                Console.WriteLine("2. Realizar una venta");
                Console.WriteLine("0. Salir\n");
                Console.Write("Seleccione una opción: ");

                option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        _productController.GetListProducts();
                        break;
                    case "2":
                        _saleController.CreateSale();
                        break;
                    case "0":
                        Console.WriteLine("¡Hasta luego!");
                        Thread.Sleep(1000);
                        Console.Clear();
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Opción inválida, por favor intente nuevamente.");
                        break;
                }
                
            }
        }
    }
}
