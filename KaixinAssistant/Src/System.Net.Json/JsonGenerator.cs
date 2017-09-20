using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom;
using System.Reflection;
using System.CodeDom.Compiler;
using System.IO;

namespace System.Net.Json
{
    public class JsonGenerator
    {
        // Methods
        public JsonGenerator()
        {
        }

        private void GenerateConstructorWithTextParameter(CodeTypeDeclaration classObject, JsonObject jsonObject)
        {
            CodeConstructor constructor = new CodeConstructor();
            classObject.Members.Add(constructor);
            constructor.Attributes = MemberAttributes.Public;
            constructor.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(string)), "text"));
            CodeVariableDeclarationStatement statement = new CodeVariableDeclarationStatement(typeof(JsonTextParser), "parser", new CodeObjectCreateExpression(new CodeTypeReference(typeof(JsonTextParser)), new CodeExpression[0]));
            constructor.Statements.Add(statement);
            CodeMethodInvokeExpression expression = new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodeVariableReferenceExpression("parser"), "Parse"), new CodeExpression[] { new CodeVariableReferenceExpression("text") });
            CodeAssignStatement statement2 = new CodeAssignStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "RootObject"), new CodeCastExpression(new CodeTypeReference(jsonObject.GetType()), expression));
            constructor.Statements.Add(statement2);
            constructor.Statements.Add(new CodeMethodReturnStatement());
        }

        private void GenerateDefaultConstructor(CodeTypeDeclaration classObject, JsonObject jsonObject)
        {
            CodeConstructor constructor = new CodeConstructor();
            classObject.Members.Add(constructor);
            constructor.Attributes = MemberAttributes.Public;
            CodeAssignStatement statement = new CodeAssignStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "RootObject"), new CodeObjectCreateExpression(jsonObject.GetType(), new CodeExpression[0]));
            constructor.Statements.Add(statement);
        }

        public void GenerateLibrary(string objectName, JsonObject jsonObject)
        {
            this.GenerateLibrary(objectName, jsonObject);
        }

        public void GenerateLibrary(string objectName, JsonObject jsonObject, string path)
        {
            CodeCompileUnit unit = new CodeCompileUnit();
            CodeNamespace namespace2 = new CodeNamespace("System.Net.Json.Generated");
            namespace2.Imports.Add(new CodeNamespaceImport("System.Net.Json"));
            unit.ReferencedAssemblies.Add("System.Net.Json.dll");
            unit.Namespaces.Add(namespace2);
            CodeTypeDeclaration declaration = new CodeTypeDeclaration(objectName);
            namespace2.Types.Add(declaration);
            declaration.TypeAttributes = TypeAttributes.Public;
            CodeMemberField field = new CodeMemberField(jsonObject.GetType(), "RootObject");
            field.Attributes = MemberAttributes.Family;
            declaration.Members.Add(field);
            this.GenerateToStringDefaultMethod(declaration);
            this.GenerateDefaultConstructor(declaration, jsonObject);
            this.GenerateConstructorWithTextParameter(declaration, jsonObject);
            this.GenerateParseStaticMethod(declaration);
            if (typeof(JsonObjectCollection) != jsonObject.GetType())
            {
                throw new NotImplementedException("Only objects supported in root level, not arrays or other variables.");
            }
            this.GenerateObjectCollection(declaration, (JsonObjectCollection)jsonObject);
            CodeDomProvider provider = CodeDomProvider.CreateProvider("cs");
            CompilerParameters options = new CompilerParameters();
            options.GenerateExecutable = false;
            if (!string.IsNullOrEmpty(path))
            {
                options.OutputAssembly = Path.ChangeExtension(Path.Combine(path, objectName), ".dll");
            }
            else
            {
                options.OutputAssembly = Path.ChangeExtension(objectName, ".dll");
            }
            options.IncludeDebugInformation = false;
            CompilerResults results = provider.CompileAssemblyFromDom(options, new CodeCompileUnit[] { unit });
            if (results.NativeCompilerReturnValue != 0)
            {
                throw new GeneratorException("Cannot compile your library.\r\nPlease send json text from which you trying to generate library to lazureykis@gmail.com", results);
            }
        }

        private void GenerateObjectCollection(CodeTypeDeclaration rootClass, JsonObjectCollection jsonObject)
        {
            CodeConstructor constructor = null;
            foreach (CodeTypeMember member in rootClass.Members)
            {
                CodeConstructor constructor2 = member as CodeConstructor;
                if ((constructor2 != null) && (constructor2.Parameters.Count == 0))
                {
                    constructor = constructor2;
                    break;
                }
            }
            if (constructor == null)
            {
                throw new Exception("Cannot find default constructor");
            }
            foreach (JsonObject obj2 in jsonObject)
            {
                CodeMemberProperty property = new CodeMemberProperty();
                property.Name = obj2.Name;
                property.Attributes = MemberAttributes.Public;
                PropertyInfo info = obj2.GetType().GetProperty("Value");
                if (info == null)
                {
                    throw new GeneratorException("Cannot generate nested arrays or objects. Wait for release :)");
                }
                property.Type = new CodeTypeReference(info.PropertyType);
                CodeExpression expression = new CodeArrayIndexerExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "RootObject"), new CodeExpression[] { new CodePrimitiveExpression(obj2.Name) });
                CodeExpression targetObject = new CodeCastExpression(obj2.GetType(), expression);
                CodeExpression expression3 = new CodeFieldReferenceExpression(targetObject, "Value");
                property.GetStatements.Add(new CodeMethodReturnStatement(expression3));
                property.SetStatements.Add(new CodeAssignStatement(expression3, new CodePropertySetValueReferenceExpression()));
                rootClass.Members.Add(property);
                CodeVariableDeclarationStatement statement = new CodeVariableDeclarationStatement(obj2.GetType(), obj2.Name.ToLower(), new CodeObjectCreateExpression(obj2.GetType(), new CodeExpression[] { new CodePrimitiveExpression(obj2.Name) }));
                constructor.Statements.Add(statement);
                CodeMethodInvokeExpression expression4 = new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "RootObject"), "Add"), new CodeExpression[] { new CodeVariableReferenceExpression(obj2.Name.ToLower()) });
                constructor.Statements.Add(expression4);
            }
        }

        private void GenerateParseStaticMethod(CodeTypeDeclaration classObject)
        {
            CodeMemberMethod method = new CodeMemberMethod();
            classObject.Members.Add(method);
            method.Name = "Parse";
            method.Attributes = MemberAttributes.Public | MemberAttributes.Static;
            method.ReturnType = new CodeTypeReference(classObject.Name);
            method.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(string)), "text"));
            method.Statements.Add(new CodeMethodReturnStatement(new CodeObjectCreateExpression(new CodeTypeReference("Person"), new CodeExpression[] { new CodeArgumentReferenceExpression("text") })));
        }

        private void GenerateToStringDefaultMethod(CodeTypeDeclaration classObject)
        {
            CodeMemberMethod method = new CodeMemberMethod();
            classObject.Members.Add(method);
            method.Name = "ToString";
            method.Attributes = MemberAttributes.Public | MemberAttributes.Overloaded | MemberAttributes.Override;
            method.ReturnType = new CodeTypeReference(typeof(string));
            CodeMethodReturnStatement statement = new CodeMethodReturnStatement(new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodeThisReferenceExpression(), "RootObject"), "ToString", new CodeExpression[0]));
            method.Statements.Add(statement);
        }
    }
}
