using System;
using System.IO;
using NUnit.Framework;
using TransactionReportGenerator.Models;


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
    }
}