using System.Collections.Generic;
using System.Linq;
using System.Text;
using TransactionReportGenerator.Models;

namespace TransactionReportGenerator.Reports
{
    public class AssetReport : ReportBase
    {
        public AssetReport(List<Transaction> transactions) : base(transactions) { }

        public override string PrintToString()
        {
            StringBuilder output = new StringBuilder();

            output.Append("********** Assets Summary **********");
            output.AppendLine("Not Implemented");
            output.AppendLine();

            return output.ToString();
        }
    }
}
