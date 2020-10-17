using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using GetLinksAnywhere.Common.Extensions;
using GetLinksAnywhere.Model;
using GetLinksAnywhere.Services.IServices;

namespace GetLinksAnywhere.Services
{
    public class FinderService : IFinderService
    {
        private const string UrlRegexPattern = @"((http|ftp|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?)";

        public async Task<IEnumerable<string>> FindAllLinks(string data)
        {
            if (string.IsNullOrWhiteSpace(data))
                throw new ArgumentNullException(nameof(data), "Cannot be null or empty");

            var chunks = await data.SplitToChunks();

            return GetLinks(chunks);
        }

        private IEnumerable<string> GetLinks(IEnumerable<Chunk> chunks)
        {
            var result = new ConcurrentBag<string>();

            Parallel.ForEach(chunks, chunk =>
            {
                var regex = new Regex(UrlRegexPattern);
                var matches = regex.Matches(chunk.Content);
                var normalizedLinks = matches.Select(m => NormalizeLink(m.Value));

                foreach (var link in normalizedLinks)
                    result.Add(link);
            });

            return result;
        }

        private string NormalizeLink(string link)
        {
            return link;
        }
    }
}
