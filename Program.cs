using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using System.IO;
using System.Globalization;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;

namespace FloodApp
{
    class Program
    {
      


        static void Main(string[] args)
        {
            Console.Clear();

            try
            {
                BusinessLogic.ProcessResults();
            }
            catch (Exception ex)
            {
               
                Console.WriteLine("An Error has occurred..." + ex.Message );
            }
        }

      
       
      
    }

   

   
}
