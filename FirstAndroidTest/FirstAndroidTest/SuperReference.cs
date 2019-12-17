namespace FirstAndroidTest
{
    public class SuperReference : Reference
    {
        public static SuperReference Instance { get; } = new SuperReference();

        private SuperReference() : base("super")
        {

        }

        public override string AsJava()
        {
            return "super";
        }
    }
}
