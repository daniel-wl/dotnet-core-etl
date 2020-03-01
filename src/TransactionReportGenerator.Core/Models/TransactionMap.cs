using CsvHelper.Configuration;
using TransactionReportGenerator.Core.Models.TypeConverters;

namespace TransactionReportGenerator.Core.Models
{
    public sealed class TransactionMap : ClassMap<Transaction>
    {
        public TransactionMap()
        {
            Map(m => m.Date).Name("TXN_DATE");
            Map(m => m.TransactionType).Name("TXN_TYPE").TypeConverter<TransactionTypeConverter>();
            Map(m => m.Shares).Name("TXN_SHARES");
            Map(m => m.Price).Name("TXN_PRICE").TypeConverter<PriceTypeConverter>();
            Map(m => m.Fund).Name("FUND").TypeConverter<StringTypeConverter>();
            Map(m => m.Investor).Name("INVESTOR").TypeConverter<StringTypeConverter>();
            Map(m => m.SalesRepresentative).Name("SALES_REP").TypeConverter<StringTypeConverter>();
        }
    }
}
