using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Moq;
using MusicBox.DataAccess.Abstract;
using MusicBox.DataAccess.Concrete.NewFolder.AppleSearch;
using MusicBox.Model.Concrete;
using Xunit;

namespace MusicBox.XUnitTest.DataAccess
{
    public class AppleMusicAfferentDalTest
    {
        private readonly Mock<HttpClient> _mockClient;
        private readonly IMusicAfferentDal _appleMusicDal;
        public AppleMusicAfferentDalTest()
        {
            _mockClient = new Mock<HttpClient>();
            _appleMusicDal = new AppleMusicAfferentDal(_mockClient.Object);
        }

        [Fact]
        public async Task BringMusics_OnExecute_ReturnsString()
        {
            var result =await _appleMusicDal.BringMusics(new SearchModel() {Attribute = "Barış Manço", Country = "TR"});
            Assert.IsType<string>(result);
        }
    }
}