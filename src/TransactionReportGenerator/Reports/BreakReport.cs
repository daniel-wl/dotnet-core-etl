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
            var output = new StringBuilder();

            output.AppendLine("*********** Break Report ***********");
            output.AppendLine("Not Implemented");
            output.AppendLine();

            return output.ToString();
        }
    }
}
