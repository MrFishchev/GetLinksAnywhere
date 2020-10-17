using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using GetLinksAnywhere.Common.Extensions;
using NUnit.Framework;
// ReSharper disable PossibleMultipleEnumeration

namespace GetLinksAnywhere.Common.Tests.Extensions
{
    [TestFixture]
    public class StringExtensionTests
    {
        private const int MaxLengthOfChunk = 5;

        [SuppressMessage("ReSharper", "StringLiteralTypo")]
        [TestCase("Hello this is a test", new string[] {"Hello", "this", "is a", "test"})]
        [TestCase("Hi, my name is Elizabeth", new string[] {"Hi,", "my", "name", "is", "Elizabeth"})]
        [TestCase("Iamlongword", new string[]{ "Iamlongword" })]
        public async Task SplitToChunks_ContainsTextData_ReturnChunks(string data, string[] expected)
        {
            var result = await data.SplitToChunks(MaxLengthOfChunk);
            var resultData = result.Select(x => x.Content)?.ToArray();
;
            Assert.That(result.Count(), Is.EqualTo(expected.Length));
            Assert.That(resultData, Is.EqualTo(expected));
        }

        [TestCase("")]
        [TestCase(null)]
        [TestCase(" ")]
        public async Task SplitToChunks_StringIsNullOrEmptyOrWhitespace_ReturnNull(string data)
        {
            var result = await data.SplitToChunks(MaxLengthOfChunk);

            Assert.That(result, Is.Empty);
        }
    }
}
