namespace FirstAndroidTest
{
    public abstract class LiteralExpression<T> : Expression
    {
        protected LiteralExpression(T value)
        {
            Value = value;
        }

        public T Value { get; private set; }
    }
}
