using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ExpressionEval
{
    /// <summary>
    /// Main Visitor Pattern interface implementation
    /// </summary>
    public class Evaluator : IVisitor
    {
        private readonly int type;

        public Evaluator(int type)
        {
            // Register used types into the ValueFactory
            ValueFactory<object>.Register<int>(0);
            ValueFactory<object>.Register<double>(1);
            this.type = type;
        }

        /// <summary>
        /// Using the Visitor Pattern does DFS, visits all the Expressions and evaluates the root
        /// </summary>
        /// <returns>Result of the main Expression</returns>
        public string EvaluateAll(IVisitor visitor, Expression root)
        {
            return root.Accept(visitor).ToString();
        }

        /// <summary>
        /// First DFS to get the value of children and cast them into the right type
        /// Using the Func delegate that takes two inputs(in our case) and returns them as the result of operation defined with the third parameter
        /// It's disgusting, I know
        /// </summary>
        private object Evaluate(BinaryExpression expression)
        {
            try
            {
                var left = ValueFactory<object>.GetValue(type, expression.Left.Accept(this));
                var right = ValueFactory<object>.GetValue(type, expression.Right.Accept(this));

                if (left is int && right is int)
                {
                    Func<int, int, int> function = (a, b) => throw new InvalidOperationException();

                    var @switch = new Dictionary<Type, Action>
                    {
                        {typeof(Addition), () => function = (a, b) => checked((int) left + (int) right)},
                        {typeof(Subtraction), () => function = (a, b) => checked((int) left - (int) right)},
                        {typeof(Multiplication), () => function = (a, b) => checked((int) left * (int) right)},
                        {typeof(Division), () => function = (a, b) => checked((int) left / (int) right)},
                    };

                    @switch[expression.GetType()]();

                    return function;
                }

                if (left is double && right is double)
                {
                    Func<double, double, double> function = (a, b) => throw new InvalidOperationException();

                    var @switch = new Dictionary<Type, Action>()
                    {
                        {typeof(Addition), () => function = (a, b) => checked((double) left + (double) right)},
                        {typeof(Subtraction), () => function = (a, b) => checked((double) left - (double) right)},
                        {typeof(Multiplication), () => function = (a, b) => checked((double) left * (double) right)},
                        {typeof(Division), () => function = (a, b) => checked((double) left / (double) right)},
                    };

                    @switch[expression.GetType()]();

                    return function;
                }

                throw new InvalidOperationException();
            }
            catch (DivideByZeroException)
            {
                throw new DivideError();
            }
            catch (OverflowException)
            {
                throw new OverflowError();
            }
        }

        private object Evaluate(UnaryExpression expression)
        {
            var target = ValueFactory<object>.GetValue(type,expression.Target.Accept(this));
            if (target is int)
            {
                Func<int, int> function = x => throw new InvalidOperationException();
                
                var @switch = new Dictionary<Type, Action>
                {
                    { typeof(Negation), () => function = x => -(int)target }
                };

                @switch[expression.GetType()]();

                return function;

            }
            if (target is double)
            {
                Func<double, double> function = x => throw new InvalidOperationException();

                var @switch = new Dictionary<Type, Action>
                {
                    { typeof(Negation), () => function = x => -(double)target }
                };

                @switch[expression.GetType()]();

                return function;
            }
            throw new InvalidOperationException();
        }

        /// <summary>
        /// Returns just the Literal value
        /// Stops the DFS evaluation
        /// </summary>
        public object Visit(Literal literal)
        {
            System.Linq.Expressions.BinaryExpression
            return ValueFactory<object>.GetValue(type, literal.Value);
        }
        
        /// <summary>
        /// Negates the target Expression
        /// </summary>
        public object Visit(Negation negation)
        {
            return Evaluate(negation);
        }
        
        /// <summary>
        /// Addition using Func
        /// </summary>
        public object Visit(Addition addition)
        {
            return Evaluate(addition);
        }

        /// <summary>
        /// Subtraction using Func
        /// </summary>
        public object Visit(Subtraction subtraction)
        {
            return Evaluate(subtraction);
        }

        /// <summary>
        /// Multiplication using Func
        /// </summary>
        public object Visit(Multiplication multiplication)
        {
            return Evaluate(multiplication);
        }

        /// <summary>
        /// Division using Func
        /// Uses anonymous method to check the divisor and call the Func delegate
        /// </summary>
        public object Visit(Division division)
        {
            return Evaluate(division);
        }
    }
}