using System.Collections.Generic;
using Amazon.JSII.JsonModel.Spec;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SF = Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Amazon.JSII.Generator.Class
{
    public class AbstractClassProxyMethodGenerator : ClassMethodGenerator
    {
        public AbstractClassProxyMethodGenerator(ClassType type, Method method, ISymbolMap symbols,
            INamespaceSet namespaces) : base(type, method, symbols, namespaces)
        {
        }

        protected override IEnumerable<SyntaxKind> GetModifierKeywords()
        {
            yield return Method.IsProtected ? SyntaxKind.ProtectedKeyword : SyntaxKind.PublicKeyword;
            // Abstract class proxies can only contain overwritten members.
            yield return SyntaxKind.OverrideKeyword;
        }

        protected override BlockSyntax GetBody()
        {
            if (Method.Returns == null)
            {
                return SF.Block(SF.ExpressionStatement(CreateInvocationExpression()));
            }

            return SF.Block(SF.ReturnStatement(CreateInvocationExpression()));
        }

        protected override bool HasSemicolon => false;
    }
}