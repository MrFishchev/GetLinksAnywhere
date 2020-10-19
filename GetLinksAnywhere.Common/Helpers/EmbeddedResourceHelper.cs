using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GetLinksAnywhere.Common.Helpers
{
    public static class EmbeddedResourceHelper
    {
        #region Public Methods
        
        public static Task<IEnumerable<string>> ReadDomainsSettings()
        {
            return Read("Domains");
        }

        public static Task<IEnumerable<string>> ReadSchemesSettings()
        {
            return Read("Schemes");
        }

        #endregion

        #region Private Methods

        private static Task<IEnumerable<string>> Read(string filename)
        {
            var resourceName = $"GetLinksAnywhere.Common.IANA.{filename}";

            return StreamHelper.ReadAllLines(() =>
                Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName), Encoding.UTF8);
        }

        #endregion
    }
}
