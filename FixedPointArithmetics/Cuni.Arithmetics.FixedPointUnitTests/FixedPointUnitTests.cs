using System;
using System.Collections;
using System.Collections.Generic;
using Cuni.Arithmetics.FixedPoint;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Cuni.Arithmetics.FixedPointUnitTests
{


    [TestClass]
    public class FixedPointUnitTests
    {
        #region Q24_8 Tests
        [TestMethod]
        public void Q24_8_ZeroValueTest()
        {
            var zeroVal = new Fixed<Q24_8>(0);
            var five = zeroVal.Add(5);

            Assert.AreEqual("5", five.ToString());
        }

        [TestMethod]
        public void Q24_8_ValidValueTest()
        {
            var positive = new Fixed<Q24_8>(3);
            var negative = new Fixed<Q24_8>(-7);

            Fixed<Q16_16> f = (Fixed<Q16_16>)negative;
            Assert.AreEqual("-7",f.ToString());
            Assert.AreEqual("3",positive.ToString());
            Assert.AreEqual("-7",negative.ToString());
        }

        [TestMethod]
        public void Q24_8_MaxValueTest()
        {
            var max = new Fixed<Q24_8>((1<<24)-1);
            
            Assert.AreEqual("-1",max.ToString());
        }

        [TestMethod]
        public void Q24_8_AddNoOverflow()
        {
            var pos = new Fixed<Q24_8>(128);
            var neg = new Fixed<Q24_8>(-1024);

            var posRes = pos.Add(pos);
            var posNeg = pos.Add(neg);
            var negPos = neg.Add(pos);
            var negRes = neg.Add(neg);

            Assert.AreEqual("256",posRes.ToString());
            Assert.AreEqual("-896",posNeg.ToString());
            Assert.AreEqual("-896",negPos.ToString());
            Assert.AreEqual("-2048",negRes.ToString());
        }

        [TestMethod]
        public void Q24_8_AddOverflow()
        {
            var max = new Fixed<Q24_8>(Int32.MaxValue);

            var res = max.Add(max);

            Assert.AreEqual("-2",res.ToString());
        }

        [TestMethod]
        public void Q24_8_AddDecimal()
        {
            var lhs = new Fixed<Q24_8>(10).Divide(new Fixed<Q24_8>(4));
            var rhs = new Fixed<Q24_8>(5).Divide(new Fixed<Q24_8>(2));

            var res = lhs.Add(rhs);

            Assert.AreEqual("5", res.ToString());
        }

        [TestMethod]
        public void Q24_8_MultiplyNoOverflow()
        {
            var pos = new Fixed<Q24_8>(2);
            var neg = new Fixed<Q24_8>(-16);

            var posRes = pos.Multiply(pos);
            var posNeg = pos.Multiply(neg);
            var negPos = neg.Multiply(pos);
            var negRes = neg.Multiply(neg);

            Assert.AreEqual("4", posRes.ToString());
            Assert.AreEqual("-32", posNeg.ToString());
            Assert.AreEqual("-32", negPos.ToString());
            Assert.AreEqual("256", negRes.ToString());
        }

        [TestMethod]
        public void Q24_8_MultiplyOverflow()
        {
            var lhs = new Fixed<Q24_8>(Int32.MaxValue);

            var res = lhs.Multiply(lhs);

            Assert.AreEqual("1", res.ToString());
        }

        [TestMethod]
        public void Q24_8_MultiplyDecimal()
        {
            var lhs = new Fixed<Q24_8>(10).Divide(new Fixed<Q24_8>(4));

            var res = lhs.Multiply(new Fixed<Q24_8>(8));

            Assert.AreEqual("20", res.ToString());
        }

        [TestMethod]
        public void Q24_8_DivideSimple()
        {
            var pos = new Fixed<Q24_8>(5);
            var neg = new Fixed<Q24_8>(-10);

            var posRes = pos.Divide(pos);
            var posNeg = pos.Divide(neg);
            var negPos = neg.Divide(pos);
            var negRes = neg.Divide(neg);

            Assert.AreEqual("1", posRes.ToString());
            Assert.AreEqual("-0,5",posNeg.ToString());
            Assert.AreEqual("-2",negPos.ToString());
            Assert.AreEqual("1",negRes.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException), "Tried dividing by zero")]
        public void Q24_8_DivideByZero()
        {
            var nonZero = new Fixed<Q24_8>(10);
            var zero = new Fixed<Q24_8>(0);

            var res = nonZero.Divide(zero);
        }

        #endregion

        #region Q16_16 Tests
        [TestMethod]
        public void Q16_16_ZeroValueTest()
        {
            var zero = new Fixed<Q16_16>(0);

            Assert.AreEqual("0", zero.ToString());
        }

        [TestMethod]
        public void Q16_16_MaxValueTest()
        {
            var max = new Fixed<Q16_16>((1<<16)-1);

            Assert.AreEqual("-1",max.ToString());
        }

        [TestMethod]
        public void Q16_16_AddNoOverflow()
        {
            var pos = new Fixed<Q16_16>(128);
            var neg = new Fixed<Q16_16>(-1024);

            var posRes = pos.Add(pos);
            var posNeg = pos.Add(neg);
            var negPos = neg.Add(pos);
            var negRes = neg.Add(neg);

            Assert.AreEqual("256", posRes.ToString());
            Assert.AreEqual("-896", posNeg.ToString());
            Assert.AreEqual("-896", negPos.ToString());
            Assert.AreEqual("-2048", negRes.ToString());
        }

        [TestMethod]
        public void Q16_16_AddOverflow()
        {
            var max = new Fixed<Q16_16>(Int32.MaxValue);

            var res = max.Add(max);

            Assert.AreEqual("-2", res.ToString());
        }

        [TestMethod]
        public void Q16_16_MultiplyNoOverflow()
        {
            var pos = new Fixed<Q16_16>(2);
            var neg = new Fixed<Q16_16>(-16);

            var posRes = pos.Multiply(pos);
            var posNeg = pos.Multiply(neg);
            var negPos = neg.Multiply(pos);
            var negRes = neg.Multiply(neg);

            Assert.AreEqual("4", posRes.ToString());
            Assert.AreEqual("-32", posNeg.ToString());
            Assert.AreEqual("-32", negPos.ToString());
            Assert.AreEqual("256", negRes.ToString());
        }

        [TestMethod]
        public void Q16_16_MultiplyOverflow()
        {
            var lhs = new Fixed<Q16_16>(Int32.MaxValue);

            var res = lhs.Multiply(lhs);

            Assert.AreEqual("1", res.ToString());
        }

        [TestMethod]
        public void Q16_16_DivideSimple()
        {
            var pos = new Fixed<Q16_16>(5);
            var neg = new Fixed<Q16_16>(-10);

            var posRes = pos.Divide(pos);
            var posNeg = pos.Divide(neg);
            var negPos = neg.Divide(pos);
            var negRes = neg.Divide(neg);

            Assert.AreEqual("1", posRes.ToString());
            Assert.AreEqual("-0,5", posNeg.ToString());
            Assert.AreEqual("-2", negPos.ToString());
            Assert.AreEqual("1", negRes.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException),"Tried dividing by zero")]
        public void Q16_16_DivideByZero()
        {
            var err = new Fixed<Q16_16>(10).Divide(new Fixed<Q16_16>(0));
        }

        #endregion

        #region Q8_24 Tests
        [TestMethod]
        public void Q8_24_ZeroValueTest()
        {
            var zero = new Fixed<Q8_24>(0);

            Assert.AreEqual("0",zero.ToString());
        }

        [TestMethod]
        public void Q8_24_MaxValueTest()
        {
            var max = new Fixed<Q8_24>((1<<8)-1);

            Assert.AreEqual("-1",max.ToString());
        }

        [TestMethod]
        public void Q8_24_AddNoOverflow()
        {
            var pos = new Fixed<Q8_24>(16);
            var neg = new Fixed<Q8_24>(-32);

            var posRes = pos.Add(pos);
            var posNeg = pos.Add(neg);
            var negPos = neg.Add(pos);
            var negRes = neg.Add(neg);

            Assert.AreEqual("32", posRes.ToString());
            Assert.AreEqual("-16", posNeg.ToString());
            Assert.AreEqual("-16", negPos.ToString());
            Assert.AreEqual("-64", negRes.ToString());
        }

        [TestMethod]
        public void Q8_24_AddOverflow()
        {
            var max = new Fixed<Q8_24>(Int32.MaxValue);

            var res = max.Add(max);

            Assert.AreEqual("-2", res.ToString());
        }

        [TestMethod]
        public void Q8_24_MultiplyNoOverflow()
        {
            var pos = new Fixed<Q8_24>(2);
            var neg = new Fixed<Q8_24>(-16);

            var posRes = pos.Multiply(pos);
            var posNeg = pos.Multiply(neg);
            var negPos = neg.Multiply(pos);
            var negRes = neg.Multiply(neg);

            Assert.AreEqual("4", posRes.ToString());
            Assert.AreEqual("-32", posNeg.ToString());
            Assert.AreEqual("-32", negPos.ToString());
        }

        [TestMethod]
        public void Q8_24_MultiplyOverflow()
        {
            var lhs = new Fixed<Q8_24>(16);

            var res = lhs.Multiply(lhs);

            Assert.AreEqual("0",res.ToString());
        }

        [TestMethod]
        public void Q8_24_DivideSimple()
        {
            var pos = new Fixed<Q8_24>(10);
            var neg = new Fixed<Q8_24>(-5);

            var posRes = pos.Divide(pos);
            var posNeg = pos.Divide(neg);
            var negPos = neg.Divide(pos);
            var negRes = neg.Divide(neg);

            Assert.AreEqual("1", posRes.ToString());
            Assert.AreEqual("-2", posNeg.ToString());
            Assert.AreEqual("-0,5", negPos.ToString());
            Assert.AreEqual("1", negRes.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException),"Tried dividing by zero")]
        public void Q8_24_DivideByZero()
        {
            var err = new Fixed<Q8_24>(10).Divide(new Fixed<Q8_24>(0));
        }
        #endregion
    }
}
