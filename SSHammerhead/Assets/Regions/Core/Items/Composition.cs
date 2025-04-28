using System.Collections.Generic;

namespace SSHammerhead.Assets.Regions.Core.Items
{
    /// <summary>
    /// Represents an objects composition.
    /// </summary>
    /// <param name="Name">The name of the object.</param>
    /// <param name="Elements">The composition of the object.</param>
    public record Composition(string Name, Dictionary<string, float> Elements);
}
