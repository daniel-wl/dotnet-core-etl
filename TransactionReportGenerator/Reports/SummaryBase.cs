using System;
using System.Collections.Generic;
using System.Linq;
using TransactionReportGenerator.Models;

namespace TransactionReportGenerator.Reports
{
    public abstract class ReportBase
    {
        protected readonly List<Transaction> Transactions;
        
        protected ReportBase(List<Transaction> transactions)
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

        internal string[] GetUniqueInvestors(List<Transaction> transactions)
        {
            return transactions.Select(t => t.Investor).Distinct().ToArray();
        }

        internal double[] GetSellAmountsForInvestor(string investor, List<Transaction> transactions)
        {
            return transactions.Where(t => t.Investor == investor && t.TransactionType == TransactionType.Sell).Select(t => t.Price).ToArray();
        }

        internal List<Transaction> FilterTransactionsByDate(DateTime startDate)
        {
            return Transactions.Where(t => t.Date >= startDate).ToList();
        }

        internal string [] GetUniqueSalesReps()
        {
            return Transactions.Select(t => t.SalesRepresentative).Distinct().ToArray();
        }
    }
}