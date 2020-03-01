using System.Collections.Generic;
using System.Linq;
using System.Text;
using TransactionReportGenerator.Core.Models;

namespace TransactionReportGenerator.Core.Reports
{
    public class BreakReport : ReportBase
    {
        public BreakReport(IEnumerable<Transaction> transactions) : base(transactions) { }

        public override string PrintToString()
        {
            var output = new StringBuilder();

            output.Append("*********** Break Report ***********");

            foreach (var investor in GetUniqueInvestors(Transactions))
            {
                output.AppendLine();
                output.AppendLine($"/// Break Report for investor '{investor}'");
                var cashBalance = GetNetAmountHeldForInvestor(investor);
                if (cashBalance < 0)
                {
                    output.AppendLine($"* Investor '{investor}' has negative cash balance of ${cashBalance}");
                }

                var fundsWithNegativeShareBalance = GetFundsWithNegativeShareBalanceForInvestor(investor);
                if (fundsWithNegativeShareBalance.Any())
                {
                    output.AppendLine($"* Investor {investor} has negative share balance in the following funds:");
                    foreach (var fund in fundsWithNegativeShareBalance)
                    {
                        output.AppendLine($"  * Fund {fund.Key} has share balance of {fund.Value}.");
                    }
                }
            }

            return output.ToString();
        }

        internal Dictionary<string, double> GetFundsWithNegativeShareBalanceForInvestor(string investor) =>
            GetFundsForInvestor(investor)
            .Select(fund => new { FundName = fund, ShareBalance = GetShareBalanceForFundByInvestor(fund, investor) })
            .Where(fund => fund.ShareBalance < 0)
            .ToDictionary(fund => fund.FundName, fund => fund.ShareBalance);

        internal double GetShareBalanceForFundByInvestor(string fund, string investor)
        {
            var transactions = GetTransactionsForFundByInvestor(fund, investor).ToArray();
            double sharesBought = transactions.Where(t => t.TransactionType == TransactionType.Buy).Select(t => t.Shares).Sum();
            double sharesSold = transactions.Where(t => t.TransactionType == TransactionType.Sell).Select(t => t.Shares).Sum();
            return sharesBought - sharesSold;
        }

        internal IEnumerable<Transaction> GetTransactionsForFundByInvestor(string fund, string investor) =>
            Transactions.Where(t => t.Fund == fund && t.Investor == investor);
    }
}
