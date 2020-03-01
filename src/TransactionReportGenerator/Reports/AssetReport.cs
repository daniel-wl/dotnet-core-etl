using System.Collections.Generic;
using System.Linq;
using System.Text;
using TransactionReportGenerator.Models;

namespace TransactionReportGenerator.Reports
{
    public class AssetReport : ReportBase
    {
        public AssetReport(IEnumerable<Transaction> transactions) : base(transactions) { }

        public override string PrintToString()
        {
            var investorsByReps = GetInvestorsForReps();
            var output = new StringBuilder();

            output.Append("********** Assets Summary **********");

            foreach (var investorListForSalesRep in investorsByReps)
            {
                output.AppendLine();
                output.AppendLine($"Assets held by investors for sales rep {investorListForSalesRep.Key}:");
                foreach (var investor in investorListForSalesRep.Value)
                {
                    output.AppendLine($"* Investor {investor}: ${GetNetAmountHeldForInvestor(investor)}");
                }
            }

            return output.ToString();
        }

        internal Dictionary<string, IEnumerable<string>> GetInvestorsForReps() =>
            GetUniqueSalesReps().ToDictionary(
                salesRep => salesRep,
                salesRep => Transactions.Where(t => t.SalesRepresentative == salesRep).Select(t => t.Investor).Distinct());

        internal double GetNetAmountHeldForInvestor(string investor)
        {
            var transactionsForInvestor = Transactions.Where(t => t.Investor == investor).ToArray();
            var totalBought = transactionsForInvestor.Where(t => t.TransactionType == TransactionType.Buy).Select(t => t.Price).Sum();
            var totalSold = transactionsForInvestor.Where(t => t.TransactionType == TransactionType.Sell).Select(t => t.Price).Sum();
            return totalSold - totalBought;
        }

        internal IEnumerable<string> GetUniqueSalesReps() =>
            Transactions.Select(t => t.SalesRepresentative).Distinct();
    }
}
