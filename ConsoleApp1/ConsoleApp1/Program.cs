using System;
using System.Collections.Generic;

using System.Data.Entity;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {

            ApplicationContext db;
        

                db = new ApplicationContext();
                db.GreanSites.Load();
            Console.WriteLine("Zip file");
            }
    }
}
