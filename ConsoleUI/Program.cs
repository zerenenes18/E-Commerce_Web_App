using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            // Data Transformation Objects 
            //ProductTest();
            // IoC container 

            ProductTest();
            //CategoryTest();

        }

        private static void CategoryTest()
        {
            CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
            foreach (var c in categoryManager.GetAll())
            {
                Console.WriteLine(c.CategoryName);
            }
        }

        private static void ProductTest()
        {
            ProductManager productManager = new ProductManager(new EfProductDal());
            var result = productManager.GetProductDetails();
            
            if (result.Success == true)
            {
                foreach (var p in result.Data)
                {

                    Console.WriteLine("Product Name :" + p.ProductName
                                    + " ---CategoryName:" + p.CategoryName
                                    + " ---UnitsInStock:" + p.UnitsInStock);
                }
            }
            else
            {
                Console.WriteLine(result.Message);
            }
        }
    }
}
