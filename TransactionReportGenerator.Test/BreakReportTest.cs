using System.Linq;
using NUnit.Framework;
using TransactionReportGenerator.BusinessLogic;

namespace TransactionReportGenerator.Test
{
    public class BreakReportTest
    {
        [Test]
        public void GetFundsPerInvestorTest()
        {
            BreakReport breakReport = new BreakReport(TestData.GetFakeTransactionsWithNegativeShareBalances());
            var funds = breakReport.GetFundsPerInvestor("Investor A");
            Assert.AreEqual(2, funds.Count, "Wrong number of funds retrieved for investor");
            Assert.IsNotNull(funds.SingleOrDefault(f => f == "Fund 1"), "Didn't return Fund 1 as expected.");
            Assert.IsNotNull(funds.SingleOrDefault(f => f == "Fund 2"), "Didn't return Fund 2 as expected.");
        }

        [Test]
        public void GetTransactionsForFundByInvestorTest()
        {
            BreakReport breakReport = new BreakReport(TestData.GetFakeTransactionsWithNegativeShareBalances());
            var transactions = breakReport.GetTransactionsForFundByInvestor("Fund 1", "Investor A");
            Assert.AreEqual(3, transactions.Count, "Wrong number of transactions retrieved for investor");
            Assert.IsFalse(transactions.Any(f => f.Investor != "Investor A"), "Should have retrieved transactions only for Investor A");
            Assert.IsFalse(transactions.Any(f => f.Fund != "Fund 1"), "Should have retrieved transactions only for Fund 1");
        }

        [Test]
        public void GetShareBalanceForFundByInvestorTest()
        {
            BreakReport breakReport = new BreakReport(TestData.GetFakeTransactionsWithNegativeShareBalances());
            double shares = breakReport.GetShareBalanceForFundByInvestor("Fund 1", "Investor A");
            Assert.AreEqual(-10, shares, "Report should show that more shares were sold than were bought.");
        }

        [Test]
        public void GetFundsWithNegativeShareBalanceForInvestorTest()
        {
            BreakReport breakReport = new BreakReport(TestData.GetFakeTransactionsWithNegativeShareBalances());
            var funds = breakReport.GetFundsWithNegativeShareBalanceForInvestor("Investor A");
            Assert.AreEqual(1, funds.Count, "Should have found 1 fund with negative share balance");
            Assert.DoesNotThrow(() => funds.Single(f => f.Key == "Fund 1"), "Should have returned Fund 1.");
            Assert.AreEqual(-10, funds.Single(f => f.Key == "Fund 1").Value, "Fund 1 balance is incorrect");
        }
    }
}