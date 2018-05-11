using System;
using System.IO;
using NUnit.Framework;
using TransactionReportGenerator;

namespace TransactionReportGenerator.Test
{
    public class CsvLoadingTest
    {
        string PathToCsv = Path.GetFullPath(@"../../../../TransactionReportGenerator/Data.csv");

        [Test]
        public void LoadCsvTest()
        {
            var transactions = Program.LoadTransactions(PathToCsv);
            Assert.Greater(transactions.Length, 0, "Should have some transactions.");
        }
    }
}
