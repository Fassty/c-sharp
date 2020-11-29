using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Runtime.InteropServices.ComTypes;

namespace ExpressionEval
{
    class ExpressionFactory
    {

        private ExpressionFactory() { }

        public static Expression GetByOperator(char op)
        {
            switch (op)
            {
                case '+':
                    return new Addition();
                case '-':
                    return new Subtraction();
                case '*':
                    return new Multiplication();
                case '/':
                    return new Division();
                case '~':
                    return new Negation();
                default: return null;
            }
        }
    }

    class ValueFactory<T>
    {
        private ValueFactory() { }

        private static readonly Dictionary<int, Type> dict = new Dictionary<int, Type>();

        public static Type GetType(int type)
        {
            return dict[type];
        }

        public static T GetValue(int id, object value)
        {
            if(dict.TryGetValue(id , out var type))
                return(T) Activator.CreateInstance(type, value);
            throw new ArgumentException("No such type");
        }

        public static void Register<TDerived>(int id) where TDerived : T
        {
            var type = typeof(TDerived);
            dict.Add(id, type);
        }
    }
}