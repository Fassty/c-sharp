using System;
using NUnit.Framework;
using Cuni.Arithmetics.FixedPoint;

namespace Tests
{
    [TestFixture(typeof(Q16_16))]
    [TestFixture(typeof(Q24_8))]
    [TestFixture(typeof(Q8_24))]
    public class Fixed_Tests<Q> where Q : QType, new() 
    {
        Fixed<Q> f1;
        Fixed<Q> f2;


        [TestCase(2, 3, "5")]
        [TestCase(-1, -3, "-4")]
        [TestCase(-2, 5, "3")]
        [TestCase(2, -5, "-3")]
        public void TestAddition(int intValue1, int intValue2, string expectedResult)
        {
            f1 = new Fixed<Q>(intValue1);
            f2 = new Fixed<Q>(intValue2);
            var f3 = f1.Add(f2);

            Assert.AreEqual(expectedResult, f3.ToString());
        }

        [TestCase(3, 2, "1")]
        [TestCase(-1, -3, "2")]
        [TestCase(-2, 5, "-7")]
        [TestCase(2, -5, "7")]
        public void TestSubtraction(int intValue1, int intValue2, string expectedResult)
        {
            f1 = new Fixed<Q>(intValue1);
            f2 = new Fixed<Q>(intValue2);
            var f3 = f1.Subtract(f2);

            Assert.AreEqual(expectedResult, f3.ToString());

        }

        [TestCase(2, 3, "6")]
        [TestCase(-1, -3, "3")]
        [TestCase(-2, 5, "-10")]
        [TestCase(2, -5, "-10")]
        [TestCase(0, 3, "0")]
        public void TestMultiplication(int intValue1, int intValue2, string expectedResult)
        {
            f1 = new Fixed<Q>(intValue1);
            f2 = new Fixed<Q>(intValue2);
            var f3 = f1.Multiply(f2);

            Assert.AreEqual(expectedResult, f3.ToString());

        }
        [TestCase(3, 2, "1,5")]
        [TestCase(-3, -1, "3")]
        [TestCase(-5, 2, "-2,5")]
        [TestCase(5, -2, "-2,5")]
        [TestCase(0, 5, "0")]
        public void TestDivision(int intValue1, int intValue2, string expectedResult)
        {
            f1 = new Fixed<Q>(intValue1);
            f2 = new Fixed<Q>(intValue2);
            var f3 = f1.Divide(f2);

            Assert.AreEqual(expectedResult, f3.ToString());

        }

        [TestCase(1,"1")]
        [TestCase(-20,"-20")]
        [TestCase(0,"0")]
        public void TestAssignment(int value, string expectedResult)
        {
            f1 = value;

            Assert.AreEqual(expectedResult,f1.ToString());
        }

        [TestCase(2, 3, "5")]
        [TestCase(-1, -3, "-4")]
        [TestCase(-2, 5, "3")]
        [TestCase(2, -5, "-3")]
        public void TestAddition_WithOperator(int intValue1, int intValue2, string expectedResult)
        {
            f1 = intValue1;
            f2 = intValue2;
            var f3 = f1 + f2;

            Assert.AreEqual(expectedResult, f3.ToString());
        }

        [TestCase(3, 2, "1")]
        [TestCase(-1, -3, "2")]
        [TestCase(-2, 5, "-7")]
        [TestCase(2, -5, "7")]
        public void TestSubtraction_WithOperator(int intValue1, int intValue2, string expectedResult)
        {
            f1 = intValue1;
            f2 = intValue2;
            var f3 = f1 - f2;

            Assert.AreEqual(expectedResult, f3.ToString());

        }

        [TestCase(2, 3, "6")]
        [TestCase(-1, -3, "3")]
        [TestCase(-2, 5, "-10")]
        [TestCase(2, -5, "-10")]
        [TestCase(0, 3, "0")]
        public void TestMultiplication_WithOperator(int intValue1, int intValue2, string expectedResult)
        {
            f1 = intValue1;
            f2 = intValue2;
            var f3 = f1 * f2;

            Assert.AreEqual(expectedResult, f3.ToString());

        }
        [TestCase(3, 2, "1,5")]
        [TestCase(-3, -1, "3")]
        [TestCase(-5, 2, "-2,5")]
        [TestCase(5, -2, "-2,5")]
        [TestCase(0, 5, "0")]
        public void TestDivision_WithOperator(int intValue1, int intValue2, string expectedResult)
        {
            f1 = intValue1;
            f2 = intValue2;
            var f3 = f1 / f2;

            Assert.AreEqual(expectedResult, f3.ToString());

        }

    }

    [TestFixture(typeof(Q16_16))]
    [TestFixture(typeof(Q24_8))]
    [TestFixture(typeof(Q8_24))]
    public class FixedQ24_8ConversionTests<Q> where Q : QType, new()
    {

        Fixed<Q24_8> sourceHigh = new Fixed<Q24_8>(1048576);
        Fixed<Q24_8> sourceLow = new Fixed<Q24_8>(8);

        [TestCase("8")]
        public void SimpleConversion(string expectedResult)
        {
            if (typeof(Q) == typeof(Q24_8))
            {
                var dest = (Fixed<Q24_8>)sourceLow;

                Assert.AreEqual(expectedResult,dest.ToString());
            }else if(typeof(Q) == typeof(Q16_16))
            {
                var dest = (Fixed<Q16_16>)sourceLow;

                Assert.AreEqual(expectedResult, dest.ToString());
            }
            else
            {
                var dest = (Fixed<Q8_24>)sourceLow;

                Assert.AreEqual(expectedResult, dest.ToString());
            }
        }

        [TestCase("0")]
        public void HigherToLowerConversion(string expectedResult)
        {
            if (typeof(Q) == typeof(Q16_16))
            {
                var dest = (Fixed<Q16_16>)sourceHigh;
                Console.WriteLine(dest.ToString());
                Assert.AreEqual(expectedResult, dest.ToString());
            }
            else if(typeof(Q) == typeof(Q8_24))
            {
                var dest = (Fixed<Q8_24>)sourceHigh;
                Console.WriteLine(dest.ToString());
                Assert.AreEqual(expectedResult, dest.ToString());
            }
        }

    }

    [TestFixture(typeof(Q16_16))]
    [TestFixture(typeof(Q24_8))]
    [TestFixture(typeof(Q8_24))]
    public class FIxedQ16_16ConversionTests<Q> where Q : QType, new()
    {
        Fixed<Q16_16> sourceHigh = new Fixed<Q16_16>(4096);
        Fixed<Q16_16> sourceLow = new Fixed<Q16_16>(8);

        [TestCase("8")]
        public void SimpleConversion(string expectedResult)
        {
            if (typeof(Q) == typeof(Q24_8))
            {
                var dest = (Fixed<Q24_8>)sourceLow;

                Assert.AreEqual(expectedResult, dest.ToString());
            }
            else if (typeof(Q) == typeof(Q16_16))
            {
                var dest = (Fixed<Q16_16>)sourceLow;

                Assert.AreEqual(expectedResult, dest.ToString());
            }
            else
            {
                var dest = (Fixed<Q8_24>)sourceLow;

                Assert.AreEqual(expectedResult, dest.ToString());
            }
        }

        [TestCase("0")]
        public void HigherToLowerConversion(string expectedResult)
        {
            if (typeof(Q) == typeof(Q8_24))
            {
                var dest = (Fixed<Q8_24>)sourceHigh;
                Console.WriteLine(dest.ToString());
                Assert.AreEqual(expectedResult, dest.ToString());
            }
        }
    }

    [TestFixture(typeof(Q16_16))]
    [TestFixture(typeof(Q24_8))]
    [TestFixture(typeof(Q8_24))]
    public class FixedQ8_24ConversionTests<Q> where Q : QType, new()
    {
        Fixed<Q8_24> sourceLow = new Fixed<Q8_24>(8);

        [TestCase("8")]
        public void SimpleConversion(string expectedResult)
        {
            if (typeof(Q) == typeof(Q24_8))
            {
                var dest = (Fixed<Q24_8>)sourceLow;

                Assert.AreEqual(expectedResult, dest.ToString());
            }
            else if (typeof(Q) == typeof(Q16_16))
            {
                var dest = (Fixed<Q16_16>)sourceLow;

                Assert.AreEqual(expectedResult, dest.ToString());
            }
            else
            {
                var dest = (Fixed<Q8_24>)sourceLow;

                Assert.AreEqual(expectedResult, dest.ToString());
            }
        }
    }

}