using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetLinksAnywhere.Common;
using GetLinksAnywhere.Common.Classes;
using GetLinksAnywhere.Common.Extensions;
using GetLinksAnywhere.Model;
using GetLinksAnywhere.Services.IServices;

namespace GetLinksAnywhere.Services
{
    public class FinderService : IFinderService
    {
        private const string UrlRegexPattern = @"((http|ftp|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?)";

        public async Task<IEnumerable<string>> FindAllLinks(string data, 
            int maxLengthOfChunk = Constants.MaxLengthOfChunk)
        {
            if (string.IsNullOrWhiteSpace(data))
                throw new ArgumentNullException(nameof(data), "Cannot be null or empty");

            var chunks = await data.SplitToChunks(maxLengthOfChunk);

            return GetLinks(chunks);
        }

        private IEnumerable<string> GetLinks(IEnumerable<Chunk> chunks)
        {
            var result = new ConcurrentBag<string>();

            Parallel.ForEach(chunks, chunk =>
            {
                var regexData = new RegexUrlBuilder().Build();
                var matches = regexData.Regex.Matches(chunk.Content);
                var links = matches.Select(m => m.Value);

                //TODO normalize
                var normalizedLinks = links;

                foreach (var link in normalizedLinks)
                    result.Add(link);
            });

            return result;
        }
    }
}
