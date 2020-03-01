using System.Collections.Generic;
using System.Linq;
using System.Text;
using TransactionReportGenerator.Models;

namespace TransactionReportGenerator.Reports
{
    public class ProfitReport : ReportBase
    {
        public ProfitReport(IEnumerable<Transaction> transactions) : base(transactions) { }

        public override string PrintToString()
        {
            StringBuilder output = new StringBuilder();

            output.Append("********** Profit  Report **********");
            foreach (var investor in GetUniqueInvestors(Transactions))
            {
                var netCashBalancePerFund = GetNetCashBalancePerFundByInvestor(investor);
                if (netCashBalancePerFund.Any())
                {
                    output.AppendLine();
                    output.AppendLine($"Profit per fund for investor '{investor}':");
                    foreach (var cashBalanceForFund in netCashBalancePerFund)
                    {
                        output.AppendLine($"Fund {cashBalanceForFund.Key} {PrintProfitOrLoss(cashBalanceForFund.Value)}.");
                    }
                }
            }

            return output.ToString();
        }

        private string PrintProfitOrLoss(double balance) =>
            balance >= 0 ? $"made ${balance}" : $"lost ${balance * -1}";

        internal Dictionary<string, double> GetNetCashBalancePerFundByInvestor(string investor) =>
            GetFundsForInvestor(investor).ToDictionary(fund => fund, fund => GetCashBalanceForFundByInvestor(fund, investor));

        internal IEnumerable<string> GetFundsForInvestor(string investor) =>
            Transactions.Where(t => t.Investor == investor).Select(t => t.Fund).Distinct();

        internal double GetCashBalanceForFundByInvestor(string fund, string investor)
        {
            var transactions = Transactions.Where(t => t.Fund == fund && t.Investor == investor).ToArray();
            var totalBought = transactions.Where(t => t.TransactionType == TransactionType.Buy).Select(t => t.Price * t.Shares).Sum();
            var totalSold = transactions.Where(t => t.TransactionType == TransactionType.Sell).Select(t => t.Price * t.Shares).Sum();
            return totalSold - totalBought;
        }
    }
}
