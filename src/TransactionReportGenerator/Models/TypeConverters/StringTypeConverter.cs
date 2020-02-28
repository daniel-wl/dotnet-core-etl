using System;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace TransactionReportGenerator.Models.TypeConverters
{
    public class StringTypeConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            return text.Replace("\n", " ").Replace("\r", string.Empty);
        }
    }
}