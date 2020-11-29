using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cuni.Arithmetics.FixedPoint
{
    public abstract class QType { }

    public sealed class Q8_24 : QType { }

    public sealed class Q16_16 : QType { }

    public sealed class Q24_8 : QType { }

    public struct Fixed<Q> : IEquatable<Fixed<Q>>, IComparable<Fixed<Q>> where Q : QType
    {
        static int _fractionalBits;

        static Fixed()
        {
            if (typeof(Q) == typeof(Q8_24)) _fractionalBits = 24;
            if (typeof(Q) == typeof(Q16_16)) _fractionalBits = 16;
            if (typeof(Q) == typeof(Q24_8)) _fractionalBits = 8;
        }

        private int _value;

        public Fixed(int integerValue) => _value = integerValue << _fractionalBits;

        public Fixed<Q> Add(Fixed<Q> fixed2)
        {
            var result = new Fixed<Q> {_value = _value + fixed2._value};
            return result;
        }

        public Fixed<Q> Subtract(Fixed<Q> fixed2) => new Fixed<Q> { _value = _value - fixed2._value };

        public Fixed<Q> Multiply(Fixed<Q> fixed2)
        {
            var result = _value * (long)fixed2._value;
            return new Fixed<Q> { _value = (int)(result >> _fractionalBits) };
        }

        public Fixed<Q> Divide(Fixed<Q> fixed2)
        {
            var result = (((long)_value) << _fractionalBits) / (long)fixed2._value;
            return new Fixed<Q> { _value = (int) result };
        }

        private double ToDouble() => _value / (double)(1 << _fractionalBits);

        public override int GetHashCode()
        {
            // No point implementing a complicated hash function
            return this._value;
        }

        public int CompareTo(Fixed<Q> other)
        {
            return this._value.CompareTo(other._value);
        }

        public static bool operator >(Fixed<Q> q1, Fixed<Q> q2) => q1.CompareTo(q2) == 1;
        public static bool operator <(Fixed<Q> q1, Fixed<Q> q2) => q1.CompareTo(q2) == -1;
        public static bool operator >=(Fixed<Q> q1, Fixed<Q> q2) => q1.CompareTo(q2) == 1 || q1.CompareTo(q2) == 0;
        public static bool operator <=(Fixed<Q> q1, Fixed<Q> q2) => q1.CompareTo(q2) == 1 || q1.CompareTo(q2) == 0;
        public static bool operator ==(Fixed<Q> q1, Fixed<Q> q2) => q1.CompareTo(q2) == 0;
        public static bool operator !=(Fixed<Q> q1, Fixed<Q> q2) => q1.CompareTo(q2) != 0;

        public override string ToString() => ToDouble().ToString();

        public bool Equals(Fixed<Q> other)
        {
            return this._value == other._value;
        }

        public static implicit operator Fixed<Q>(int value) => new Fixed<Q>(value);

        public static explicit operator int(Fixed<Q> @fixed) => @fixed._value;

        public static Fixed<Q> operator +(Fixed<Q> fixed1, Fixed<Q> fixed2) => fixed1.Add(fixed2);

        public static Fixed<Q> operator -(Fixed<Q> fixed1, Fixed<Q> fixed2) => fixed1.Add(-(fixed2._value >> _fractionalBits));

        public static Fixed<Q> operator *(Fixed<Q> fixed1, Fixed<Q> fixed2) => fixed1.Multiply(fixed2);

        public static Fixed<Q> operator /(Fixed<Q> fixed1, Fixed<Q> fixed2) => fixed1.Divide(fixed2);

        public static explicit operator Fixed<Q24_8>(Fixed<Q> @fixed)
        {
            if(typeof(Q) == typeof(Q24_8)) return new Fixed<Q24_8>
            {
                _value = @fixed._value
            };
            if (typeof(Q) == typeof(Q16_16)) return new Fixed<Q24_8>
            {
                _value = @fixed._value >> 8
            };
            else return new Fixed<Q24_8>
            {
                _value = @fixed._value >> 16
            };
        }

        public static explicit operator Fixed<Q16_16>(Fixed<Q> @fixed)
        {
            if (typeof(Q) == typeof(Q24_8)) return new Fixed<Q16_16>
            {
                _value = @fixed._value << 8
            };
            if (typeof(Q) == typeof(Q16_16)) return new Fixed<Q16_16>
            {
                _value = @fixed._value 
            };
            else return new Fixed<Q16_16>
            {
                _value = @fixed._value >> 8
            };
        }

        public static explicit operator Fixed<Q8_24>(Fixed<Q> @fixed)
        {
            if (typeof(Q) == typeof(Q24_8)) return new Fixed<Q8_24>
            {
                _value = @fixed._value << 16
            };
            if (typeof(Q) == typeof(Q16_16)) return new Fixed<Q8_24>
            {
                _value = @fixed._value << 8
            };
            else return new Fixed<Q8_24>
            {
                _value = @fixed._value
            };
        }
    }
}
