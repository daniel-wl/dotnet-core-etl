using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using TransactionReportGenerator.Models;

namespace TransactionReportGenerator.BusinessLogic
{
    public static class TransactionLoader
    {
        public static List<Transaction> LoadTransactions(string pathToCsvFile)
        {
            CsvReader csvReader = GetCsvReader(pathToCsvFile);
            return csvReader.GetRecords<Transaction>().ToList();
        }

        internal static CsvReader GetCsvReader(string pathToCsvFile)
        {
            CsvReader csvReader = new CsvReader(File.OpenText(pathToCsvFile));
            csvReader.Configuration.RegisterClassMap<TransactionMap>();
            return csvReader;
        }
    }
}