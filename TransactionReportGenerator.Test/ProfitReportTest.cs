using System.Collections.Generic;
using NUnit.Framework;
using TransactionReportGenerator.Reports;

namespace TransactionReportGenerator.Test
{
    public class ProfitReportTest
    {
        [Test]
        public void GetNetCashBalancePerFundByInvestorTest()
        {
            ProfitReport profitReport = new ProfitReport(TestData.GetFakeTransactions());
            Dictionary<string, double> netCashBalance = profitReport.GetNetCashBalancePerFundByInvestor("John Doe");
            Assert.AreEqual(2, netCashBalance.Count, "John Doe should have cash balances for two funds.");
            Assert.AreEqual(-2, netCashBalance["Fund 1"]);
            Assert.AreEqual(2, netCashBalance["Fund 2"]);
        }
    }
}