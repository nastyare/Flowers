using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using Microsoft.VisualBasic.FileIO;
using System.Xml.Schema;

namespace Csv
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = @"C:\Users\anast\OneDrive\Документы\important.csv";
            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ",",
                HasHeaderRecord = false,
            };

            Console.WriteLine("Введите месяц: ");
            var currentMonth = Console.ReadLine();

            string smallestFlowerName = null;
            string smallestFlowerSize = null; 

            try
            {
                using (var reader = new StreamReader(filePath))
                using (var csv = new CsvReader(reader, csvConfig))
                {
                   var records = csv.GetRecords<FlowersInfo>();                        
                  
                   foreach (var record in records)
                   {
                        if (record.months.Contains(currentMonth))
                        {
                            Console.WriteLine($"Название: {record.name}, размер цветка (в см): {record.size}");                          
                            if (smallestFlowerSize == null || Convert.ToInt32(record.size) < Convert.ToInt32(smallestFlowerSize))
                            {
                                smallestFlowerName = record.name;
                                smallestFlowerSize = record.size;
                            }
                                        
                        }
                        
                        
                   }
                   if (smallestFlowerName != null)
                   {
                       Console.WriteLine($"Самый маленький цветок: {smallestFlowerName}\nРазмер: {smallestFlowerSize}см");
                   }
                   else
                   {
                       Console.WriteLine("В этом месяце цветы не цветут.");
                   }
                } 
            } catch (Exception ex)
            {
                Console.WriteLine($"Error");
            }
            

        }

    }

    
    public class FlowersInfo
    {
        public string name { get; set; }
        public string months { get; set; }
        public string size { get; set; }
    }
   
    
}