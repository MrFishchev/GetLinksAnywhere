using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GetLinksAnywhere.Services;
using NUnit.Framework;

namespace GetLinksAnywhere.Tests.Services
{
    [TestFixture]
    public class FinderServiceTests
    {
        private FinderService _finderService;
        private string _testData;
        private const int MaxLengthOfChunk = 100;

        [SetUp]
        public async Task SetUp()
        {
            _finderService = new FinderService();
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
        public async Task FindAllLinks_DataExists_ReturnLinksList()
        {
            var expected = new List<string>
            {
                "www.flickr.com",
                "500px.com",
                "www.freeimagehosting.net",
                "https://postimage.io",
                "https://www.facebook.com",
                "http://🍕.ws",
                "http://example.com/default.asp",
                "http://example.com/a/index.html",
                "http://208.77.188.1",
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
