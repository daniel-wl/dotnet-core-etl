using System;
using System.Collections.Generic;
using System.Linq;
using TransactionReportGenerator.Models;

namespace TransactionReportGenerator.BusinessLogic
{
    public class AssetsSummary : SummaryBase
    {
        public AssetsSummary(List<Transaction> transactions)
            : base (transactions)
        {
        }

        public override string PrintToString()
        {
            throw new NotImplementedException();
        }

        internal static string [] GetUniqueSalesReps(List<Transaction> transactions)
        {
            return transactions.Select(t => t.SalesRepresentative).Distinct().ToArray();
        }

        internal static Dictionary<string, List<string>> GetInvestorsByReps(List<Transaction> transactions)
        {
            Dictionary<string, List<string>> investorsByReps = new Dictionary<string, List<string>>();
            foreach(string salesRep in GetUniqueSalesReps(transactions))
            {
                investorsByReps.Add(salesRep, transactions.Where(t => t.SalesRepresentative == salesRep).Select(t => t.Investor).Distinct().ToList());
            }

            return investorsByReps;
        }
    }
}