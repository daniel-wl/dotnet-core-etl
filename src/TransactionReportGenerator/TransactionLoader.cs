using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using TransactionReportGenerator.Models;

namespace TransactionReportGenerator
{
    public static class TransactionLoader
    {
        public static IEnumerable<Transaction> LoadTransactions(string pathToCsvFile)
        {
            using(CsvReader csvReader = GetCsvReader(pathToCsvFile))
            {
                return csvReader.GetRecords<Transaction>().ToArray();
            }
        }

        internal static CsvReader GetCsvReader(string pathToCsvFile)
        {
            CsvReader csvReader = new CsvReader(File.OpenText(pathToCsvFile));
            csvReader.Configuration.RegisterClassMap<TransactionMap>();
            csvReader.Configuration.DetectColumnCountChanges = true;
            return csvReader;
        }
    }
}
