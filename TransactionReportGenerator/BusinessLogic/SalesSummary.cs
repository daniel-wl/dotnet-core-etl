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

        internal Dictionary<string, double> GetTotalSoldForDateRangePerInvestor(DateTime startDate)
        {
            List<Transaction> filteredTransactions = FilterTransactionsByDate(startDate);
            string[] investors = GetUniqueInvestors(filteredTransactions);

            Dictionary<string, double> totalSoldPerInvestor = new Dictionary<string, double>();
            foreach(string investor in investors)
            {
                double totalSold = GetSellAmountsForInvestor(investor, filteredTransactions).Sum();
                totalSoldPerInvestor.Add(investor, totalSold);
            }

            return totalSoldPerInvestor;
        }

        internal static string[] GetUniqueInvestors(List<Transaction> transactions)
        {
            return transactions.Select(t => t.Investor).Distinct().ToArray();
        }

        internal static double[] GetSellAmountsForInvestor(string investor, List<Transaction> transactions)
        {
            return transactions.Where(t => t.Investor == investor && t.TransactionType == TransactionType.Sell).Select(t => t.Price).ToArray();
        }

        internal List<Transaction> FilterTransactionsByDate(DateTime startDate)
        {
            return Transactions.Where(t => t.Date >= startDate).ToList();
        }
    }
}