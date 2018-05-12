using TransactionReportGenerator.BusinessLogic;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using TransactionReportGenerator.Models;

namespace TransactionReportGenerator.Test
{
    public class SalesSummaryTest
    {
        [Test]
        public void CreateSalesSummaryWithNullTransactions()
        {
            Assert.Throws<ArgumentNullException>(() => new SalesSummary(null), "Creating a sales summary with null transactions should throw");
        }

        [Test]
        public void CreateSalesSummaryWithEmptyTransactions()
        {
            Assert.Throws<ArgumentException>(() => new SalesSummary(new List<Transaction>()), "Creating a sales summary with empty transactions should throw");
        }
    }
}