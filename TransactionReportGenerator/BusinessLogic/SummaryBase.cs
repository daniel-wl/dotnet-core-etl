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
    }
}