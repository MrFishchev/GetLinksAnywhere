using System;
using System.Threading.Tasks;

namespace GetLinksAnywhere.Common.Classes
{
    public class UriNormalizer
    {
        public Task<string> Normalize(string rawUri)
        {
            if (string.IsNullOrWhiteSpace(rawUri))
                throw new ArgumentNullException();

            try
            {
                var uriBuilder = new UriBuilder(rawUri);
                return Task.FromResult(uriBuilder.Uri.ToString());
            }
            catch
            {
                return Task.FromResult(string.Empty);
            }
        }
    }
}
