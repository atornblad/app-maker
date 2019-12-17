using System.Collections.Generic;

namespace FirstAndroidTest
{
    public class SimpleTextPage : Page
    {
        public string Text { get; private set; }

        public SimpleTextPage(string id, string text) : base(id)
        {
            Text = text;
        }

        internal override IEnumerable<Statement> GenerateViewCreation(string mainViewName)
        {
            yield return new VariableDefinition("android.widget.TextView", mainViewName, new NewObjectExpression("android.widget.TextView", Reference.This));
            yield return new MethodCallExpression(new Reference(mainViewName), "setText", Text);
        }
    }
}
