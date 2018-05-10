using System;

namespace TransactionReportGenerator.Models
{
    public class Transaction
    {
        public DateTime Date {get;set;}
        public string TransactionType {get;set;}
        public string Shares {get;set;}
        public string Price {get;set;}
        public string Fund {get;set;}
        public string Investor {get;set;}
        public string SalesRepresentative {get;set;}
    }
}