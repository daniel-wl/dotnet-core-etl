using TransactionReportGenerator.BusinessLogic;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using TransactionReportGenerator.Models;
using System.Linq;

namespace TransactionReportGenerator.Test
{
    public class SalesSummaryTest
    {
        [Test]
        public void CreateSalesSummaryWithNullTransactionsTest()
        {
            Assert.Throws<ArgumentNullException>(() => new SalesSummary(null), "Creating a sales summary with null transactions should throw");
        }

        [Test]
        public void CreateSalesSummaryWithEmptyTransactionsTest()
        {
            Assert.Throws<ArgumentException>(() => new SalesSummary(new List<Transaction>()), "Creating a sales summary with empty transactions should throw");
        }

        [Test]
        public void CreateSalesSummaryWithTransactionsTest()
        {
            Assert.DoesNotThrow(() => new SalesSummary(new List<Transaction>(TestData.GetFakeTransactions())), "Creating a sales summary with a non-empty transaction list should not throw.");
        }

        [Test]
        public void GetUniqueInvestorsTest()
        {
            string[] investors = SalesSummary.GetUniqueInvestors(TestData.GetFakeTransactions());
            Assert.AreEqual(investors.Length, 3, "Incorrect number of unique investors");
            Assert.IsNotNull(investors.SingleOrDefault(i => i == "John Doe"), "Investor John Doe not found.");
            Assert.IsNotNull(investors.SingleOrDefault(i => i == "Samantha Sample"), "Investor Samantha Sample not found.");
            Assert.IsNotNull(investors.SingleOrDefault(i => i == "Tom Test"), "Investor Tom Test not found.");
        }

        [Test]
        public void GetSellAmountsPerInvestorTestTest()
        {
            double[] sellAmounts = SalesSummary.GetSellAmountsForInvestor("John Doe", TestData.GetFakeTransactions());
            Assert.AreEqual(2, sellAmounts.Length, "Incorrect number of sale prices");
            Assert.IsNotNull(sellAmounts.SingleOrDefault(i => i == 1), "Expected sale price not found.");
            Assert.IsNotNull(sellAmounts.SingleOrDefault(i => i == 2), "Expected sale price not found.");
            Assert.Throws<InvalidOperationException>(() => sellAmounts.Single(i => i == 3), "Price for buy transaction should not be included.");
        }

        [Test]
        public void GetSummaryForDateRangeTest()
        {
            SalesSummary salesSummary = new SalesSummary(TestData.GetFakeTransactions());
            var totalSold = salesSummary.GetTotalSoldForDateRangePerInvestor(DateTime.Now.Subtract(new TimeSpan(1, 5, 0, 0)));
            Assert.IsNotNull(totalSold, "Sales summary should not be null");
            Assert.IsNotEmpty(totalSold, "Sales summary should not be empty.");
            Assert.IsTrue(totalSold.TryGetValue("John Doe", out var johnDoeSales), "Should have gotten sales for John Doe");
            Assert.AreEqual(1, johnDoeSales, "Incorrect sales amount for John Doe.");
            Assert.IsTrue(totalSold.TryGetValue("Samantha Sample", out var samanthaSampleSales), "Should have gotten sales for Samantha Sample");
            Assert.AreEqual(1, samanthaSampleSales, "Incorrect sales amount for Samantha Sample.");
            Assert.IsTrue(totalSold.TryGetValue("Tom Test", out var tomTestSales), "Should have gotten sales for Tom Test");
            Assert.AreEqual(1, tomTestSales, "Incorrect sales amount for Tom Test.");
        }

        [Test]
        public void FilterTransactionsByDateTest()
        {
            SalesSummary salesSummary = new SalesSummary(TestData.GetFakeTransactions());
            DateTime startDate = DateTime.Now.Subtract(new TimeSpan(days: 2, hours: 0, minutes:0, seconds: 0));
            List<Transaction> transactions = salesSummary.FilterTransactionsByDate(startDate);
            Assert.IsFalse(transactions.Any(t => t.Date < startDate), "Should have returned only transactions with date greater than starting date.");
        }

        [TestCase(1, 1)]
        [TestCase(2, 1)]
        [TestCase(3, 1)]
        [TestCase(4, 4)]
        [TestCase(5, 4)]
        [TestCase(6, 4)]
        [TestCase(7, 7)]
        [TestCase(8, 7)]
        [TestCase(9, 7)]
        [TestCase(10, 10)]
        [TestCase(11, 10)]
        [TestCase(12, 10)]
        public void GetStartOfQuarterTest(int month, int expectedResult)
        {
            SalesSummary salesSummary = new SalesSummary(TestData.GetFakeTransactions());
            int startOfQuarter = salesSummary.GetStartOfQuarter(new DateTime(year: 2018, month: month, day: 15));
            Assert.AreEqual(expectedResult, startOfQuarter, $"Incorred quarter start date.");
        }
    }
}