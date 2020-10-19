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

                var matches = Regex.Matches(data, pattern,
                    RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.ECMAScript | RegexOptions.Multiline);
                var  chunks = matches.Select(r => r.Value);

                return chunks.Select(c => new Chunk(c.Trim()));
            });
        }
    }
}
