using NetAF.Assets;
using NetAF.Utilities;

namespace SSHammerhead.Commands.Helpers
{
    /// <summary>
    /// Provides an identifier for an item template.
    /// </summary>
    /// <param name="Name">The name of the item.</param>
    /// <param name="Item">The item template.</param>
    internal record ItemTemplateIdentifier(string Name, IAssetTemplate<Item> Item);
}
