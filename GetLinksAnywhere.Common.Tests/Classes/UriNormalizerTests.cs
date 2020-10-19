using System;
using GetLinksAnywhere.Common.Classes;
using NUnit.Framework;

namespace GetLinksAnywhere.Common.Tests.Classes
{
    [TestFixture]
    public class UriNormalizerTests
    {
        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void Normalize_UriIsNullOrEmpty_ThrowArgumentNullException(string uri)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var success= UriNormalizer
                    .TryNormalize(uri, out var result);
            });
        }

        [Test]
        public void Normalize_UriIsNotValid_ReturnFalseAndNull()
        {
            var success = UriNormalizer
                .TryNormalize("http://example:80.com", out var result);

            Assert.That(success, Is.False);
            Assert.That(result, Is.Null);
        }

        [TestCase("HTTP://WWW.Example.com", "http://www.example.com/")]
        [TestCase("http://www.example.com", "http://www.example.com/")]
        [TestCase("http://www.example.com:80", "http://www.example.com/")]
        [TestCase("http://www.example.com/%7Eusername/", "http://www.example.com/~username/")]
        [TestCase("http://www.example.com/../a/b/../c/./d.html", "http://www.example.com/a/c/d.html")]

        public void Normalize_UriIsCorrect_ReturnNormalizedUri(string uri, string expected)
        {
            var success = UriNormalizer
                .TryNormalize(uri, out var result);

            Assert.That(success, Is.True);
            Assert.That(result, Is.EqualTo(expected));
        }
    }
}
