using FluentAssertions;
using NUnit.Framework;
using TransactionReportGenerator.Models;
using TransactionReportGenerator.Reports;

namespace TransactionReportGenerator.Test
{
    public class AssetReportTest
    {
        [Test]
        public void GetUniqueSalesRepsTest()
        {
            var factory = new TransactionFactory().WithSalesRep(() => "Rep 1");
            var transactions = factory.Build(3);
            transactions[0].SalesRepresentative = "Rep 2";
            new AssetReport(transactions).GetUniqueSalesReps()
                .Should().OnlyHaveUniqueItems().And.OnlyContain(rep => rep == "Rep 1" || rep == "Rep 2");
        }

        [Test]
        public void GetInvestorsForRepsTest()
        {
            var factory = new TransactionFactory().WithSalesRep(() => "Rep 1")
                .WithInvestor(() => "Investor 1");
            var transactions = factory.Build(3);
            transactions[0].Investor = "Investor 2";
            new AssetReport(transactions).GetInvestorsForReps()
                .Should().ContainKey("Rep 1").WhichValue
                .Should().OnlyHaveUniqueItems().And.OnlyContain(value => value == "Investor 1" || value == "Investor 2");
        }

        [Test]
        public void GetNetAmountHeldForInvestorTest()
        {
            var factory = new TransactionFactory().WithInvestor(() => "Investor 1")
                .WithFund(() => "Fund 1")
                .WithPrice(() => 100)
                .WithShares(() => 1)
                .WithTransactionType(() => TransactionType.Sell);
            var transactions = factory.Build(3);
            transactions.AddRange(factory.WithTransactionType(() => TransactionType.Buy).Build(1));
            transactions.AddRange(factory.WithFund(() => "Fund 2").Build(1));
            new AssetReport(transactions).GetNetAmountHeldForInvestor("Investor 1").Should().Be(100);
        }
    }
}
