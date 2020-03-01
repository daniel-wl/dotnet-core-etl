using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using TransactionReportGenerator.Core.Models;
using TransactionReportGenerator.Core.Reports;

namespace TransactionReportGenerator.Test
{
    public class ReportBaseTest
    {
        [Test]
        public void CreateReportWithNullTransactionsTest()
        {
            Action createSalesReportWithNullTransactionsList = () => new SalesReport(null);
            createSalesReportWithNullTransactionsList.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void CreateReportWithEmptyTransactionsTest()
        {
            Action createSalesReportWithEmptyTransactionsList = () => new SalesReport(new List<Transaction>());
            createSalesReportWithEmptyTransactionsList.Should().Throw<ArgumentException>();
        }

        [Test]
        public void CreateReportWithTransactionsTest()
        {
            Action createSalesReportWithNonEmptyTransactionsList = () => new SalesReport(new TransactionFactory().Build(3));
            createSalesReportWithNonEmptyTransactionsList.Should().NotThrow();
        }

        [Test]
        public void GetUniqueInvestorsTest()
        {
            new SalesReport().GetUniqueInvestors(new TransactionFactory().Build(3))
                .Should().HaveCount(3);
        }
    }
}
