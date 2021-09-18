using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MusicBox.API.Controllers;
using MusicBox.Business.Abstract;
using MusicBox.Model.Concrete;
using MusicBox.Model.Concrete.Response;
using Xunit;

namespace MusicBox.XUnitTest.Api
{
    public class MusicControllerTest
    {
        private readonly Mock<IMusicAfferentService> _mockAfferentService;
        private SearchModel _model;
        private readonly MusicsController _musicController;
        public MusicControllerTest()
        {
            _mockAfferentService = new Mock<IMusicAfferentService>();
            _musicController = new MusicsController(_mockAfferentService.Object);
        }
        [Fact]
        public async void GetMusic_OnExecute_OKResult()
        {
            List<Result> results = new List<Result>();
            _model = new SearchModel();
            _mockAfferentService.Setup(x => x.BringMusic(_model,null)).ReturnsAsync(results);
            var actionResult =await _musicController.GetMusic(_model,null);
            Assert.IsType<OkObjectResult>(actionResult);
        }

        [Fact]
        public async void GetMusic_NullResult_BadRequestResult()
        {
            _model = new SearchModel();
            var actionResult = await _musicController.GetMusic(_model,null);
            Assert.IsType<BadRequestObjectResult>(actionResult);
        }
    }
}