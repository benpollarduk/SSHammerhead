# InheritDocRewriter

Roslyn-based tool that rewrites repeated XML documentation on inheritable members
to `/// <inheritdoc/>` across the entire solution.

A member is considered **eligible** when it is any of:

- an `override` of a base member;
- an explicit interface implementation;
- an implicit interface implementation (verified via
  `INamedTypeSymbol.FindImplementationForInterfaceMember`, including inherited
  interfaces on sealed/record types);
- a constructor (incl. primary constructors) whose base type exposes a matching
  constructor signature carrying XML documentation.

For every eligible member that currently has a `///` doc comment, the whole
doc-comment trivia block (summary, params, returns, remarks, exceptions, etc.)
is replaced with a single `/// <inheritdoc/>` line. Members already using
`<inheritdoc/>` are left untouched.

## Run

```powershell
dotnet run --project tools/InheritDocRewriter -- "SSHammerhead.sln"
```

The project targets `net10.0` and uses `Microsoft.Build.Locator` to resolve the
installed SDK. It is intentionally **not** included in `SSHammerhead.sln`.

## Notes

- Generated files (`*.g.cs`, `*.Designer.cs`, anything under `obj/`) are skipped.
- Files are written back with their original encoding.
- The rewrite is idempotent; re-running the tool should produce no further changes.
- Review the diff before committing — the operation discards divergent doc text
  by design.
