using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TransactionReportGenerator.Models;

namespace TransactionReportGenerator.Reports
{
    public class SalesReport : ReportBase
    {
        public SalesReport() { }

        public SalesReport(IEnumerable<Transaction> transactions) : base(transactions) { }

        public override string PrintToString()
        {
            var output = new StringBuilder();

            output.AppendLine("********** Sales  Summary **********");
            output.AppendLine("/// Year to date: ///");
            foreach (var investor in GetYearToDateSalesPerInvestor())
            {
                output.AppendLine($"Investor '{investor.Key}' sold ${investor.Value}.");
            }

            output.AppendLine();
            output.AppendLine("/// Quarter to date: ///");
            foreach (var investor in GetQuarterToDateSalesPerInvestor())
            {
                output.AppendLine($"Investor '{investor.Key}' sold ${investor.Value}.");
            }

            output.AppendLine();
            output.AppendLine("/// Month to date: ///");
            foreach (var investor in GetMonthToDateSalesPerInvestor())
            {
                output.AppendLine($"Investor '{investor.Key}' sold ${investor.Value}.");
            }

            output.AppendLine();
            output.AppendLine("/// Inception to date: ///");
            foreach (var investor in GetInceptionToDateSalesPerInvestor())
            {
                output.AppendLine($"Investor '{investor.Key}' sold ${investor.Value}.");
            }

            return output.ToString();
        }

        internal Dictionary<string, double> GetYearToDateSalesPerInvestor()
        {
            var beginningOfYear = new DateTime(year: DateTime.Now.Year, month: 1, day: 1);
            return GetTotalSoldForDateRangePerInvestor(beginningOfYear);
        }

        internal Dictionary<string, double> GetMonthToDateSalesPerInvestor()
        {
            DateTime now = DateTime.Now;
            DateTime beginningOfMonth = new DateTime(year: now.Year, month: now.Month, day: 1);
            return GetTotalSoldForDateRangePerInvestor(beginningOfMonth);
        }

        internal Dictionary<string, double> GetQuarterToDateSalesPerInvestor()
        {
            DateTime now = DateTime.Now;
            DateTime beginningOfQuarter = new DateTime(year: now.Year, month: GetStartOfQuarter(now), day: 1);
            return GetTotalSoldForDateRangePerInvestor(beginningOfQuarter);
        }

        internal Dictionary<string, double> GetInceptionToDateSalesPerInvestor()
        {
            return GetTotalSoldForDateRangePerInvestor(DateTime.MinValue);
        }

        internal Dictionary<string, double> GetTotalSoldForDateRangePerInvestor(DateTime startDate)
        {
            var totalSoldPerInvestor = new Dictionary<string, double>();
            var filteredTransactions = FilterTransactionsByDate(startDate);
            var investors = GetUniqueInvestors(filteredTransactions);

            foreach (string investor in investors)
            {
                double totalSold = GetSellAmountsForInvestor(investor, filteredTransactions).Sum();
                totalSoldPerInvestor.Add(investor, totalSold);
            }

            return totalSoldPerInvestor;
        }

        internal IEnumerable<double> GetSellAmountsForInvestor(string investor, IEnumerable<Transaction> transactions)
        {
            return transactions.Where(t => t.Investor == investor && t.TransactionType == TransactionType.Sell).Select(t => t.Price);
        }

        internal IEnumerable<Transaction> FilterTransactionsByDate(DateTime startDate)
        {
            return Transactions.Where(t => t.Date >= startDate).ToList();
        }

        internal int GetStartOfQuarter(DateTime now)
        {
            /*
                Quarters are:
                Jan-Mar
                Apr - Jun
                Jul - Sep
                Oct - Dec
             */
            int month = now.Month;

            if (month <= 3)
            {
                return 1; //Jan
            }

            if (month > 3 && month <= 6)
            {
                return 4; //Apr
            }

            if (month > 6 && month <= 9)
            {
                return 7; //Jul
            }

            //else month is greater than 9
            return 10;
        }

        internal IEnumerable<string> GetUniqueInvestors(IEnumerable<Transaction> transactions)
        {
            return transactions.Select(t => t.Investor).Distinct();
        }
    }
}
