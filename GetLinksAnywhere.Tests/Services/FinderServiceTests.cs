using System;
using System.IO;
using System.Linq;
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
            var result = await _finderService.FindAllLinks(_testData);

            Assert.That(result.Count(), Is.EqualTo(6));
        }

        private Task<string> GetTestData()
        {
            var path = Path.Combine(
                Directory.GetCurrentDirectory(), "testdata");

            return File.ReadAllTextAsync(path);
        }
    }
}
