using System.Collections.Generic;
using GetLinksAnywhere.Common.Helpers;

// ReSharper disable IdentifierTypo

namespace GetLinksAnywhere.Common.Classes
{
    // ReSharper disable once InconsistentNaming
    public static class IANASettings
    {
        static IANASettings()
        {
            var domains = EmbeddedResourceHelper.ReadDomainsSettings()
                .ConfigureAwait(false);

            var schemes = EmbeddedResourceHelper.ReadSchemesSettings()
                .ConfigureAwait(false);

            Domains = new HashSet<string>(domains.GetAwaiter().GetResult());
            Schemes = new HashSet<string>(schemes.GetAwaiter().GetResult());
        }

        public static HashSet<string> Domains { get; set; }

        public static HashSet<string> Schemes { get; set; }
    }
}
