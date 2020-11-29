namespace ExpressionEval
{
    /// <summary>
    /// Interface for the Visitor Pattern
    /// </summary>
    public interface IVisitor
    {
        object Visit(Literal literal);
        object Visit(Negation negation);
        object Visit(Addition addition);
        object Visit(Subtraction subtraction);
        object Visit(Multiplication multiplication);
        object Visit(Division division);

    }
}