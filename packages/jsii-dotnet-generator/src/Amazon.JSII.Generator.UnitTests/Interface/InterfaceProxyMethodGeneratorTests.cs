﻿using Amazon.JSII.Generator.Interface;
using Amazon.JSII.JsonModel.Spec;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Xunit;
using TypeKind = Amazon.JSII.JsonModel.Spec.TypeKind;

namespace Amazon.JSII.Generator.UnitTests.Interface
{
    public class InterfaceProxyMethodGeneratorTests : GeneratorTestBase
    {
        const string Prefix = nameof(Generator) + "." + nameof(InterfaceProxyMethodGenerator) + ".";

        string Render(Method method)
        {
            InterfaceType interfaceType = new InterfaceType
            (
                "myInterfaceFqn",
                "myModule",
                "myClass",
                "myNamespace",
                methods: new Method[] { method }
            );

            Symbols.MapTypeToPackage("myInterfaceFqn", "myPackage");
            Symbols.MapNamespace("", "MyNamespace");
            Symbols.MapTypeName("myInterfaceFqn", "MyInterface", TypeKind.Interface);

            var generator = new InterfaceProxyMethodGenerator(interfaceType, method, Symbols, Namespaces);

            MethodDeclarationSyntax methodSyntax = generator.CreateMethod();
            return methodSyntax.NormalizeWhitespace(elasticTrivia: true).ToString();
        }

        [Fact(DisplayName = Prefix + nameof(IncludesAttribute))]
        public void IncludesAttribute()
        {
            Method method = new Method(false, false, true, name: "myMethod");

            Symbols.MapMethodName("myInterfaceFqn", "myMethod", "MyMethod");

            string actual = Render(method);
            string expected =
@"[JsiiMethod(""myMethod"", null, ""[]"")]
public void MyMethod()
{
    InvokeInstanceVoidMethod(new object[]{});
}";
            Assert.Equal(expected, actual, ignoreLineEndingDifferences: true);
        }

        [Fact(DisplayName = Prefix + nameof(IncluesParameters))]
        public void IncluesParameters()
        {
            Method method = new Method
            (
                false, false, true, name: "myMethod",
                parameters: new[]
                {
                    new Parameter("myParam", new TypeReference("myParamTypeFqn")),
                    new Parameter("event", new TypeReference(primitive: PrimitiveType.String))
                }
            );

            Symbols.MapMethodName("myInterfaceFqn", "myMethod", "MyMethod");
            Symbols.MapTypeName("myParamTypeFqn", "MyParamType", TypeKind.Class);
            Symbols.MapParameterName("myParam", "myParam");
            Symbols.MapParameterName("event", "@event");

            string actual = Render(method);
            string expected =
@"[JsiiMethod(""myMethod"", null, ""[{\""name\"":\""myParam\"",\""type\"":{\""fqn\"":\""myParamTypeFqn\""}},{\""name\"":\""event\"",\""type\"":{\""primitive\"":\""string\""}}]"")]
public void MyMethod(MyParamType myParam, string @event)
{
    InvokeInstanceVoidMethod(new object[]{myParam, @event});
}";
            Assert.Equal(expected, actual, ignoreLineEndingDifferences: true);
        }

        [Fact(DisplayName = Prefix + nameof(DoesNotIncludeDocs))]
        public void DoesNotIncludeDocs()
        {
            Method method = new Method
            (
                false, false, true, name: "myMethod",
                docs: new Docs { { "foo", "bar" } }
            );

            Symbols.MapMethodName("myInterfaceFqn", "myMethod", "MyMethod");

            string actual = Render(method);
            string expected =
@"[JsiiMethod(""myMethod"", null, ""[]"")]
public void MyMethod()
{
    InvokeInstanceVoidMethod(new object[]{});
}";
            Assert.Equal(expected, actual, ignoreLineEndingDifferences: true);
        }

        [Fact(DisplayName = Prefix + nameof(IncludesReturnTypeForNonVoid))]
        public void IncludesReturnTypeForNonVoid()
        {
            Method method = new Method
            (
                false, false, true, name: "myMethod",
                returns: new TypeReference("myReturnTypeFqn")
            );

            Symbols.MapMethodName("myInterfaceFqn", "myMethod", "MyMethod");
            Symbols.MapTypeName("myReturnTypeFqn", "MyReturnType", TypeKind.Class);

            string actual = Render(method);
            string expected =
@"[JsiiMethod(""myMethod"", ""{\""fqn\"":\""myReturnTypeFqn\""}"", ""[]"")]
public MyReturnType MyMethod()
{
    return InvokeInstanceMethod<MyReturnType>(new object[]{});
}";
            Assert.Equal(expected, actual, ignoreLineEndingDifferences: true);
        }
    }
}
