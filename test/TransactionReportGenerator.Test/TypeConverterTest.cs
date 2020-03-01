using System;
using FluentAssertions;
using NUnit.Framework;
using TransactionReportGenerator.Core.Models;
using TransactionReportGenerator.Core.Models.TypeConverters;

namespace TransactionReportGenerator.Test
{
    public class TypeConverterTest
    {
        [TestCase("BUY")]
        [TestCase("buy")]
        [TestCase("bUy")]
        public void ConvertValidTransactionTypeBuyTest_Buy(string toConvert)
        {
            var converter = new TransactionTypeConverter();
            var convertedType = (TransactionType)converter.ConvertFromString(toConvert, null, null);
            convertedType.Should().Be(TransactionType.Buy);
        }

        [TestCase("SELL")]
        [TestCase("sell")]
        [TestCase("sELl")]
        public void ConvertValidTransactionTypeValueTest_Sell(string toConvert)
        {
            var converter = new TransactionTypeConverter();
            var convertedType = (TransactionType)converter.ConvertFromString(toConvert, null, null);
            convertedType.Should().Be(TransactionType.Sell);
        }

        [TestCase("abc")]
        [TestCase("selI")]
        [TestCase("")]
        [TestCase("                 ")]
        [TestCase(null)]
        public void ConvertInvalidTransactionTypeValueTest(string toConvert)
        {
            var converter = new TransactionTypeConverter();
            var convertedType = (TransactionType)converter.ConvertFromString(toConvert, null, null);
            convertedType.Should().Be(TransactionType.None);
        }

        [TestCase("$0.99", 0.99)]
        [TestCase("$123", 123)]
        [TestCase("$92834.34", 92834.34)]
        [TestCase("$2348.00", 2348.00)]
        [TestCase("$123.0012", 123.0012)]
        [TestCase("-123.99", -123.99)]
        [TestCase("-123", -123)]
        [TestCase("-123.00", -123.00)]
        [TestCase("0.99", 0.99)]
        [TestCase("0", 0)]
        [TestCase("123.123", 123.123)]
        public void ConvertValidPriceTest(string toConvert, double expectedValue)
        {
            var converter = new PriceTypeConverter();
            var convertedType = (double)converter.ConvertFromString(toConvert, null, null);
            convertedType.Should().Be(expectedValue);
        }

        [TestCase("abc")]
        [TestCase("")]
        [TestCase("       ")]
        [TestCase("#0.99")]
        public void ConvertInvalidPriceTest(string toConvert)
        {
            Action convertInvalidPrice = () => new PriceTypeConverter().ConvertFromString(toConvert, null, null);
            convertInvalidPrice.Should().Throw<FormatException>();
        }
    }
}
