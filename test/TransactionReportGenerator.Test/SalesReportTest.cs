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
            new SalesReport().GetUniqueInvestors(new TransactionFactory().Build(3))
                .Should().HaveCount(3);
        }

        [Test]
        public void GetSellAmountsPerInvestorTest()
        {
            var factory = new TransactionFactory();
            var numberOfTransactions = Faker.Random.Int(1, 100);
            var allTransactions = factory.Build(numberOfTransactions);
            var transactionsForInvestor1 = factory
                .WithPrice(() => 100)
                .WithShares(() => 1)
                .WithTransactionType(() => TransactionType.Sell)
                .WithInvestor(() => "Investor 1")
                .WithTransactionType(() => TransactionType.Sell)
                .Build(numberOfTransactions);
            allTransactions.AddRange(transactionsForInvestor1);
            var result = new SalesReport().GetSellAmountForInvestor("Investor 1", allTransactions);
            result.Should().Be(numberOfTransactions * 100);
        }

        [Test]
        public void GetSummaryForDateRangeTest()
        {
            var allTransactions = new List<Transaction>();
            var numberOfTransactions = 10;
            var factory = new TransactionFactory()
                .WithInvestor(() => "Investor 1")
                .WithDate(() => DateTime.Now)
                .WithPrice(() => 100)
                .WithShares(() => 1)
                .WithTransactionType(() => TransactionType.Sell);
            allTransactions.AddRange(factory.Build(numberOfTransactions));

            allTransactions.AddRange(factory
                .WithInvestor(() => "Investor 2")
                .WithDate(() => DateTime.Now)
                .WithPrice(() => 100)
                .WithShares(() => 1)
                .WithTransactionType(() => TransactionType.Sell)
                .Build(numberOfTransactions));

            allTransactions.AddRange(factory
                .WithInvestor(() => "Investor 3")
                .WithDate(() => DateTime.Now.Subtract(new TimeSpan(10, 0, 0)))
                .WithTransactionType(() => TransactionType.Sell)
                .Build(numberOfTransactions));

            var result = new SalesReport(allTransactions).GetTotalSoldForDateRangePerInvestor(DateTime.Now.Subtract(new TimeSpan(5, 0, 0)));
            result.Select(kvp => kvp.Key).Should().NotContain("Investor 3")
                .And.Contain("Investor 1")
                .And.Contain("Investor 2");

            result["Investor 1"].Should().Be(numberOfTransactions * 100);
            result["Investor 2"].Should().Be(numberOfTransactions * 100);
        }

        [Test]
        public void FilterTransactionsByDateTest()
        {
            var factory = new TransactionFactory().WithDate(() => DateTime.Now);
            var transactionsFromToday = factory.Build(3);
            var transactionsFromFiveDaysAgo = factory.WithDate(() => Faker.Date.Recent(5)).Build(3);
            var allTransactions = new List<Transaction>();
            allTransactions.AddRange(transactionsFromToday);
            allTransactions.AddRange(transactionsFromFiveDaysAgo);

            var twoDaysAgo = DateTime.Now.Subtract(new TimeSpan(2, 0, 0, 0));
            var result = new SalesReport(allTransactions).FilterTransactionsByDate(twoDaysAgo)
                .Select(t => t.Date).Should().OnlyContain(d => d >= twoDaysAgo);
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
            new SalesReport().GetStartOfQuarter(new DateTime(year: 2018, month: month, day: 15))
                .Should().Be(expectedResult);
        }
    }
}
