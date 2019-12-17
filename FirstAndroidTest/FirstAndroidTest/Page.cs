using System.Collections.Generic;

namespace FirstAndroidTest
{
    public abstract class Page
    {
        public string Id { get; private set; }

        public Page(string id)
        {
            Id = id;
        }

        internal abstract IEnumerable<Statement> GenerateViewCreation(string mainViewName);
    }
}
