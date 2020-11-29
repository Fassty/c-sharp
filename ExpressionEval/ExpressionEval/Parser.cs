using System;
using System.Collections.Generic;
using System.IO;

namespace ExpressionEval
{
    class Parser
    {
        /// <summary>
        /// Parses the input line by pushing elements to the Stack
        /// The stack guarantees that the expressions will be evaluated in the correct order
        /// </summary>
        public Expression ParseInput(string[] terms)
        {
            Expression result = null;
            var unresolved = new Stack<Expression>();

            foreach (var term in terms)
            {
                if (result != null
                ) // We have the result before we have evaluated the whole expression, that can't be right
                {
                    throw new FormatError();
                }

                // Try to get Expression from ExpressionFactory
                Expression expression = ExpressionFactory.GetByOperator(term[0]);

                // If Factory returned a non-null value, we have an operator
                if (expression != null)
                {
                    unresolved.Push(expression);
                }
                // Else valuates the stack
                // All Expressions on the stack are null by default
                // So after evaluating the stack we get:
                // 1. An expression with two children, but one is still null, because we still need to evaluate the other branch
                // 2. An expression with two(or one for negation) children both non-null
                // But case 2 means that we have reached the root, since there are no other non-evaluated Expressions
                else
                {
                    if (!int.TryParse(term, out int value)) // Check for literal
                    {
                        throw new FormatError();
                    }

                    expression = new Literal(value);

                    while (unresolved.Count > 0)
                    {
                        var top = unresolved.Peek();
                        if (top.AddOperand(expression))
                        {
                            unresolved.Pop();
                            expression = top;
                        }
                        else
                        {
                            expression = null;
                            break;
                        }
                    }

                    if (expression != null)
                    {
                        result = expression;
                    }
                }
            }

            return result;
        }
    }
}