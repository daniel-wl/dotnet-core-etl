using FluentAssertions;
using NUnit.Framework;
using TransactionReportGenerator.Models;
using TransactionReportGenerator.Reports;

namespace TransactionReportGenerator.Test
{
    public class BreakReportTest
    {
        [Test]
        public void GetTransactionsForFundByInvestor()
        {
            var factory = new TransactionFactory().WithFund(() => "Fund 1").WithInvestor(() => "Investor 1");
            var transactions = factory.Build(3);
            transactions.Add(factory.WithFund(() => "Fund 2").Build());
            transactions.Add(factory.WithInvestor(() => "Investor 2").Build());
            new BreakReport(transactions).GetTransactionsForFundByInvestor("Fund 1", "Investor 1")
                .Should().OnlyContain(t => t.Fund == "Fund 1" && t.Investor == "Investor 1");
        }

        [Test]
        public void GetShareBalanceForFundByInvestorTest()
        {
            var factory = new TransactionFactory()
                .WithFund(() => "Fund 1")
                .WithInvestor(() => "Investor 1")
                .WithShares(() => 100)
                .WithTransactionType(() => TransactionType.Buy);
            var transactions = factory.Build(3);
            transactions.Add(factory.WithTransactionType(() => TransactionType.Sell).Build());
            new BreakReport(transactions).GetShareBalanceForFundByInvestor("Fund 1", "Investor 1")
                .Should().Be(200);
        }

        [Test]
        public void GetFundsWithNegativeShareBalanceForInvestorTest()
        {
            var factory = new TransactionFactory()
                .WithFund(() => "Fund 1")
                .WithInvestor(() => "Investor 1")
                .WithShares(() => 100)
                .WithTransactionType(() => TransactionType.Sell);
            var transactions = factory.Build(3);
            transactions.AddRange(
                factory.WithFund(() => "Fund 2").WithTransactionType(() => TransactionType.Buy).Build(3));
            new BreakReport(transactions).GetFundsWithNegativeShareBalanceForInvestor("Investor 1")
                .Should().NotContainKey("Fund 2")
                .And.ContainKey("Fund 1")
                .WhichValue.Should().Be(-300);
        }
    }
}
