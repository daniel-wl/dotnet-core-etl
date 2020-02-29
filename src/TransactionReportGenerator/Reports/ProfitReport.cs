using System.Collections.Generic;
using System.Text;
using TransactionReportGenerator.Models;

namespace TransactionReportGenerator.Reports
{
    public class ProfitReport : ReportBase
    {
        public ProfitReport(List<Transaction> transactions) : base(transactions) { }

        public override string PrintToString()
        {
            StringBuilder output = new StringBuilder();

            output.AppendLine("********** Profit  Report **********");
            output.AppendLine("Not Implemented");
            output.AppendLine();

            return output.ToString();
        }
    }
}
