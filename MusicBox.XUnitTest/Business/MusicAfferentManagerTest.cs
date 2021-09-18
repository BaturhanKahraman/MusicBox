using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using FluentValidation;
using Moq;
using MusicBox.Business.Abstract;
using MusicBox.Business.Concrete;
using MusicBox.Business.Utility.ValidationRules;
using MusicBox.DataAccess.Abstract;
using MusicBox.Model.Concrete;
using MusicBox.Model.Concrete.Response;
using Xunit;

namespace MusicBox.XUnitTest.Business
{
    public class MusicAfferentManagerTest
    {
       
        private readonly IMusicAfferentService _musicService;
        private readonly Mock<IMusicAfferentDal> _mockDal;
        private SearchModel _model;
        private readonly string _jsonMusics;
        public MusicAfferentManagerTest()
        {
            _mockDal = new Mock<IMusicAfferentDal>();
            IValidator<SearchModel> validator = new SearchModelValidator();
            _musicService = new MusicAfferentManager(_mockDal.Object,validator);
            _jsonMusics = File.ReadAllText(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName  + "\\Items\\Musics.json");
        }

        [Fact]
        public async void BringMusic_NullCountryOrAttribute_ThrowsValidationException()
        {
            _model = new SearchModel()
            {
                Attribute = null,
                Country = string.Empty
            };
            await Assert.ThrowsAsync<ValidationException>(async () => await _musicService.BringMusic(_model,null));
            _model= new SearchModel()
            {
                Attribute = null,
                Country = string.Empty
            };
            await Assert.ThrowsAsync<ValidationException>(async () => await _musicService.BringMusic(_model,null));
        }

        [Fact]
        public async void BringMusic_NullModel_ThrowsNullReferenceException()
        {
            await Assert.ThrowsAsync<NullReferenceException>(async () => await _musicService.BringMusic(null,null));
        }

        [Fact]
        public async void BringMusic_OnExecute_ReturnsAtLeastTenListOfResult()
        {
            _model = new SearchModel() {Country = "TR", Term = "Barış Manço"};
            _mockDal.Setup(x => x.BringMusics(_model)).ReturnsAsync(_jsonMusics);
            var result = await _musicService.BringMusic(_model,null);
            Assert.IsType<List<Result>>(result);
            Assert.True(result.Count<=10);
        }

        [Fact]
        public async void BringMusic_CacheList_ReturnsAtLeastTenListOfResult()
        {
            _model = new SearchModel() { Country = "TR",Term = "Barış Manço" };
            _mockDal.Setup(x => x.BringMusics(_model)).ReturnsAsync(_jsonMusics);
            await _musicService.BringMusic(_model,null);
            var result = await _musicService.BringMusic(_model,"Key");
            Assert.IsType<List<Result>>(result);
            Assert.True(result.Count<=10);
        }
    }
}