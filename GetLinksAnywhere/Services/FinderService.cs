using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GetLinksAnywhere.Common;
using GetLinksAnywhere.Common.Classes;
using GetLinksAnywhere.Common.Extensions;
using GetLinksAnywhere.Model;
using GetLinksAnywhere.Services.IServices;
using Microsoft.Extensions.Logging;

namespace GetLinksAnywhere.Services
{
    public class FinderService : IFinderService
    {
        #region Fields

        private readonly ILogger<FinderService> _logger;

        #endregion

        #region Constructor

        public FinderService(ILogger<FinderService> logger)
        {
            _logger = logger;
        }

        #endregion

        #region Public Methods

        public async Task<IEnumerable<string>> FindAllLinks(string data,
            int maxLengthOfChunk = Constants.MaxLengthOfChunk,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrWhiteSpace(data))
                throw new ArgumentNullException(nameof(data), "Cannot be null or empty");

            var chunks = await data.SplitToChunks(maxLengthOfChunk);
            _logger.LogInformation($"Data split into {chunks.Count()} chunks");

            return GetLinks(chunks, cancellationToken);
        }

        #endregion

        #region Private Methods

        private IEnumerable<string> GetLinks(IEnumerable<Chunk> chunks, CancellationToken cancellationToken)
        {
            var result = new ConcurrentBag<string>();
            var regexData = new RegexUrlBuilder().Build();
            var totalCount = chunks.Count();
            var doneChunksCount = 0;

            var po = new ParallelOptions
            {
                CancellationToken = cancellationToken,
                MaxDegreeOfParallelism = System.Environment.ProcessorCount
            };

            Parallel.ForEach(chunks, po, async chunk =>
            {
                var uriNormalizer = new UriNormalizer();

                var matches = regexData.Regex.Matches(chunk.Content);
                var links = matches.Select(m => m.Value);

                foreach (var link in links)
                {
                    var normalized = await uriNormalizer.Normalize(link);

                    if (normalized != null)
                        result.Add(normalized);
                }

                doneChunksCount++;
                _logger.LogInformation($"Processing done for {doneChunksCount} of {totalCount}");

                po.CancellationToken.ThrowIfCancellationRequested();
            });

            return result.Distinct();
        }

        #endregion

    }
}
