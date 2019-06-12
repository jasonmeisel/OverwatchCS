using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using Microsoft.CodeAnalysis.CSharp.Syntax;

partial class Transpiler
{
    class MethodSubstituter : CSharpSyntaxRewriter
    {
        int m_methodCount = 0;
        StringWriter m_methodsSource = new StringWriter();
        SemanticModel m_semanticModel;
        Dictionary<string, string> m_generatedMethodToWorkshopCode;

        public MethodSubstituter(SemanticModel semanticModel, Dictionary<string, string> generatedMethodToWorkshopCode)
        {
            m_semanticModel = semanticModel;
            m_generatedMethodToWorkshopCode = generatedMethodToWorkshopCode;
        }

        public SyntaxTree GetGeneratedClass()
        {
            return SyntaxFactory.ParseSyntaxTree($"internal static class Generated {{ {m_methodsSource.ToString()} }}");
        }

        (string code, string[] paramTypes, ArgumentSyntax[] arguments) InvocationToWorkshopCode(InvocationExpressionSyntax invocation)
        {
            var argumentList = invocation.ArgumentList;

            var invocationMethodSymbol = GetMethodSemantics(invocation);
            var targetMethod = invocationMethodSymbol.OriginalDefinition;
            var targetCode = GetCodeName(targetMethod);
            if (targetCode != null)
            {
                var parameterTypes = targetMethod.Parameters.Select(p => GenericToConcrete(invocationMethodSymbol, p.Type).ToString()).ToList();
                var arguments = argumentList.Arguments.Select(a => a.Expression).ToList();

                // add support for defaults
                while (parameterTypes.Count != arguments.Count)
                {
                    var parameterSymbol = targetMethod.Parameters[arguments.Count];
                    Debug.Assert(parameterSymbol.HasExplicitDefaultValue);
                    var paramType = parameterSymbol.Type;
                    if (paramType.TypeKind == TypeKind.Enum)
                    {
                        var paramEnumType = GetWorkshopType(paramType.ToString());
                        var enumName = paramEnumType.GetEnumName(parameterSymbol.ExplicitDefaultValue);
                        arguments.Add(SyntaxFactory.ParseExpression($"{paramEnumType.FullName}.{enumName}"));
                    }
                    else
                        arguments.Add(SyntaxFactory.ParseExpression(parameterSymbol.ExplicitDefaultValue?.ToString() ?? "null"));
                }

                // test for extension method
                // TODO: support struct methods?
                var isStatic = targetMethod.IsStatic;
                if (!isStatic && invocation.Expression is MemberAccessExpressionSyntax memberAccess)
                {
                    arguments.Insert(0, memberAccess.Expression);
                    parameterTypes.Insert(0, targetMethod.ReceiverType.ToString());
                }

                if (arguments.Count() != 0)
                {
                    var argCodes = arguments.Zip(parameterTypes, (a, p) =>
                    {
                        switch (a)
                        {
                            case InvocationExpressionSyntax i:
                                return InvocationToWorkshopCode(i);
                            case LiteralExpressionSyntax literal:
                                return (
                                    code: literal.ToString(),
                                    paramTypes: new string[0],
                                    arguments: new ArgumentSyntax[0]
                                );
                            case MemberAccessExpressionSyntax enumAccess:
                                var code = a.SyntaxTree == m_semanticModel.SyntaxTree ?
                                    GetCodeName(m_semanticModel.GetSymbolInfo(a).Symbol) :
                                    GetCodeName(GetWorkshopType(enumAccess.Expression.ToString()).GetMember(enumAccess.Name.ToString()).FirstOrDefault());
                                if (code != null)
                                {
                                    return (code, paramTypes: new string[0], arguments: new ArgumentSyntax[0]);
                                }
                                break;
                        }

                        return (
                            code: "<PARAM>",
                            paramTypes: new[] { p },
                            arguments: new[] { SyntaxFactory.Argument(a) }
                        );
                    }).ToArray();

                    return (
                        $"{targetCode}({argCodes.Select(a => a.code).ListToString()})",
                        argCodes.SelectMany(a => a.paramTypes).ToArray(),
                        argCodes.SelectMany(a => a.arguments).ToArray()
                    );
                }
                return (targetCode, new string[0], new ArgumentSyntax[0]);
            }
            throw null;
        }

        // if paramType is one of the generic types, swap it with the concrete type that's being used
        static ITypeSymbol GenericToConcrete(IMethodSymbol targetMethod, ITypeSymbol paramType)
        {
            return targetMethod.TypeParameters.Zip(targetMethod.TypeArguments).
                FirstOrDefault(paramArg => paramArg.First.Name == paramType.Name).Second ?? paramType;
        }

        public override SyntaxNode VisitInvocationExpression(InvocationExpressionSyntax invocation)
        {
            var argumentList = invocation.ArgumentList;

            var targetMethod = GetMethodSemantics(invocation);
            var targetCode = GetCodeName(targetMethod);
            if (targetCode != null)
            {
                var baseMethodName = string.Join("", targetMethod.Name.Where(ch => char.IsLetterOrDigit(ch)));
                var generatedMethodName = $"Impl_{baseMethodName}_{m_methodCount++}";
                var workshopCode = InvocationToWorkshopCode(invocation);
                m_generatedMethodToWorkshopCode[generatedMethodName] = workshopCode.code;

                var methodSemantics = (Microsoft.CodeAnalysis.IMethodSymbol)m_semanticModel.GetSymbolInfo(invocation).Symbol;
                var returnType = methodSemantics.ReturnType;
                var paramListSource = workshopCode.paramTypes.Select((type, i) => $"{type} arg{i}").ListToString();
                var methodSource = $"internal static {returnType} {generatedMethodName} ({paramListSource}) => throw null;";
                m_methodsSource.WriteLine(methodSource);
                m_methodsSource.WriteLine();

                Console.WriteLine(methodSource);

                var arguments = SyntaxFactory.SeparatedList(workshopCode.arguments.Select(a => base.Visit(a)));
                return SyntaxFactory.InvocationExpression(
                    SyntaxFactory.MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        SyntaxFactory.IdentifierName("Generated"),
                        SyntaxFactory.IdentifierName(generatedMethodName)),
                    SyntaxFactory.ArgumentList(arguments));
            }

            return base.VisitInvocationExpression(invocation);
        }

        IMethodSymbol GetMethodSemantics(InvocationExpressionSyntax invocation)
        {
            switch (invocation.Expression)
            {
                case MemberAccessExpressionSyntax memberAccess:
                    return (IMethodSymbol)m_semanticModel.GetSymbolInfo(memberAccess.Name).Symbol;
                default:
                    Console.WriteLine(invocation);
                    Console.WriteLine(invocation.Expression);
                    var symbolInfo = m_semanticModel.GetSymbolInfo(invocation.Expression);
                    return symbolInfo.Symbol as IMethodSymbol;
            }
        }
    }
}
