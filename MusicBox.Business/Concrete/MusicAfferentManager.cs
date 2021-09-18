using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using MusicBox.Business.Abstract;
using MusicBox.DataAccess.Abstract;
using MusicBox.Model.Concrete;
using MusicBox.Model.Concrete.Response;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace MusicBox.Business.Concrete
{
    public class MusicAfferentManager : IMusicAfferentService
    {
        private readonly IMusicAfferentDal _dal;
        private readonly IValidator<SearchModel> _validator;
        private static List<Result> _cachedList;
        private static int _skipCount;
        private const int TakeCount = 10;
        public MusicAfferentManager(IMusicAfferentDal dal,IValidator<SearchModel> validator)
        {
            _dal = dal;
            _validator = validator;
        }
        public async Task<List<Result>> BringMusic(SearchModel model,string nextKey)
        {
            if (!string.IsNullOrEmpty(nextKey))
                return GetNextTenResult();
            ModelCanNotBeNull(model);
            await _validator.ValidateAndThrowAsync(model);
            var musics = await _dal.BringMusics(model);
            ResponseContent contents = JsonConvert.DeserializeObject<ResponseContent>(musics.Trim());
            _cachedList = contents?.Results.ToList();
            return _cachedList?.Take(TakeCount).ToList();
        }

        private List<Result> GetNextTenResult()
        {
            if(_cachedList == null)
            {
                return _cachedList;
            }
            _skipCount++;
            return _cachedList.Skip(_skipCount * TakeCount).Take(TakeCount).ToList();
        }

        private void ModelCanNotBeNull(SearchModel model)
        {
            if(model == null)
                throw new NullReferenceException("Gönderilen model boş olamaz.");
        }
    }
}