namespace FirstAndroidTest
{
    public class AndroidJavaCodeGenerator : JavaCodeGenerator
    {
        private string package;

        public AndroidJavaCodeGenerator(string package)
        {
            this.package = package;
        }

        public override string PackageName => package;
    }
}
