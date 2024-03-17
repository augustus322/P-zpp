/* using Database.Models;
using DataBaseZPP.Data;
using System.Diagnostics;

using DatabaseContext context = new DatabaseContext();


Product Laptop = new Product()
{
    Name = "LaptopA",
    Price = 9.99M,
    Category = "Tech"
};
context.Product.Add(Laptop);

Product Pc = new Product()
{
    Name = "LaptopB",
    Price = 9.92M,
    Category = "Tech"
};
Product Sc = new Product()
{
    Name = "LaptopC",
    Price = 9.22M,
    Category = "Tech",
    Description = "sadjas"
};
context.Add(Pc);

context.SaveChanges();
*/

using DataBaseZPP.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;


namespace DataBaseZPP
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            
        }
    }
}