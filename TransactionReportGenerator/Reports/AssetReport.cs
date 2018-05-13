using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TransactionReportGenerator.Models;

namespace TransactionReportGenerator.Reports
{
    public class AssetReport : ReportBase
    {
        public AssetReport(List<Transaction> transactions)
            : base (transactions)
        {
        }

        public override string PrintToString()
        {
            Dictionary<string, List<string>> investorsByReps = GetInvestorsByReps();            
            StringBuilder output = new StringBuilder();

            output.Append("********** Assets Summary **********");
            
            foreach(var investorListForSalesRep in investorsByReps)
            {
                output.AppendLine();
                output.AppendLine($"Assets held by investors for sales rep {investorListForSalesRep.Key}:");
                foreach(var investor in investorListForSalesRep.Value)
                {
                    output.AppendLine($"* Investor {investor}: ${GetNetAmountHeldForInvestor(investor)}");
                }
            }

            return output.ToString();
        }

        internal Dictionary<string, List<string>> GetInvestorsByReps()
        {
            Dictionary<string, List<string>> investorsByReps = new Dictionary<string, List<string>>();
            foreach(string salesRep in GetUniqueSalesReps())
            {
                investorsByReps.Add(salesRep, Transactions.Where(t => t.SalesRepresentative == salesRep).Select(t => t.Investor).Distinct().ToList());
            }

            return investorsByReps;
        }

        internal double GetNetAmountHeldForInvestor(string investor)
        {
            List<Transaction> transactionsForInvestor = Transactions.Where(t => t.Investor == investor).ToList();
            double totalBought = transactionsForInvestor.Where(t => t.TransactionType ==  TransactionType.Buy).Select(t => t.Price).Sum();
            double totalSold = transactionsForInvestor.Where(t => t.TransactionType ==  TransactionType.Sell).Select(t => t.Price).Sum();
            return totalSold - totalBought;
        }
    }
}