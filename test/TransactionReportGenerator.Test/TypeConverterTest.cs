using System;
using System.IO;
using NUnit.Framework;
using TransactionReportGenerator.Models;
using TransactionReportGenerator.Models.TypeConverters;

namespace TransactionReportGenerator.Test
{
    public class TypeConverterTest
    {
        [TestCase("BUY")]
        [TestCase("buy")]
        [TestCase("bUy")]
        public void ConvertValidTransactionTypeBuyTest_Buy(string toConvert)
        {
            TransactionTypeConverter converter = new TransactionTypeConverter();
            TransactionType convertedType = (TransactionType)converter.ConvertFromString(toConvert, null, null);
            Assert.AreEqual(TransactionType.Buy, convertedType, "Expected BUY to convert to TransactionType.Buy.");
        }

        [TestCase("SELL")]
        [TestCase("sell")]
        [TestCase("sELl")]
        public void ConvertValidTransactionTypeValueTest_Sell(string toConvert)
        {
            TransactionTypeConverter converter = new TransactionTypeConverter();
            TransactionType convertedType = (TransactionType)converter.ConvertFromString(toConvert, null, null);
            Assert.AreEqual(TransactionType.Sell, convertedType, "Expected SELL to convert to TransactionType.Buy.");
        }

        [TestCase("abc")]
        [TestCase("selI")]
        [TestCase("")]
        [TestCase("                 ")]
        [TestCase(null)]
        public void ConvertInvalidTransactionTypeValueTest(string toConvert)
        {
            TransactionTypeConverter converter = new TransactionTypeConverter();
            TransactionType convertedType = (TransactionType)converter.ConvertFromString(toConvert, null, null);
            Assert.AreEqual(TransactionType.None, convertedType, "Expected SELL to convert to TransactionType.Buy.");
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
            PriceTypeConverter converter = new PriceTypeConverter();
            double convertedType = (double)converter.ConvertFromString(toConvert, null, null);
            Assert.AreEqual(expectedValue, convertedType, $"Expected valid value {toConvert} to convert successfully.");
        }

        [TestCase("abc")]
        [TestCase("")]
        [TestCase("       ")]
        [TestCase("#0.99")]
        public void ConvertInvalidPriceTest(string toConvert)
        {
            PriceTypeConverter converter = new PriceTypeConverter();
            Assert.Throws<FormatException>(() => {
                converter.ConvertFromString(toConvert, null, null);
            });
        }
    }
}