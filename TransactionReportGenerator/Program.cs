using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using CsvHelper;
using TransactionReportGenerator.Models;

namespace TransactionReportGenerator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string dataFile = Path.GetFullPath(@"./Data.csv");
            Console.WriteLine(Directory.GetCurrentDirectory());
            Console.WriteLine(dataFile);
        }

        public static Transaction[] LoadTransactions(string pathToCsvFile)
        {
            CsvReader csvReader = new CsvReader(File.OpenText(pathToCsvFile));
            csvReader.Configuration.RegisterClassMap<TransactionMap>();
            return csvReader.GetRecords<Transaction>().ToArray();
        }
    }
}
