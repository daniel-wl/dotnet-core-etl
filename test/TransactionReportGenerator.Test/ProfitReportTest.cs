using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using TransactionReportGenerator.Core.Models;
using TransactionReportGenerator.Core.Reports;

namespace TransactionReportGenerator.Test
{
    public class ProfitReportTest
    {
        [Test]
        public void GetFundsForInvestorTest()
        {
            var factory = new TransactionFactory().WithInvestor(() => "Investor 1")
                .WithFund(() => "Fund 1");
            var transactions = factory.Build(3);
            transactions[0].Fund = "Fund 2";

            new ProfitReport(transactions).GetFundsForInvestor("Investor 1")
                .Should().Contain("Fund 1").And.Contain("Fund 2");
        }

        [Test]
        public void GetCashBalanceForFundByInvestorTest()
        {
            var factory = new TransactionFactory().WithInvestor(() => "Investor 1")
                .WithFund(() => "Fund 1")
                .WithPrice(() => 100)
                .WithShares(() => 1)
                .WithTransactionType(() => TransactionType.Sell);
            var transactions = factory.Build(3);
            transactions[0].TransactionType = TransactionType.Buy;

            new ProfitReport(transactions).GetCashBalanceForFundByInvestor("Fund 1", "Investor 1")
                .Should().Be(100);
        }

        [Test]
        public void GetNetCashBalancePerFundByInvestorTest()
        {
            var factory = new TransactionFactory().WithInvestor(() => "Investor 1")
                .WithFund(() => "Fund 1")
                .WithPrice(() => 100)
                .WithShares(() => 1)
                .WithTransactionType(() => TransactionType.Sell);
            var transactionsForInvestor1 = factory.Build(3);
            transactionsForInvestor1[0].Fund = "Fund 2";

            var transactionsForInvestor2 = factory
                .WithInvestor(() => "Investor 2")
                .WithFund(() => "Fund 3")
                .WithShares(() => 1)
                .WithPrice(() => 100)
                .WithTransactionType(() => TransactionType.Sell)
                .Build(3);

            var allTransactions = new List<Transaction>();
            allTransactions.AddRange(transactionsForInvestor1);
            allTransactions.AddRange(transactionsForInvestor2);

            var subject = new ProfitReport(allTransactions);
            var profit = subject.GetNetCashBalancePerFundByInvestor("Investor 1");
            profit.Should().ContainKey("Fund 1").And.ContainKey("Fund 2").And.NotContainKey("Fund 3");
            profit["Fund 1"].Should().Be(200);
            profit["Fund 2"].Should().Be(100);
        }
    }
}
