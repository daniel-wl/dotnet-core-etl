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
    }
}