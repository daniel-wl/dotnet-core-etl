using System;
using System.ComponentModel;
using System.Globalization;

using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace TransactionReportGenerator.Models
{
    public class TransactionTypeConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if(string.Compare(text, "BUY", StringComparison.OrdinalIgnoreCase) == 0)
            {
                return TransactionType.Buy;
            }

            if(string.Compare(text, "SELL", StringComparison.OrdinalIgnoreCase) == 0)
            {
                return TransactionType.Sell;
            }

            return TransactionType.None;
        }
    }
}