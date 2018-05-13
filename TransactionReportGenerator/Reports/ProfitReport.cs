using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TransactionReportGenerator.Models;

namespace TransactionReportGenerator.Reports
{
    public class ProfitReport : ReportBase
    {
        public ProfitReport(List<Transaction> transactions)
            : base(transactions)
        {
        }

        public override string PrintToString()
        {            
            StringBuilder output = new StringBuilder();
            
            output.Append("********** Profit  Report **********");
            foreach(var investor in GetUniqueInvestors(Transactions))
            {
                var netCashBalancePerFund = GetNetCashBalancePerFundByInvestor(investor);
                if(netCashBalancePerFund.Any())
                {
                    output.AppendLine();
                    output.AppendLine($"Profit per fund for investor '{investor}':");
                    foreach(var cashBalanceForFund in netCashBalancePerFund)
                    {
                        output.AppendLine($"Fund {cashBalanceForFund.Key} {PrintProfitOrLoss(cashBalanceForFund.Value)}.");
                    }
                }
            }

            return output.ToString();
        }

        private string PrintProfitOrLoss(double balance)
        {
            if(balance > 0)
            {
                return $"made ${balance}";
            }
            else if (balance < 0)
            {
                return $"lost ${balance * -1}";
            }
            
            return "broke even";
        }

        internal Dictionary<string, double> GetNetCashBalancePerFundByInvestor(string investor)
        {
            Dictionary<string, double> cashBalances = new Dictionary<string, double>();
            List<string> funds = GetFundsPerInvestor(investor);
            foreach(var fund in funds)
            {
                double cashBalance = GetCashBalanceForFundAndInvestor(fund, investor);
                cashBalances.Add(fund, cashBalance);
            }
            return cashBalances;
        }

        internal double GetCashBalanceForFundAndInvestor(string fund, string investor)
        {
            List<Transaction> transactionsForInvestorAndFund = Transactions.Where(t => t.Fund == fund && t.Investor == investor).ToList();
            double totalBought = transactionsForInvestorAndFund.Where(t => t.TransactionType ==  TransactionType.Buy).Select(t => t.Price).Sum();
            double totalSold = transactionsForInvestorAndFund.Where(t => t.TransactionType ==  TransactionType.Sell).Select(t => t.Price).Sum();
            return totalSold - totalBought;
        }
    }
}