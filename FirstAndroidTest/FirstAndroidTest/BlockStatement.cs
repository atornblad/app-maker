using System.Collections.Generic;
using System.Linq;

namespace FirstAndroidTest
{
    public class BlockStatement : Statement
    {
        private List<Statement> statements = new List<Statement>();

        public BlockStatement(params Statement[] statements)
        {
            this.statements.AddRange(statements);
        }

        public void Add(Statement statement)
        {
            statements.Add(statement);
        }

        public void AddRange(IEnumerable<Statement> statements)
        {
            this.statements.AddRange(statements);
        }

        public override string AsJava(string indent)
        {
            string innerIndent = $"{indent}    ";
            return $"{{\n{string.Join("\n", statements.Select(s => innerIndent + s.AsJava(innerIndent)))}\n{indent}}}";
        }
    }
}
