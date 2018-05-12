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
    }
}