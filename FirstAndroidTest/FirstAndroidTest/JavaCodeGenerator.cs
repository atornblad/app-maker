using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FirstAndroidTest
{
    public class JavaCodeGenerator
    {
        private Random random = new Random();
        private HashSet<string> uniqueNames = new HashSet<string>();
        private double max = 1.999;
        private char[] uniqueNameChars = "abcdefghijklmnop".ToCharArray();
        public string CreateUniqueName()
        {
            string value;
            do
            {
                byte[] bytes = BitConverter.GetBytes(random.Next(0, (int)max)).Reverse().ToArray();
                value = string.Join("", bytes.Select(b => new String(new[] { uniqueNameChars[(b >> 4)], uniqueNameChars[(b & 15)] })));
                while (value.StartsWith("a") && value != "a") value = value.Substring(1);
                if (uniqueNames.Contains(value))
                {
                    max *= 1.5;
                }
            } while (uniqueNames.Contains(value));
            uniqueNames.Add(value);
            return value;
        }
        public virtual string PackageName { get; private set; }
        public virtual string ClassName { get; private set; }
        public virtual string ExtendsClassName { get; private set; }

        public void Write(TextWriter writer)
        {
            writer.WriteLine($"package {PackageName};");
            writer.Write($"public class {ClassName}");
            if (!string.IsNullOrEmpty(ExtendsClassName))
            {
                writer.Write($" extends {ExtendsClassName}");
            }
            writer.WriteLine(" {");

            foreach (var instanceVariable in EnumerateInstanceVariables())
            {
                writer.WriteLine($"    {instanceVariable.AsInstanceVariable()}");
            }

            foreach (var method in EnumerateMethods())
            {
                writer.WriteLine($"    {method.AsJava(4)}");
            }

            writer.WriteLine("}");
        }

        public virtual IEnumerable<VariableDefinition> EnumerateInstanceVariables()
        {
            yield break;
        }

        public virtual IEnumerable<Method> EnumerateMethods()
        {
            yield break;
        }
    }
}
