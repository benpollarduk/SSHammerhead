using Microsoft.CodeAnalysis;

namespace InheritDocRewriter;

internal static class InheritDocEligibility
{
    /// <summary>
    /// Returns true if the given member symbol has XML documentation it can inherit from a base type or implemented interface.
    /// </summary>
    public static bool IsEligible(ISymbol symbol)
    {
        if (symbol is null)
            return false;

        switch (symbol)
        {
            case IMethodSymbol method:
                return IsMethodEligible(method);
            case IPropertySymbol property:
                return IsPropertyEligible(property);
            case IEventSymbol evt:
                return IsEventEligible(evt);
            default:
                return false;
        }
    }

    private static bool IsMethodEligible(IMethodSymbol method)
    {
        if (method.IsOverride)
            return true;

        if (!method.ExplicitInterfaceImplementations.IsDefaultOrEmpty)
            return true;

        if (method.MethodKind == MethodKind.Constructor)
            return IsConstructorEligible(method);

        if (method.MethodKind is MethodKind.Ordinary or MethodKind.PropertyGet or MethodKind.PropertySet
            or MethodKind.EventAdd or MethodKind.EventRemove)
        {
            return ImplementsInterfaceMember(method);
        }

        return false;
    }

    private static bool IsPropertyEligible(IPropertySymbol property)
    {
        if (property.IsOverride)
            return true;
        if (!property.ExplicitInterfaceImplementations.IsDefaultOrEmpty)
            return true;
        return ImplementsInterfaceMember(property);
    }

    private static bool IsEventEligible(IEventSymbol evt)
    {
        if (evt.IsOverride)
            return true;
        if (!evt.ExplicitInterfaceImplementations.IsDefaultOrEmpty)
            return true;
        return ImplementsInterfaceMember(evt);
    }

    private static bool ImplementsInterfaceMember(ISymbol memberSymbol)
    {
        var containingType = memberSymbol.ContainingType;
        if (containingType is null)
            return false;

        foreach (var iface in containingType.AllInterfaces)
        {
            foreach (var ifaceMember in iface.GetMembers())
            {
                var impl = containingType.FindImplementationForInterfaceMember(ifaceMember);
                if (impl is not null && SymbolEqualityComparer.Default.Equals(impl, memberSymbol))
                    return true;
            }
        }

        return false;
    }

    private static bool IsConstructorEligible(IMethodSymbol ctor)
    {
        var baseType = ctor.ContainingType?.BaseType;
        if (baseType is null)
            return false;

        foreach (var baseCtor in baseType.Constructors)
        {
            if (ParametersMatch(baseCtor.Parameters, ctor.Parameters)
                && !string.IsNullOrWhiteSpace(baseCtor.GetDocumentationCommentXml()))
            {
                return true;
            }
        }

        return false;
    }

    private static bool ParametersMatch(
        System.Collections.Immutable.ImmutableArray<IParameterSymbol> a,
        System.Collections.Immutable.ImmutableArray<IParameterSymbol> b)
    {
        if (a.Length != b.Length)
            return false;
        for (int i = 0; i < a.Length; i++)
        {
            if (!SymbolEqualityComparer.Default.Equals(a[i].Type, b[i].Type))
                return false;
        }
        return true;
    }
}
