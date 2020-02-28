using System;
using System.IO;
using CsvHelper;
using NUnit.Framework;
using TransactionReportGenerator;
using TransactionReportGenerator.Reports;

namespace TransactionReportGenerator.Test
{
    public class CsvLoadingTest
    {
        string KnownGoodCsv = Path.GetFullPath(@"../../../TestFiles/GoldenData.csv");
        string CsvWithTooManyColumns = Path.GetFullPath(@"../../../TestFiles/TooManyColumns.csv");
        string CsvWithTooFewColumns = Path.GetFullPath(@"../../../TestFiles/TooFewColumns.csv");

        [Test]
        public void LoadValidCsvTest()
        {
            var transactions = TransactionLoader.LoadTransactions(KnownGoodCsv);
            Assert.AreEqual(12, transactions.Count, "Incorrect number of rows.");
        }

        [Test]
        public void LoadCsvWithTooManyColumnsTest()
        {
            Assert.Throws<BadDataException>(() => TransactionLoader.LoadTransactions(CsvWithTooManyColumns), "Loading a csv with too many columns should throw BadDataException");
        }

        [Test]
        public void LoadCsvWithTooFewColumnsTest()
        {
            Assert.Throws<BadDataException>(() => TransactionLoader.LoadTransactions(CsvWithTooFewColumns), "Loading a csv with too few columns should throw BadDataException");
        }
    }
}
