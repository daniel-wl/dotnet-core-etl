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
        public static int Main(string[] args)
        {
            if(args.Length != 1)
            {
                Console.WriteLine("Usage: dotnet run PathToCsv");
                return 1;
            }

            string dataFile = Path.GetFullPath(args[0]);
            if(!File.Exists(dataFile))
            {
                Console.WriteLine($"File at {dataFile} does not exist.");
                return 1;
            }
            
            Transaction[] transactions = LoadTransactions(dataFile);

            return 0;
        }

        public static Transaction[] LoadTransactions(string pathToCsvFile)
        {
            CsvReader csvReader = new CsvReader(File.OpenText(pathToCsvFile));
            csvReader.Configuration.RegisterClassMap<TransactionMap>();
            return csvReader.GetRecords<Transaction>().ToArray();
        }
    }
}
