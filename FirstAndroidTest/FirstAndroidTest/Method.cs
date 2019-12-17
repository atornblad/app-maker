using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FirstAndroidTest
{
    public class Method
    {
        public string Name { get; private set; }
        public string ReturnType { get; private set; }
        public MemberProtectionLevel Protection { get; private set; }
        public bool IsOverride { get; private set; }
        private List<VariableDefinition> parameters = new List<VariableDefinition>();
        public BlockStatement Body { get; private set; }

        public Method(MemberProtectionLevel protection, string type, string name, bool isOverride)
        {
            this.Protection = protection;
            this.ReturnType = type;
            this.Name = name;
            this.IsOverride = isOverride;
            this.Body = new BlockStatement();
        }

        public VariableDefinition[] Parameters
        {
            get
            {
                return parameters.ToArray();
            }
        }
        public void AddParameter(string type, string name)
        {
            parameters.Add(new VariableDefinition(type, name));
        }

        public string AsJava(int indentation)
        {
            string indent = new string(' ', indentation);
            var builder = new StringBuilder();
            if (IsOverride)
            {
                builder.AppendLine("@Override");
                builder.Append(indent);
            }
            builder.Append($"{Protection.ToString().ToLowerInvariant()} {ReturnType} {Name}({string.Join(", ", parameters.Select(p => p.AsParameter()))}) ");
            builder.Append(Body.AsJava(indent));

            return builder.ToString();
        }
    }
}
