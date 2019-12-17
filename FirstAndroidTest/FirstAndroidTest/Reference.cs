namespace FirstAndroidTest
{
    public class Reference : Expression
    {
        public Reference(string name)
        {
            this.Name = name;
        }

        public Expression Target { get; private set; }
        public string Name { get; private set; }
        public override string AsJava()
        {
            if (Target == null)
            {
                return Name;
            }
            else
            {
                return $"{Target.AsJava()}.{Name}";
            }
        }
        public static Reference This
        {
            get
            {
                return ThisReference.Instance;
            }
        }
        public static Reference Super
        {
            get
            {
                return SuperReference.Instance;
            }
        }
    }
}
