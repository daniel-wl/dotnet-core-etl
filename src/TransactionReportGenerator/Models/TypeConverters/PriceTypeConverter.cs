using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace TransactionReportGenerator.Models.TypeConverters
{
    public class PriceTypeConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            return double.Parse(text.Replace("$", string.Empty));
        }
    }
}
