using System.Collections.Immutable;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace IfWithBrackets
{
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class IfWithBracketsAnalyzer
    : DiagnosticAnalyzer
{
 public const string DiagnosticId
     = "IfWithBrackets";
 
 private const string Category = "Style";
 
 private static readonly LocalizableString Title
     = new LocalizableResourceString(
         nameof(Resources.AnalyzerTitle),
         Resources.ResourceManager,
         typeof(Resources));
 
 private static readonly LocalizableString Message
     = new LocalizableResourceString(
         nameof(Resources.AnalyzerMessageFormat),
         Resources.ResourceManager,
         typeof(Resources));
 
 private static readonly LocalizableString Description
     = new LocalizableResourceString(
         nameof(Resources.AnalyzerDescription),
         Resources.ResourceManager,
         typeof(Resources));
 
 private static readonly DiagnosticDescriptor Rule
     = new DiagnosticDescriptor(
         DiagnosticId,
         Title,
         Message,
         Category,
         DiagnosticSeverity.Warning,
         isEnabledByDefault: true,
         description: Description);
 
 public override ImmutableArray<DiagnosticDescriptor>
     SupportedDiagnostics
     => ImmutableArray.Create(Rule);
 
 public override void Initialize(AnalysisContext context)
 {
  context.RegisterSyntaxNodeAction(
   nodeContext =>
   {
    var node = nodeContext.Node;
    var ifStatement = node as IfStatementSyntax;
    var statement = ifStatement?.Statement;
    if (statement is ExpressionStatementSyntax)
    {
     var diagnostic = Diagnostic.Create(
         Rule,
         statement.GetLocation());
     nodeContext
         .ReportDiagnostic(diagnostic);
    }
   },
   ImmutableArray.Create(SyntaxKind.IfStatement));
 }
}
}