using System;
using System.Collections.Generic;
using System.Linq;
using TransactionReportGenerator.Core.Models;

namespace TransactionReportGenerator.Core.Reports
{
    public abstract class ReportBase
    {
        protected readonly IEnumerable<Transaction> Transactions;

        protected ReportBase() { }

        protected ReportBase(IEnumerable<Transaction> transactions)
        {
            if (transactions == null)
            {
                throw new ArgumentNullException(nameof(transactions));
            }

            if (!transactions.Any())
            {
                throw new ArgumentException("Must have at least one transaction.", nameof(transactions));
            }

            Transactions = transactions;
        }

        public abstract string PrintToString();

        internal IEnumerable<string> GetUniqueInvestors(IEnumerable<Transaction> transactions) =>
            transactions.Select(t => t.Investor).Distinct();

        internal IEnumerable<string> GetFundsForInvestor(string investor) =>
            Transactions.Where(t => t.Investor == investor).Select(t => t.Fund).Distinct();

        internal double GetNetAmountHeldForInvestor(string investor)
        {
            var transactionsForInvestor = Transactions.Where(t => t.Investor == investor).ToArray();
            var totalBought = transactionsForInvestor.Where(t => t.TransactionType == TransactionType.Buy).Select(t => t.Price).Sum();
            var totalSold = transactionsForInvestor.Where(t => t.TransactionType == TransactionType.Sell).Select(t => t.Price).Sum();
            return totalSold - totalBought;
        }
    }
}
