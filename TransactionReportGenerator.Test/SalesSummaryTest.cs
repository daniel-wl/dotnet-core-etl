using TransactionReportGenerator.BusinessLogic;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using TransactionReportGenerator.Models;
using System.Linq;

namespace TransactionReportGenerator.Test
{
    public class SalesSummaryTest
    {
        [Test]
        public void CreateSalesSummaryWithNullTransactionsTest()
        {
            Assert.Throws<ArgumentNullException>(() => new SalesSummary(null), "Creating a sales summary with null transactions should throw");
        }

        [Test]
        public void CreateSalesSummaryWithEmptyTransactionsTest()
        {
            Assert.Throws<ArgumentException>(() => new SalesSummary(new List<Transaction>()), "Creating a sales summary with empty transactions should throw");
        }

        [Test]
        public void CreateSalesSummaryWithTransactionsTest()
        {
            Assert.DoesNotThrow(() => new SalesSummary(new List<Transaction>(GetFakeTransactions())), "Creating a sales summary with a non-empty transaction list should not throw.");
        }

        [Test]
        public void GetUniqueInvestorsTest()
        {
            string[] investors = SalesSummary.GetUniqueInvestors(GetFakeTransactions());
            Assert.AreEqual(investors.Length, 3, "Incorrect number of unique investors");
            Assert.IsNotNull(investors.SingleOrDefault(i => i == "John Doe"), "Investor John Doe not found.");
            Assert.IsNotNull(investors.SingleOrDefault(i => i == "Samantha Sample"), "Investor Samantha Sample not found.");
            Assert.IsNotNull(investors.SingleOrDefault(i => i == "Tom Test"), "Investor Tom Test not found.");
        }

        [Test]
        public void GetSellAmountsPerInvestorTestTest()
        {
            double[] sellAmounts = SalesSummary.GetSellAmountsForInvestor("John Doe", GetFakeTransactions());
            Assert.AreEqual(2, sellAmounts.Length, "Incorrect number of sale prices");
            Assert.IsNotNull(sellAmounts.SingleOrDefault(i => i == 1), "Expected sale price not found.");
            Assert.IsNotNull(sellAmounts.SingleOrDefault(i => i == 2), "Expected sale price not found.");
            Assert.Throws<InvalidOperationException>(() => sellAmounts.Single(i => i == 3), "Price for buy transaction should not be included.");
        }

        [Test]
        public void GetSummaryForDateRangeTest()
        {
            SalesSummary salesSummary = new SalesSummary(GetFakeTransactions());
            var totalSold = salesSummary.GetTotalSoldForDateRangePerInvestor(DateTime.Now.Subtract(new TimeSpan(2, 0, 0, 0)));
            Assert.IsNotNull(totalSold, "Sales summary should not be null");
            Assert.IsNotEmpty(totalSold, "Sales summary should not be empty.");
        }

        [Test]
        public void FilterTransactionsByDateTest()
        {
            SalesSummary salesSummary = new SalesSummary(GetFakeTransactions());
        }

        private List<Transaction> GetFakeTransactions()
        {
            return new List<Transaction>
            {
                new Transaction
                {
                    Investor = "John Doe",
                    Price = 1,
                    TransactionType = TransactionType.Sell,
                    Date = DateTime.Now.Subtract(new TimeSpan(days: 1, hours: 0, minutes:0, seconds: 0))
                },
                new Transaction
                {
                    Investor = "Samantha Sample",
                    Price = 1,
                    TransactionType = TransactionType.Sell,
                    Date = DateTime.Now.Subtract(new TimeSpan(days: 1, hours: 0, minutes:0, seconds: 0))                    
                },
                new Transaction
                {
                    Investor = "John Doe",
                    Price = 2,
                    TransactionType = TransactionType.Sell,
                    Date = DateTime.Now.Subtract(new TimeSpan(days: 2, hours: 0, minutes:0, seconds: 0))                    
                },
                new Transaction
                {
                    Investor = "John Doe",
                    Price = 3,
                    TransactionType = TransactionType.Buy,
                    Date = DateTime.Now.Subtract(new TimeSpan(days: 3, hours: 0, minutes:0, seconds: 0))                    
                },
                new Transaction
                {
                    Investor = "Tom Test",
                    Price = 1,
                    TransactionType = TransactionType.Sell,
                    Date = DateTime.Now.Subtract(new TimeSpan(days: 1, hours: 1, minutes:0, seconds: 0))                    
                }
            };
        }
    }
}