using System;
using System.Collections.Generic;
using System.Linq;
using TransactionReportGenerator.Models;

namespace TransactionReportGenerator.BusinessLogic
{
    public abstract class SummaryBase
    {
        internal readonly List<Transaction> Transactions;
        
        protected SummaryBase(List<Transaction> transactions)
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

        public abstract string PrintToString();

        internal List<Transaction> GetTransactionsForFundByInvestor(string fund, string investor)
        {
            return Transactions.Where(t => t.Fund == fund && t.Investor == investor).ToList();
        }

        internal List<string> GetFundsPerInvestor(string investor)
        {
            return Transactions.Where(t => t.Investor == investor).Select(t => t.Fund).Distinct().ToList();
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

        internal static string [] GetUniqueSalesReps(List<Transaction> transactions)
        {
            return transactions.Select(t => t.SalesRepresentative).Distinct().ToArray();
        }
    }
}