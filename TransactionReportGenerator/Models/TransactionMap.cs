using CsvHelper.Configuration;

namespace TransactionReportGenerator.Models
{
    public sealed class TransactionMap : ClassMap<Transaction>
    {
        public TransactionMap()
        {
            Map(m => m.Date).Name("TXN_DATE");
            Map(m => m.TransactionType).Name("TXN_TYPE").TypeConverter<TransactionTypeConverter>();
            Map(m => m.Shares).Name("TXN_SHARES");
            Map(m => m.Price).Name("TXN_PRICE");
            Map(m => m.Fund).Name("FUND");
            Map(m => m.Investor).Name("INVESTOR");
            Map(m => m.SalesRepresentative).Name("SALES_REP");
        }
    }
}