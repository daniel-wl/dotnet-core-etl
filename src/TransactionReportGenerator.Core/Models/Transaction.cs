using System;

namespace TransactionReportGenerator.Core.Models
{
    public class Transaction
    {
        public DateTime Date { get; set; }
        public TransactionType TransactionType { get; set; }
        public double Shares { get; set; }
        public double Price { get; set; }
        public string Fund { get; set; }
        public string Investor { get; set; }
        public string SalesRepresentative { get; set; }
    }
}
