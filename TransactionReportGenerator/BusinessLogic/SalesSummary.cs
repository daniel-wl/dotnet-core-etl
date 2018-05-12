using System;
using System.Collections.Generic;
using System.Linq;
using TransactionReportGenerator.Models;

namespace TransactionReportGenerator.BusinessLogic
{
    public class SalesSummary
    {
        internal readonly List<Transaction> Transactions;
        
        public SalesSummary(List<Transaction> transactions)
        {
            if(transactions == null)
            {
                throw new ArgumentNullException(nameof(transactions));
            }

            if(!transactions.Any())
            {
                throw new ArgumentException("Must have at least one transaction.", nameof(transactions));
            }

            Transactions = transactions;
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