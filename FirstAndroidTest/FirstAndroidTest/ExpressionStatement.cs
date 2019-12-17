namespace FirstAndroidTest
{
    public class ExpressionStatement : Statement
    {
        public Expression Expression { get; private set; }
        public ExpressionStatement(Expression expression)
        {
            this.Expression = expression;
        }

        public override string AsJava(string indent)
        {
            return $"{Expression.AsJava()};";
        }
    }
}
