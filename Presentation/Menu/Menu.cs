﻿using Application.Interface.IPrinter;
using Application.Interface.IService;


namespace Presentation.Menu
{
    public class Menu
    {
        private readonly IProductService _ProductService;
        //private readonly ISaleService _SaleService;
        private readonly IProductPrinter _productPrinter;
        private readonly SaleMenu _saleMenu;

        public Menu(IProductService productService, IProductPrinter productPrinter, SaleMenu saleMenu)
        {
            _ProductService = productService;
            _productPrinter = productPrinter;
            _saleMenu = saleMenu;
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
                        _productPrinter.ListProductDetail(_ProductService.GetListProducts());
                        //StartSale();
                        _saleMenu.ShowSaleMenu();
                        break;
                    case "2":
                        //StartSale();
                        _saleMenu.ShowSaleMenu();
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

        //private void StartSale()
        //{
        //    var saleMenu = new SaleMenu(_ProductService,_SaleService);
        //    saleMenu.ShowSaleMenu();
        //}
    }
}
