using System.Collections.Generic;
using System.Text;
using TransactionReportGenerator.Models;

namespace TransactionReportGenerator.Reports
{
    public class AssetReport : ReportBase
    {
        public AssetReport(IEnumerable<Transaction> transactions) : base(transactions) { }

        public override string PrintToString()
        {
            var output = new StringBuilder();

            output.AppendLine("********** Assets Summary **********");
            output.AppendLine("Not Implemented");
            output.AppendLine();

            return output.ToString();
        }
    }
}
