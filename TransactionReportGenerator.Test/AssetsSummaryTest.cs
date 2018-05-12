using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TransactionReportGenerator.BusinessLogic;

namespace TransactionReportGenerator.Test
{
    public class AssetsSummaryTest
    {
        [Test]
        public void GetUniqueSalesRepsTest()
        {
            string[] salesReps = AssetsSummary.GetUniqueSalesReps(Fakes.GetFakeTransactions());
            Assert.AreEqual(salesReps.Length, 2, "Incorrect number of unique investors");
            Assert.IsNotNull(salesReps.SingleOrDefault(i => i == "Sally"), "Sales rep Sally not found.");
            Assert.IsNotNull(salesReps.SingleOrDefault(i => i == "Bob"), "Sales rep Bob not found.");
        }

        [Test]
        public void GetInvestorsByRepTest()
        {
            Dictionary<string, List<string>> investorsByRep = AssetsSummary.GetInvestorsByReps(Fakes.GetFakeTransactions());
            Assert.IsNotNull(investorsByRep, "GetInvestorsByReps should not return null.");
            Assert.IsNotEmpty(investorsByRep, "GetInvestorsByReps should not return empty collection for non-empty transaction list");
            Assert.AreEqual(2, investorsByRep.Count, "There are only two reps.");
            Assert.IsTrue(investorsByRep.TryGetValue("Sally", out var investorsForSally), "Should have found investors for Sally");
            Assert.IsTrue(investorsByRep.TryGetValue("Bob", out var investorsForBob), "Should have found investors for Bob");
            Assert.AreEqual(2, investorsForSally.Count, "Sally should have 2 investors");
            Assert.AreEqual(1, investorsForBob.Count, "Bob should have one investor");
            Assert.IsNotNull(investorsForSally.SingleOrDefault(i => i == "John Doe"), "Should have found investor John Doe for rep Sally");
            Assert.IsNotNull(investorsForSally.SingleOrDefault(i => i == "Tom Test"), "Should have found investor Tom Test for rep Sally");
            Assert.IsNotNull(investorsForBob.SingleOrDefault(i => i == "Samantha Sample"), "Should have found investor SamanthaSample for rep Bob");
        }

        [TestCase("John Doe", 0)]
        [TestCase("Samantha Sample", 1)]
        [TestCase("Tom Test", 1)]
        public void GetNetAmountHeldForInvestorTest(string investor, double expectedResult)
        {
            AssetsSummary assetsSummary = new AssetsSummary(Fakes.GetFakeTransactions());
            double netAmountHeld = assetsSummary.GetNetAmountHeldForInvestor(investor);
            Assert.AreEqual(expectedResult, netAmountHeld, $"Incorrect amount held for investor {investor}.");
        }
    }
}