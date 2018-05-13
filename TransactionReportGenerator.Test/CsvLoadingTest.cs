using System;
using System.IO;
using NUnit.Framework;
using TransactionReportGenerator;
using TransactionReportGenerator.Reports;

namespace TransactionReportGenerator.Test
{
    public class CsvLoadingTest
    {
        string PathToCsv = Path.GetFullPath(@"../../../../TransactionReportGenerator/Data.csv");

        [Test]
        public void LoadCsvTest()
        {
            var transactions = TransactionLoader.LoadTransactions(PathToCsv);
            Assert.Greater(transactions.Count, 0, "Should have some transactions.");
        }
    }
}
