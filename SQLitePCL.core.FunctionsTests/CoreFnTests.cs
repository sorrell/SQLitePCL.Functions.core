using Microsoft.VisualStudio.TestTools.UnitTesting;
using SQLitePCL.Functions.core;


namespace SQLitePCL.core.Functions.Tests
{
    [TestClass()]
    public class FnTests
    {
        [TestMethod()]
        public void RegexIsNotMatchTest()
        {
            Assert.IsFalse(CoreFn.RegexIsMatch("this is a 123 test", @"\w+\d{3}\w+"));
        }

        [TestMethod()]
        public void RegexIsMatchTest()
        {
            Assert.IsTrue(CoreFn.RegexIsMatch("12345", @"\d{5}"));
        }

        [TestMethod()]
        public void DateIsValidTest()
        {
            Assert.IsTrue(CoreFn.DateIsValid("20170821", @"yyyymmdd|dd/mm/yyyy"));
        }

        [TestMethod()]
        public void DateIsNotValidTest()
        {
            Assert.IsFalse(CoreFn.DateIsValid("2017/08/21", @"yyyymmdd|dd/mm/yyyy"));
        }

        [TestMethod()]
        public void IsBoolTest()
        {
            Assert.IsTrue(CoreFn.IsBool("true"));
            Assert.IsTrue(CoreFn.IsBool("0"));
        }

        [TestMethod()]
        public void IsNotBoolTest()
        {
            Assert.IsFalse(CoreFn.IsBool("y"));
            Assert.IsFalse(CoreFn.IsBool("100"));
        }

        [TestMethod()]
        public void IsUriTest()
        {
            Assert.IsTrue(CoreFn.IsUri("http://www.cint.io", "http"));
            Assert.IsTrue(CoreFn.IsUri("file://myfile.txt", "file"));
        }

        [TestMethod()]
        public void IsNotUriTest()
        {
            Assert.IsFalse(CoreFn.IsUri("http://www.cint.io", "mailto|uuid"));
        }

        [TestMethod()]
        public void IsIntTest()
        {
            Assert.IsTrue(CoreFn.IsInt("12345"));
            Assert.IsTrue(CoreFn.IsInt("2,147,483,647"));
        }

        [TestMethod()]
        public void IsNotIntTest()
        {
            Assert.IsFalse(CoreFn.IsInt("12345.678"));
        }

        [TestMethod()]
        public void IsUintTest()
        {
            Assert.IsTrue(CoreFn.IsUint("4,294,967,295"));
        }

        [TestMethod()]
        public void IsNotUintTest()
        {
            Assert.IsFalse(CoreFn.IsUint("4,294,967,296"));
            Assert.IsFalse(CoreFn.IsUint("-4"));
        }

        [TestMethod()]
        public void IsLongTest()
        {
            Assert.IsTrue(CoreFn.IsLong("9,223,372,036,854,775,807"));
        }

        [TestMethod()]
        public void IsNotLongTest()
        {
            Assert.IsFalse(CoreFn.IsLong("9,223,372,036,854,775,808"));
            Assert.IsFalse(CoreFn.IsLong("10.56"));
        }

        [TestMethod()]
        public void IsUlongTest()
        {
            Assert.IsTrue(CoreFn.IsUlong("18,446,744,073,709,551,615"));
        }

        [TestMethod()]
        public void IsNotUlongTest()
        {
            Assert.IsFalse(CoreFn.IsUlong("18,446,744,073,709,551,616"));
            Assert.IsFalse(CoreFn.IsUlong("-18.4756"));
        }

        [TestMethod()]
        public void IsByteTest()
        {
            Assert.IsTrue(CoreFn.IsByte("255"));
        }

        [TestMethod()]
        public void IsNotByteTest()
        {
            Assert.IsFalse(CoreFn.IsByte("256"));
            Assert.IsFalse(CoreFn.IsByte("254.5"));
        }

        [TestMethod()]
        public void IsSbyteTest()
        {
            Assert.IsTrue(CoreFn.IsSbyte("127"));
        }

        [TestMethod()]
        public void IsNotSbyteTest()
        {
            Assert.IsFalse(CoreFn.IsSbyte("-129"));
            Assert.IsFalse(CoreFn.IsSbyte("127.5"));
        }

        [TestMethod()]
        public void IsShortTest()
        {
            Assert.IsTrue(CoreFn.IsShort("32,767"));
        }

        [TestMethod()]
        public void IsNotShortTest()
        {
            Assert.IsFalse(CoreFn.IsShort("-32,769"));
            Assert.IsFalse(CoreFn.IsShort("32.54"));
        }

        [TestMethod()]
        public void IsUshortTest()
        {
            Assert.IsTrue(CoreFn.IsUshort("0"));
            Assert.IsTrue(CoreFn.IsUshort("65,535"));
        }

        [TestMethod()]
        public void IsNotUshortTest()
        {
            Assert.IsFalse(CoreFn.IsUshort("-5"));
            Assert.IsFalse(CoreFn.IsUshort("535.5"));
        }

        [TestMethod()]
        public void IsFloatTest()
        {
            Assert.IsTrue(CoreFn.IsFloat("3.4E37"));
            Assert.IsTrue(CoreFn.IsFloat("3.4576746"));
        }

        [TestMethod()]
        public void IsNotFloatTest()
        {
            Assert.IsFalse(CoreFn.IsFloat("3.4E39"));
            Assert.IsFalse(CoreFn.IsFloat("X"));
        }

        [TestMethod()]
        public void IsDoubleTest()
        {
            Assert.IsTrue(CoreFn.IsDouble("1.7E308"));
            Assert.IsTrue(CoreFn.IsDouble("3.4576746"));
        }

        [TestMethod()]
        public void IsNotDoubleTest()
        {
            Assert.IsFalse(CoreFn.IsDouble("1.7E309"));
            Assert.IsFalse(CoreFn.IsDouble("X"));
        }

        [TestMethod()]
        public void IsDecimalTest()
        {
            Assert.IsTrue(CoreFn.IsDecimal(decimal.MaxValue.ToString()));
            Assert.IsTrue(CoreFn.IsDecimal("3.14159"));
        }

        [TestMethod()]
        public void IsNotDecimalTest()
        {
            Assert.IsFalse(CoreFn.IsDecimal("X"));
        }


        [TestMethod()]
        public void IsCharTest()
        {
            Assert.IsTrue(CoreFn.IsChar("A"));
        }

        [TestMethod()]
        public void IsNotCharTest()
        {
            Assert.IsFalse(CoreFn.IsChar("Alphabet"));
        }


        [TestMethod()]
        public void IsISO8601TimespanTest()
        {
            Assert.IsTrue(CoreFn.IsISO8601Timespan("P1Y2MT2H"));
        }

        [TestMethod()]
        public void IsNotISO8601TimespanTest()
        {
            Assert.IsFalse(CoreFn.IsISO8601Timespan("02:14:18"));
        }

        [TestMethod()]
        public void IsDotNetTimespanTest()
        {
            Assert.IsTrue(CoreFn.IsDotNetTimespan("02:14:18"));
        }

        [TestMethod()]
        public void IsNotDotNetTimespanTest()
        {
            Assert.IsFalse(CoreFn.IsDotNetTimespan("P1Y2MT2H"));
        }

        [TestMethod()]
        public void DateCompareTest()
        {
            Assert.IsTrue(CoreFn.DateCompare("20170801", "yyyymmdd", ">", "20110101"));
        }

        [TestMethod()]
        public void DateCompareFalseTest()
        {
            Assert.IsFalse(CoreFn.DateCompare("20170801", "yyyymmdd", "<", "20110101"));
        }

        [TestMethod()]
        public void CompareValsTest()
        {
            Assert.IsTrue(CoreFn.CompareVals("5", ">", "4", "int"));
            Assert.IsTrue(CoreFn.CompareVals("5", ">", "4", "uint"));
            Assert.IsTrue(CoreFn.CompareVals("5", ">", "4", "float"));
            Assert.IsTrue(CoreFn.CompareVals("5", ">", "4", "double"));
            Assert.IsTrue(CoreFn.CompareVals("5", ">", "4", "decimal"));
            Assert.IsTrue(CoreFn.CompareVals("5", ">", "4", "short"));
            Assert.IsTrue(CoreFn.CompareVals("5", ">", "4", "ushort"));
            Assert.IsTrue(CoreFn.CompareVals("5", ">", "4", "long"));
            Assert.IsTrue(CoreFn.CompareVals("5", ">", "4", "ulong"));
            Assert.IsTrue(CoreFn.CompareVals("5", ">", "4", "byte"));
            Assert.IsTrue(CoreFn.CompareVals("5", ">", "4", "sbyte"));
        }

        [TestMethod()]
        public void CompareFalseValsTest()
        {
            Assert.IsFalse(CoreFn.CompareVals("5", "<", "4", "int"));
            Assert.IsFalse(CoreFn.CompareVals("5", "<", "4", "uint"));
            Assert.IsFalse(CoreFn.CompareVals("5", "<", "4", "float"));
            Assert.IsFalse(CoreFn.CompareVals("5", "<", "4", "double"));
            Assert.IsFalse(CoreFn.CompareVals("5", "<", "4", "decimal"));
            Assert.IsFalse(CoreFn.CompareVals("5", "<", "4", "short"));
            Assert.IsFalse(CoreFn.CompareVals("5", "<", "4", "ushort"));
            Assert.IsFalse(CoreFn.CompareVals("5", "<", "4", "long"));
            Assert.IsFalse(CoreFn.CompareVals("5", "<", "4", "ulong"));
            Assert.IsFalse(CoreFn.CompareVals("5", "<", "4", "byte"));
            Assert.IsFalse(CoreFn.CompareVals("5", "<", "4", "sbyte"));
        }

        [TestMethod()]
        public void GetGuidTest()
        {
            var guid = CoreFn.GetGuid();
            Assert.IsTrue(CoreFn.IsGuid(guid.ToString()));
        }

        [TestMethod()]
        public void GetRowNumberTest()
        {
            var rn = CoreFn.GetRowNumber("key");
            Assert.IsTrue(rn == 1);
            rn = CoreFn.GetRowNumber("key");
            Assert.IsTrue(rn == 2);
        }

        [TestMethod()]
        public void IsNonPositiveIntTest()
        {
            Assert.IsTrue(CoreFn.IsNonPositiveInt("0"));
            Assert.IsTrue(CoreFn.IsNonPositiveInt("-1"));
        }

        [TestMethod()]
        public void IsNonNegativeIntTest()
        {
            Assert.IsTrue(CoreFn.IsNonNegativeInt("0"));
            Assert.IsTrue(CoreFn.IsNonNegativeInt("1"));
        }

        [TestMethod()]
        public void IsPositiveIntTest()
        {
            Assert.IsTrue(CoreFn.IsPositiveInt(int.MaxValue.ToString()));
        }

        [TestMethod()]
        public void IsNegativeIntTest()
        {
            Assert.IsTrue(CoreFn.IsNegativeInt(int.MinValue.ToString()));
        }
    }
}