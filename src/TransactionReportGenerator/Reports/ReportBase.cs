using System;
using System.Collections.Generic;
using System.Linq;
using TransactionReportGenerator.Models;

namespace TransactionReportGenerator.Reports
{
    public abstract class ReportBase
    {
        protected readonly List<Transaction> Transactions;

        protected ReportBase() { }

        protected ReportBase(List<Transaction> transactions)
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

        internal List<string> GetFundsPerInvestor(string investor)
        {
            return Transactions.Where(t => t.Investor == investor).Select(t => t.Fund).Distinct().ToList();
        }

        internal string[] GetUniqueInvestors(List<Transaction> transactions)
        {
            return transactions.Select(t => t.Investor).Distinct().ToArray();
        }
    }
}
