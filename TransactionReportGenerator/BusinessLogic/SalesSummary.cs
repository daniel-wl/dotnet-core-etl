using System;
using System.Collections.Generic;
using System.Linq;
using TransactionReportGenerator.Models;

namespace TransactionReportGenerator.BusinessLogic
{
    public class SalesSummary
    {
        internal readonly List<Transaction> Transactions;
        
        public SalesSummary(string csvFile)
        {
            if(string.IsNullOrWhiteSpace(csvFile))
            {
                throw new ArgumentException("Can't be be null or empty!", nameof(csvFile));
            }

            Transactions = TransactionLoader.LoadTransactions(csvFile);
        }

        public void GetInceptionToDateSummary()
        {
            string[] investors = Transactions.Select(t => t.Investor).Distinct().ToArray();
            Dictionary<string, double> totalSoldPerInvestor = new Dictionary<string, double>();
            foreach(string investor in investors)
            {
                Transactions.Where(t => t.TransactionType == TransactionType.Sell && t.Investor == investor).Select(t => t.Price);
            }
        }
    }
}