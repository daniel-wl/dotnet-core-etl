using System;
using System.Collections.Generic;
using TransactionReportGenerator.Models;

namespace TransactionReportGenerator.Test
{
    public static class Fakes
    {
        public static List<Transaction> GetFakeTransactions()
        {
            return new List<Transaction>
            {
                new Transaction
                {
                    Investor = "John Doe",
                    Price = 1,
                    TransactionType = TransactionType.Sell,
                    Date = DateTime.Now.Subtract(new TimeSpan(days: 1, hours: 0, minutes:0, seconds: 0)),
                    SalesRepresentative = "Sally"
                },
                new Transaction
                {
                    Investor = "Samantha Sample",
                    Price = 1,
                    TransactionType = TransactionType.Sell,
                    Date = DateTime.Now.Subtract(new TimeSpan(days: 1, hours: 0, minutes:0, seconds: 0)),
                    SalesRepresentative = "Bob"               
                },
                new Transaction
                {
                    Investor = "John Doe",
                    Price = 2,
                    TransactionType = TransactionType.Sell,
                    Date = DateTime.Now.Subtract(new TimeSpan(days: 2, hours: 0, minutes:0, seconds: 0)),
                    SalesRepresentative = "Sally"               
                },
                new Transaction
                {
                    Investor = "John Doe",
                    Price = 3,
                    TransactionType = TransactionType.Buy,
                    Date = DateTime.Now.Subtract(new TimeSpan(days: 3, hours: 0, minutes:0, seconds: 0)),
                    SalesRepresentative = "Sally"                
                },
                new Transaction
                {
                    Investor = "Tom Test",
                    Price = 1,
                    TransactionType = TransactionType.Sell,
                    Date = DateTime.Now.Subtract(new TimeSpan(days: 1, hours: 0, minutes:0, seconds: 0)),
                    SalesRepresentative = "Sally"                    
                }
            };
        }
    }
}