using System.Collections.Generic;
using System.Text;
using TransactionReportGenerator.Models;

namespace TransactionReportGenerator.Reports
{
    public class BreakReport : ReportBase
    {
        public BreakReport(IEnumerable<Transaction> transactions) : base(transactions) { }

        public override string PrintToString()
        {
            StringBuilder output = new StringBuilder();

            output.Append("*********** Break Report ***********");
            output.AppendLine("Not Implemented");
            output.AppendLine();

            return output.ToString();
        }
    }
}
