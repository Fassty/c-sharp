
using System;

namespace ExpressionEval
{
    /// <summary>
    /// All Expressions must implement the Accept method for Visitor Pattern
    /// </summary>
    public interface IExpression
    {
        object Accept(IVisitor visitor);
    }

    /// <summary>
    /// General Expression class(I need this class alongside with the interface, since I can't assign interface into the operands)
    /// </summary>
    public abstract class Expression : IExpression
    {
        /// <summary>
        /// Tries to complete the Expression by adding an operand
        /// </summary>
        public abstract bool AddOperand(Expression operand);

        public abstract object Accept(IVisitor visitor);
    }

    /// <summary>
    /// Defines the BinaryExpression format
    /// </summary>
    public abstract class BinaryExpression : Expression
    {
        public Expression Left { get; private set; }
        public Expression Right { get; private set; }

        public override bool AddOperand(Expression operand)
        {
            if (Left == null)
            {
                Left = operand;
                return false;
            }
            if (Right == null)
            {
                Right = operand;
            }

            return true;
        }
    }

    /// <summary>
    /// Defines the unary Expression format
    /// </summary>
    public abstract class UnaryExpression : Expression
    {
        private Expression target;
        public Expression Target
        {
            get => target;
            private set
            {
                if (target == null)
                {
                    target = value;
                }
            }
        }

        public override bool AddOperand(Expression operand)
        {
            Target = operand;
            return true;
        }
    }

    // This region implements the Accept method for Visitor Pattern
    // All the Accept method does is calling the Visit method from interface with the given object
    #region Expressions

    /// <summary>
    /// Holds only its value
    /// </summary>
    public class Literal : Expression
    {
        public object Value { get; }

        public Literal(int value)
        {
            this.Value = value;
        }

        // No reason to call this method
        public override bool AddOperand(Expression operand)
        {
            throw new System.NotImplementedException();
        }

        public override object Accept(IVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }

    public class Negation : UnaryExpression
    {
        public override object Accept(IVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }

    public class Addition : BinaryExpression
    {
        public override object Accept(IVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }

    public class Subtraction : BinaryExpression
    {
        public override object Accept(IVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }

    public class Multiplication : BinaryExpression
    {
        public override object Accept(IVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }

    public class Division : BinaryExpression
    {
        public override object Accept(IVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
    #endregion
}