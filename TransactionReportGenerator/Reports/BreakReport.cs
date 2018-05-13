using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TransactionReportGenerator.Models;

namespace TransactionReportGenerator.Reports
{
    public class BreakReport : ReportBase
    {
        public BreakReport(List<Transaction> transactions)
            : base(transactions)
        {
        }

        public override string PrintToString()
        {
            StringBuilder output = new StringBuilder();

            output.Append("*********** Break Report ***********");
            
            foreach(var investor in Transactions.Select(t => t.Investor).Distinct())
            {
                output.AppendLine();
                output.AppendLine($"/// Break Report for investor '{investor}'");
                double cashBalance = GetCashBalanceForInvestor(investor);
                if(cashBalance < 0)
                {
                    output.AppendLine($"* Investor '{investor}' has negative cash balance of ${cashBalance}");
                }

                var fundsWithNegativeShareBalance = GetFundsWithNegativeShareBalanceForInvestor(investor);
                if(fundsWithNegativeShareBalance.Any())
                {
                    output.AppendLine($"* Investor {investor} has negative share balance in the following accounts:");
                    foreach(var fund in fundsWithNegativeShareBalance)
                    {
                        output.AppendLine($"  * Fund {fund.Key} has share balance of {fund.Value}.");
                    }
                }
            }

            return output.ToString();
        }

        internal double GetCashBalanceForInvestor(string investor)
        {
            return new AssetReport(Transactions).GetNetAmountHeldForInvestor(investor);
        }

        internal Dictionary<string, double> GetFundsWithNegativeShareBalanceForInvestor(string investor)
        {
            Dictionary<string, double> fundsWithNegativeShareBalances = new Dictionary<string, double>();
            foreach(string fund in GetFundsPerInvestor(investor))
            {
                double shareBalance = GetShareBalanceForFundByInvestor(fund, investor);
                if(shareBalance < 0)
                {
                    fundsWithNegativeShareBalances.Add(fund, shareBalance);
                }
            }

            return fundsWithNegativeShareBalances;
        }

        internal double GetShareBalanceForFundByInvestor(string fund, string investor)
        {
            List<Transaction> transactions = GetTransactionsForFundByInvestor(fund, investor);
            double sharesBought = transactions.Where(t => t.TransactionType == TransactionType.Buy).Select(t => t.Shares).Sum();
            double sharesSold = transactions.Where(t => t.TransactionType == TransactionType.Sell).Select(t => t.Shares).Sum();
            return sharesBought - sharesSold;
        }
    }
}