namespace FirstAndroidTest
{
    public abstract class Expression
    {
        public abstract string AsJava();
        public static implicit operator Statement(Expression value)
        {
            return new ExpressionStatement(value);
        }

        public static implicit operator Expression(string value)
        {
            return new StringExpression(value);
        }
    }
}
