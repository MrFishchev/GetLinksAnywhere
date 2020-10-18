using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using GetLinksAnywhere.Model;

namespace GetLinksAnywhere.Common.Extensions
{
    public static class StringExtension
    {
        public static Task<IEnumerable<Chunk>> SplitToChunks(this string data, 
            int maxLengthOfChunk)
        {
            if (string.IsNullOrWhiteSpace(data))
                return Task.FromResult(Enumerable.Empty<Chunk>());

            return Task.Run(() =>
            {
                var pattern = @"(?:(.{1," + maxLengthOfChunk + @"})(?:\s|$)|(.+?)(?:\s|$))";

                return Regex.Split(data, pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase)
                    .Where(x => x.Length > 0)
                    .Select(x => new Chunk(x));
            });
        }
    }
}
