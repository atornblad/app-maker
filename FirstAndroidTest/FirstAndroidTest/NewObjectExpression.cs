using System.Collections.Generic;
using System.Linq;

namespace FirstAndroidTest
{
    public class NewObjectExpression : Expression
    {
        public NewObjectExpression(string className, params Expression[] arguments)
        {
            this.ClassName = className;
            this.arguments.AddRange(arguments);
        }
        public string ClassName { get; private set; }
        private List<Expression> arguments = new List<Expression>();
        public Expression[] Arguments
        {
            get
            {
                return arguments.ToArray();
            }
        }
        public void AddArgument(Expression argument)
        {
            arguments.Add(argument);
        }

        public override string AsJava()
        {
            return $"new {ClassName}({string.Join(", ", arguments.Select(a => a.AsJava()))})";
        }
    }
}
