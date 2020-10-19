﻿using System;
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
        private readonly ILogger<FinderService> _logger;

        public FinderService(ILogger<FinderService> logger)
        {
            _logger = logger;
        }

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

        private IEnumerable<string> GetLinks(IEnumerable<Chunk> chunks, CancellationToken cancellationToken)
        {
            var result = new ConcurrentBag<string>();
            var regexData = new RegexUrlBuilder().Build();

            var counter = 0;
            var totalCount = chunks.Count();

            Parallel.ForEach(chunks, async chunk =>
            {
                cancellationToken.ThrowIfCancellationRequested();

                var uriNormalizer = new UriNormalizer();

                counter++;
                _logger.LogInformation($"Processing {counter} of {totalCount}");

                var matches = regexData.Regex.Matches(chunk.Content);
                var links = matches.Select(m => m.Value);

                foreach (var link in links)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    var normalized = await uriNormalizer.Normalize(link);

                    if(normalized != null) 
                        result.Add(normalized);
                }

                _logger.LogInformation($"Processing done for {counter} of {totalCount}");

            });

            return result.Distinct();
        }
    }
}
