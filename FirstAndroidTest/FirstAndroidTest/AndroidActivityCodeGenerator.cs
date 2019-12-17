using System.Collections.Generic;

namespace FirstAndroidTest
{
    public class AndroidActivityCodeGenerator : AndroidJavaCodeGenerator
    {
        private Page page;

        public AndroidActivityCodeGenerator(Page page, string package) : base(package)
        {
            this.page = page;
        }

        public override string ClassName => $"{page.Id.UpperFirst()}Activity";

        public override string ExtendsClassName => "android.app.Activity";

        public override IEnumerable<Method> EnumerateMethods()
        {
            yield return CreateActivityOnCreateOverride(page);
        }

        private Method CreateActivityOnCreateOverride(Page page)
        {
            var method = new Method(MemberProtectionLevel.Public, "void", "onCreate", true);
            method.AddParameter("android.os.Bundle", "savedInstanceState");
            method.Body.Add(new MethodCallExpression(Reference.Super, "onCreate", new Reference("savedInstanceState")));

            string mainViewName = CreateUniqueName();
            method.Body.AddRange(page.GenerateViewCreation(mainViewName));
            method.Body.Add(new MethodCallExpression(Reference.This, "setContentView", new Reference(mainViewName)));
            
            return method;
        }
    }
}
