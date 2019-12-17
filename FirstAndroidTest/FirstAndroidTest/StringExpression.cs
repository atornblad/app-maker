using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;

namespace FirstAndroidTest
{
    public class StringExpression : LiteralExpression<string>
    {
        public StringExpression(string value) : base(value)
        {
        }

        public override string AsJava()
        {
            using (var writer = new StringWriter())
            {
                using (var provider = CodeDomProvider.CreateProvider("CSharp"))
                {
                    provider.GenerateCodeFromExpression(new CodePrimitiveExpression(Value), writer, null);
                    return writer.ToString();
                }
            }
        }
    }
}
