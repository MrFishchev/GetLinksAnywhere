using System;
using System.Threading.Tasks;
using GetLinksAnywhere.Common.Classes;
using NUnit.Framework;

namespace GetLinksAnywhere.Common.Tests.Classes
{
    [TestFixture]
    public class UriNormalizerTests
    {
        private UriNormalizer _uriNormalizer;

        [SetUp]
        public void SetUp()
        {
            _uriNormalizer = new UriNormalizer();
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void Normalize_UriIsNullOrEmpty_ThrowArgumentNullException(string uri)
        {
            Assert.ThrowsAsync<ArgumentNullException>( () => _uriNormalizer.Normalize(uri));
        }

        [Test]
        public async Task Normalize_UriIsNotValid_ReturnEmptyString()
        {
            var result = await _uriNormalizer.Normalize("http://example:80.com");

            Assert.That(result, Is.Empty);
        }

        [TestCase("HTTP://WWW.Example.com", "http://www.example.com/")]
        [TestCase("http://www.example.com", "http://www.example.com/")]
        [TestCase("http://www.example.com:80", "http://www.example.com/")]
        [TestCase("http://www.example.com/%7Eusername/", "http://www.example.com/~username/")]
        [TestCase("http://www.example.com/../a/b/../c/./d.html", "http://www.example.com/a/c/d.html")]

        public async Task Normalize_UriIsCorrect_ReturnNormalizedUri(string uri, string expected)
        {
            var result = await _uriNormalizer.Normalize(uri);

            Assert.That(result, Is.EqualTo(expected));
        }
    }
}
