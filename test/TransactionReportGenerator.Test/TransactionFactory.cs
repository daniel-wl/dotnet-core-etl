using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using TransactionReportGenerator.Models;

namespace TransactionReportGenerator.Test
{
    public class TransactionFactory
    {
        private static Faker Faker = new Faker();
        private Func<double> _Price = () => Faker.Random.Double(0, double.MaxValue);
        private Func<string> _Investor = () => Faker.Random.String2(Faker.Random.Int(0, 100));
        private Func<TransactionType> _TransactionType = () => (TransactionType)Faker.Random.Int(0, 2);
        private Func<DateTime> _Date = () => Faker.Date.Recent();
        private Func<string> _SalesRep = () => Faker.Random.String2(Faker.Random.Int(0, 100));

        public List<Transaction> Build(int numberOfTransactions) =>
            Enumerable.Range(0, numberOfTransactions).Select(i => Build()).ToList();

        public Transaction Build() => new Transaction
        {
            Date = _Date(),
            TransactionType = _TransactionType(),
            Shares = Faker.Random.Double(0, double.MaxValue),
            Price = _Price(),
            Fund = Faker.Random.String2(Faker.Random.Int(0, 100)),
            Investor = _Investor(),
            SalesRepresentative = _SalesRep()
        };

        public TransactionFactory WithPrice(Func<double> price)
        {
            _Price = price;
            return this;
        }

        public TransactionFactory WithInvestor(Func<string> investor)
        {
            _Investor = investor;
            return this;
        }

        public TransactionFactory WithTransactionType(Func<TransactionType> transactionType)
        {
            _TransactionType = transactionType;
            return this;
        }

        public TransactionFactory WithDate(Func<DateTime> date)
        {
            _Date = date;
            return this;
        }

        public TransactionFactory WithSalesRep(Func<string> salesRep)
        {
            _SalesRep = salesRep;
            return this;
        }
    }
}
