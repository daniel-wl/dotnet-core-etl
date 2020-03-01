using System.Collections.Generic;
using System.Linq;
using System.Text;
using TransactionReportGenerator.Core.Models;

namespace TransactionReportGenerator.Core.Reports
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

        internal IEnumerable<string> GetUniqueSalesReps() =>
            Transactions.Select(t => t.SalesRepresentative).Distinct();
    }
}
