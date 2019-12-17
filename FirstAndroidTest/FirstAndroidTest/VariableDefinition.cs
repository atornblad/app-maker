namespace FirstAndroidTest
{
    public class VariableDefinition
    {
        public VariableDefinition(string type, string name)
        {
            Type = type;
            Name = name;
        }

        public VariableDefinition(string type, string name, Expression initialValue)
        {
            Type = type;
            Name = name;
            InitialValue = initialValue;
        }

        public string Name { get; private set; }
        public string Type { get; private set; }
        public MemberProtectionLevel Protection { get; private set; }
        public Expression InitialValue { get; private set; }

        public string AsInstanceVariable()
        {
            if (InitialValue == null)
            {
                return $"{Protection.ToString().ToLowerInvariant()} {Type} {Name};";
            }
            else
            {
                return $"{Protection.ToString().ToLowerInvariant()} {Type} {Name} = {InitialValue.AsJava()};";
            }
        }

        public string AsParameter()
        {
            return $"{Type} {Name}";
        }

        public string AsInline()
        {
            if (InitialValue == null)
            {
                return $"{Type} {Name};";
            }
            else
            {
                return $"{Type} {Name} = {InitialValue.AsJava()};";
            }
        }
        public static implicit operator Statement(VariableDefinition value)
        {
            return new VariableDefinitionStatement(value);
        }
    }
}
