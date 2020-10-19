using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GetLinksAnywhere.Services;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;

namespace GetLinksAnywhere.Tests.Services
{
    [TestFixture]
    public class FinderServiceTests
    {
        private FinderService _finderService;
        private string _testData;
        private const int MaxLengthOfChunk = 50;

        [SetUp]
        public async Task SetUp()
        {
            var logger = new NullLogger<FinderService>();
            _finderService = new FinderService(logger);
            _testData = await GetTestData();
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void FindAllLinks_DataIsNullOrEmptyOrWhiteSpace_ThrowArgumentNullException(
            string data)
        {
            Assert.ThrowsAsync<ArgumentNullException>(() =>
                _finderService.FindAllLinks(data));
        }

        [Test]
        public async Task FindAllLinks_ContainsDoubles_ReturnWithoutDoubles()
        {
            var data = @"This text http://example.com contains http://example.com 
                contains doubles http://another.com";
            var expected = new List<string>
            {
                "http://example.com/",
                "http://another.com/",
            };

            var result = await _finderService.FindAllLinks(data);

            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result, Is.EquivalentTo(expected));
        }

        [Test]
        public async Task FindAllLinks_DataExists_ReturnNormalizedLinksList()
        {
            var expected = new List<string>
            {
                "http://www.flickr.com/",
                "http://500px.com/",
                "http://www.freeimagehosting.net/",
                "https://postimage.io/",
                "https://www.facebook.com/",
                "http://🍕.ws/",
                "http://example.com/default.asp",
                "http://example.com/a/index.html",
                "http://208.77.188.1/",
                "http://example.com/story?id=xyz"
            };

            var result = await _finderService.FindAllLinks(_testData, MaxLengthOfChunk);

            Assert.That(result.Count(), Is.EqualTo(expected.Count));
            Assert.That(result, Is.EquivalentTo(expected));
        }

        private Task<string> GetTestData()
        {
            var path = Path.Combine(
                Directory.GetCurrentDirectory(), "testdata");

            return File.ReadAllTextAsync(path);
        }
    }
}
