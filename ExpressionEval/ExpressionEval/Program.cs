using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionEval
{
    /// <summary>
    /// Exceptions used to handle incorrect behavior
    /// </summary>
    #region Exceptions
    public class EvaluationException : Exception {}
    public class FormatError : EvaluationException{}
    public class DivideError : EvaluationException{}
    public class OverflowError : EvaluationException{}
    #endregion

    class Program
    {
        /// <summary>
        /// First parses the expression using stack
        /// Then it evaluates each expression using the Visitor Pattern
        /// </summary>
        /// <param name="expression"></param>
        private static void EvaluateExpression(string expression)
        {
            try
            {
                Parser parser = new Parser();
                Expression root = parser.ParseInput(expression.Split());

                Evaluator evaluator = new Evaluator(1);
                string result = evaluator.EvaluateAll(evaluator, root);

                Console.WriteLine(result);
            }
            catch (EvaluationException e) 
            {
                switch (e)
                {
                    case FormatError _:
                        Console.WriteLine("Format Error");
                        break;
                    case DivideError _:
                        Console.WriteLine("Divide Error");
                        break;
                    case OverflowError _:
                        Console.WriteLine("Overflow Error");
                        break;
                }
            }
        }

        /// <summary>
        /// Simple parameter check and passing input to the main execution method
        /// </summary>
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            if (input == null)
            {
                return;
            }

            EvaluateExpression(input);
        }
    }
}
