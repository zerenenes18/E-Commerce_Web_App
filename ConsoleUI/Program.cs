using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {

            ProductManager productManager = new ProductManager(new EfProductDal());

            foreach (var p in productManager.GetByUnitPrice(10,20))
            {
                Console.WriteLine("Product Name :" + p.ProductName 
                                + " ---CategoryID:" + p.CategoryId 
                                + " ---Price:" + p.UnitPrice );
            } 
        }
    }
}
