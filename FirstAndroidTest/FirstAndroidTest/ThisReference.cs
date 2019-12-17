namespace FirstAndroidTest
{
    public class ThisReference : Reference
    {
        public static ThisReference Instance { get; } = new ThisReference();

        private ThisReference() : base("this")
        {

        }

        public override string AsJava()
        {
            return "this";
        }
    }
}
