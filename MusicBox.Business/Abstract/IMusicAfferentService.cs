using MusicBox.Model.Concrete;
using MusicBox.Model.Concrete.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicBox.Business.Abstract
{
    public interface IMusicAfferentService
    {
        Task<List<Result>> BringMusic(SearchModel model,string nextKey);
    }
}