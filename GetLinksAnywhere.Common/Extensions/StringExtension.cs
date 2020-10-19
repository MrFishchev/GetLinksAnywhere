using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using GetLinksAnywhere.Model;

namespace GetLinksAnywhere.Common.Extensions
{
    public static class StringExtension
    {
        private static readonly HashSet<char> _breakChars = new HashSet<char> {',', '\r', '\n', ' ', '!'};

        public static Task<IEnumerable<Chunk>> SplitToChunks(this string data,
            int maxLengthOfChunk)
        {
            if (string.IsNullOrWhiteSpace(data))
                return Task.FromResult(Enumerable.Empty<Chunk>());

            var chunks = GetChunks(data, maxLengthOfChunk);
            return Task.FromResult(chunks);
        }

        private static IEnumerable<Chunk> GetChunks(string data, int maxLength)
        {
            var result = new List<Chunk>();
            var availableData = data;

            while (availableData.Length > maxLength)
            {
                var isFound = false;

                for (var i = maxLength; i >= 0; i--)
                {
                    if (!_breakChars.Contains(availableData[i])) continue;

                    var index = (i == 0) ? availableData.Length : i;
                    var chunk = new Chunk(availableData.Substring(0, index).Trim());
                    availableData = availableData.Remove(0, index);

                    isFound = true;
                    result.Add(chunk);
                    break;
                }

                if (!isFound)
                {
                    result.Add(new Chunk(availableData.Trim()));
                    availableData = string.Empty;
                    break;
                }
            }

            if(availableData?.Trim().Length > 0)
                result.Add(new Chunk(availableData.Trim()));

            return result;
        }

    }
}
