using System;

namespace GetLinksAnywhere.Common.Classes
{
    public static class UriNormalizer
    {
        public static bool TryNormalize(string rawUri, out string normalizedUri)
        {
            if (string.IsNullOrWhiteSpace(rawUri))
                throw new ArgumentNullException();

            normalizedUri = null;
            try
            {
                var uriBuilder = new UriBuilder(rawUri);
                normalizedUri = uriBuilder.Uri.ToString();
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
