namespace FirstAndroidTest
{
    public class VariableDefinitionStatement : Statement
    {
        public VariableDefinition Variable { get; private set; }
        public VariableDefinitionStatement(VariableDefinition variable)
        {
            this.Variable = variable;
        }

        public override string AsJava(string indent)
        {
            return $"{Variable.AsInline()}";
        }
    }
}
