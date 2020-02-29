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
            var salesReport = Mock.Of<SalesReport>();
            var numberOfTransactions = Faker.Random.Int(1, 5);
            var result = salesReport.GetUniqueInvestors(new TransactionFactory().Build(numberOfTransactions));
            result.Distinct().Should().HaveCount(numberOfTransactions);
        }

        [Test]
        public void GetSellAmountsPerInvestorTestTest()
        {
            var salesReport = Mock.Of<SalesReport>();
            var numberOfTransactions = Faker.Random.Int(1, 100);
            var transactions = new TransactionFactory()
                .WithPrice(() => numberOfTransactions)
                .WithInvestor(() => "John Doe")
                .WithTransactionType(() => TransactionType.Sell)
                .Build(numberOfTransactions);
            var result = salesReport.GetSellAmountsForInvestor("John Doe", transactions);
            result.Should().HaveCount(numberOfTransactions);
            Enumerable.Sum(result).Should().Be(Enumerable.Sum(transactions.Select(t => t.Price)));
        }

        [Test]
        public void GetSummaryForDateRangeTest()
        {
            var factory = new TransactionFactory().WithDate(() => DateTime.Now).WithSalesRep(() => "John Doe");
            var transactions = factory.Build(Faker.Random.Int(1, 100));
            transactions.Add(factory.WithSalesRep(() => "Sam Sample").WithDate(() => Faker.Date.Past(5)).Build());
            var salesReport = new SalesReport(transactions);
            var totalSold = salesReport.GetTotalSoldForDateRangePerInvestor(DateTime.Now.Subtract(new TimeSpan(1, 0, 0, 0)));
            totalSold.Should().HaveCount(transactions.Count - 1);
            totalSold.Should().NotContainKey("Sam Sample");
        }

        [Test]
        public void FilterTransactionsByDateTest()
        {
            var factory = new TransactionFactory().WithDate(() => Faker.Date.Recent(1));
            var transactions = factory.Build(3);
            transactions.AddRange(factory.WithDate(() => Faker.Date.Recent(2)).Build(3));
            transactions.AddRange(factory.WithDate(() => Faker.Date.Recent(3)).Build(3));
            var salesSummary = new SalesReport(transactions);
            var startDate = DateTime.Now.Subtract(new TimeSpan(2, 0, 0, 0));
            var result = salesSummary.FilterTransactionsByDate(startDate);
            result.Select(t => t.Date).Should().OnlyContain(t => t >= startDate);
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
            var salesSummary = new SalesReport(TestData.GetFakeTransactions());
            var startOfQuarter = salesSummary.GetStartOfQuarter(new DateTime(year: 2018, month: month, day: 15));
            startOfQuarter.Should().Be(expectedResult);
        }
    }
}
