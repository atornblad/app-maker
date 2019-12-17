using System.Collections.Generic;
using System.Linq;

namespace FirstAndroidTest
{
    public class MethodCallExpression : Expression
    {
        public MethodCallExpression(Reference target, string methodName, params Expression[] arguments)
        {
            this.Target = target;
            this.MethodName = methodName;
            this.arguments.AddRange(arguments);
        }

        public string MethodName { get; private set; }
        public Reference Target { get; private set; }
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
            if (Target == Reference.This)
            {
                return $"{MethodName}({string.Join(", ", arguments.Select(a => a.AsJava()))})";
            }
            else
            {
                return $"{Target.AsJava()}.{MethodName}({string.Join(", ", arguments.Select(a => a.AsJava()))})";
            }
        }
    }
}
