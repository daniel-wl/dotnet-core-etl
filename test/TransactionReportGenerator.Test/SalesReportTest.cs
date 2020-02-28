using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using TransactionReportGenerator.Models;
using TransactionReportGenerator.Reports;

namespace TransactionReportGenerator.Test
{
    public class SalesReportTest
    {
        public static Faker Faker = new Faker();

        [Test]
        public void CreateSalesSummaryWithNullTransactionsTest()
        {
            Action createSalesReportWithNullTransactionsList = () => new SalesReport(null);
            createSalesReportWithNullTransactionsList.Should().Throw<ArgumentNullException>("creating a sales summary with null transactions should throw");
        }

        [Test]
        public void CreateSalesSummaryWithEmptyTransactionsTest()
        {
            Action createSalesReportWithEmptyTransactionsList = () => new SalesReport(new List<Transaction>());
            createSalesReportWithEmptyTransactionsList.Should().Throw<ArgumentException>("creating a sales summary with empty transactions should throw");
        }

        [Test]
        public void CreateSalesSummaryWithTransactionsTest()
        {
            Action createSalesReportWithNonEmptyTransactionsList = () => new SalesReport(new TransactionFactory().Build(3));
            createSalesReportWithNonEmptyTransactionsList.Should().NotThrow("creating a sales summary with a non-empty transaction list should not throw.");
        }

        [Test]
        public void GetUniqueInvestorsTest()
        {
            SalesReport salesReport = Mock.Of<SalesReport>();
            int numberOfTransactions = Faker.Random.Int(1, 100);
            string[] result = salesReport.GetUniqueInvestors(new TransactionFactory().Build(numberOfTransactions));
            result.Distinct().Should().HaveCount(numberOfTransactions);
        }

        [Test]
        public void GetSellAmountsPerInvestorTestTest()
        {
            SalesReport salesReport = Mock.Of<SalesReport>();
            int numberOfTransactions = Faker.Random.Int(1, 100);
            List<Transaction> transactions = new TransactionFactory()
                .WithPrice(() => numberOfTransactions)
                .WithInvestor(() => "John Doe")
                .WithTransactionType(() => TransactionType.Sell)
                .Build(numberOfTransactions);
            double[] result = salesReport.GetSellAmountsForInvestor("John Doe", transactions);
            result.Should().HaveCount(numberOfTransactions);
            Enumerable.Sum(result).Should().Be(Enumerable.Sum(transactions.Select(t => t.Price)));
        }

        [Test]
        public void GetSummaryForDateRangeTest()
        {
            SalesReport salesSummary = new SalesReport(TestData.GetFakeTransactions());
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
            SalesReport salesSummary = new SalesReport(TestData.GetFakeTransactions());
            DateTime startDate = DateTime.Now.Subtract(new TimeSpan(days: 2, hours: 0, minutes: 0, seconds: 0));
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
            SalesReport salesSummary = new SalesReport(TestData.GetFakeTransactions());
            int startOfQuarter = salesSummary.GetStartOfQuarter(new DateTime(year: 2018, month: month, day: 15));
            Assert.AreEqual(expectedResult, startOfQuarter, $"Incorred quarter start date.");
        }
    }
}
