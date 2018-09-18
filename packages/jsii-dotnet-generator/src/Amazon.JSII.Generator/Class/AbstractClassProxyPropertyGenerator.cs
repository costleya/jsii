using System.Collections.Generic;
using Amazon.JSII.JsonModel.Spec;
using Microsoft.CodeAnalysis.CSharp;

namespace Amazon.JSII.Generator.Class
{
    public class AbstractClassProxyPropertyGenerator : ClassPropertyGenerator
    {
        public AbstractClassProxyPropertyGenerator(ClassType type, Property property, ISymbolMap symbols,
            INamespaceSet namespaces) : base(type, property, symbols, namespaces)
        {
        }

        protected override IEnumerable<SyntaxKind> GetModifierKeywords()
        {
            yield return Property.IsProtected == true ? SyntaxKind.ProtectedKeyword : SyntaxKind.PublicKeyword;
            // Abstract class proxies can only contain overwritten members.
            yield return SyntaxKind.OverrideKeyword;
        }
    }
}