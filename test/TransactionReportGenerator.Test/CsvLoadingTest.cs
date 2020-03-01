using System;
using System.IO;
using CsvHelper;
using FluentAssertions;
using NUnit.Framework;
using TransactionReportGenerator.Core;

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
            transactions.Should().HaveCount(12);
        }

        [Test]
        public void LoadCsvWithTooManyColumnsTest()
        {
            Action loadBadCsv = () => TransactionLoader.LoadTransactions(CsvWithTooManyColumns);
            loadBadCsv.Should().Throw<BadDataException>();
        }

        [Test]
        public void LoadCsvWithTooFewColumnsTest()
        {
            Action loadBadCsv = () => TransactionLoader.LoadTransactions(CsvWithTooFewColumns);
            loadBadCsv.Should().Throw<BadDataException>();
        }
    }
}
