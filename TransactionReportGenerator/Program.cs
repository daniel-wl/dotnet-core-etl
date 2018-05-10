using System;
using System.IO;
using CsvHelper;
using TransactionReportGenerator.Models;

namespace TransactionReportGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            string dataFile = Path.GetFullPath(@"./Data.csv");
            Console.WriteLine(Directory.GetCurrentDirectory());
            Console.WriteLine(dataFile);

            CsvReader csvReader = new CsvReader(File.OpenText(dataFile));
            csvReader.Configuration.RegisterClassMap<TransactionMap>();
            var records = csvReader.GetRecords<Transaction>();
            foreach(var record in records)
            {
                Console.WriteLine($"${record.Price}");
            }
        }
    }
}
