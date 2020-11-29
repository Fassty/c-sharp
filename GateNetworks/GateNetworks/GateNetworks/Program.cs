using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GateNetworks
{
    /// <summary>
    /// Helper static classes
    /// </summary>
    static class Utilities
    {
        public static bool Contains<T>(T[] arr, T name)
        {
            return arr.Any(item => EqualityComparer<T>.Default.Equals(item, name));
        }

        public static int IndexOf<T>(T[] arr, T name)
        {
            for (var i = 0; i < arr.Length; i++)
            {
                if (EqualityComparer<T>.Default.Equals(arr[i], name)) return i;
            }
            throw new MyException("Invalid index");
        }

        public static bool HasDuplicates<T>(T[] arr)
        {
            return arr.Length == arr.Distinct().Count();
        }

        public static T[] Slice<T>(this T[] source, int from, int to)
        {
            int len = to - from;
            T[] dest = new T[len];
            for (int i = 0; i < len; i++)
            {
                dest[i] = source[i + from];
            }

            return dest;
        }

        public static bool ValidValues(this string[] source)
        {
            foreach (var value in source)
            {
                if (value.Contains(" ") || value.Contains("\t") || value.Contains("\n") || value.Contains("\r"))
                {
                    return false;
                }

                if (value.Contains(".") || value.Contains(";") || value.Contains("->") || value.StartsWith("end"))
                {
                    return false;
                }

                if (value.Length == 0) return false;
            }

            return true;
        }
    }

    /// <summary>
    /// Custom exception definitions
    /// </summary>
    #region Exceptions

    class MyException : Exception
    {
        public MyException() { }
        public MyException(string message) : base(message) { }
    }
    class ErrorReadingFile : Exception { }

    class ParseError : Exception
    {
        private long line;
        private string message;

        public ParseError(long line, string message)
        {
            this.line = line;
            this.message = message;
        }

        public string ErrorToString()
        {
            return string.Format("Line {0}: {1}", line, message);
        }
    }

    class DuplicateError : ParseError
    {
        public DuplicateError(long line) : base(line, "Duplicate."){ }
    }

    class MissingKeywordError : ParseError
    {
        public MissingKeywordError(long line) : base(line, "Missing keyword."){ }
    }

    class BindingRuleError : ParseError
    {
        public BindingRuleError(long line) : base(line, "Binding rule."){ }
    }

    class SyntaxError : ParseError
    {
        public SyntaxError(long line) : base(line, "Syntax error.") { }
    }
    class ChargeParsingError : Exception { }
    class DuplicityException : Exception { }

    class InvalidValueException : Exception { }
    class InvalidVectorSizeException : Exception { }
    class EOF : Exception { }

    #endregion

    class Program
    {
        static void Main(string[] args)
        {
            FunctionVector vector = new FunctionVector();
            vector.GetHashCode();
        }
    }
}
