using CommandLine;

namespace TransactionReportGenerator.Console
{
    public class Options
    {
        [Option('p', "pathToCsv", Required = true)]
        public string PathToCsv { get; set; }
    }
}
