using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GetLinksAnywhere.Controllers;
using GetLinksAnywhere.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using NUnit.Framework;

namespace GetLinksAnywhere.Tests.Controllers
{
    [TestFixture]
    public class ProcessingControllerTests
    {
        #region Fields
        
        private ProcessingController _controller;
        private Mock<IFinderService> _finderService;
        private CancellationTokenSource _cancellationToken;

        #endregion

        #region SetUp

        [SetUp]
        public void SetUp()
        {
            _cancellationToken = new CancellationTokenSource();
            _finderService = new Mock<IFinderService>();
            _controller = new ProcessingController(_finderService.Object,
                new NullLogger<ProcessingController>());
        }

        #endregion

        #region Public Methods

        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public async Task FindLinksInText_DataIsNullOrWhiteSpace_ReturnStatusNoContent(string data)
        {
            var result = await _controller.FindLinksInText(data, _cancellationToken.Token);

            Assert.That(result, Is.TypeOf<NoContentResult>());
        }

        [Test]
        public async Task FindLinksInText_WhenCalled_CallFinderService()
        {
            var result = await _controller.FindLinksInText("data", _cancellationToken.Token);

            _finderService.Verify(x => x.FindAllLinks(It.IsAny<string>(),
                It.IsAny<int>(), It.IsAny<CancellationToken>()));
        }

        [Test]
        public async Task FindLinksInText_WhenSuccess_ReturnOkWithResult()
        {
            _finderService.Setup(x => x.FindAllLinks(It.IsAny<string>(),
                    It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => new List<string> {"result"});

            var result = await _controller.FindLinksInText("data", _cancellationToken.Token);

            Assert.That(result, Is.TypeOf<OkObjectResult>());
            Assert.That(((OkObjectResult) result).Value,
                Is.EquivalentTo(new List<string> {"result"}));
        }

        #endregion
    }
}
