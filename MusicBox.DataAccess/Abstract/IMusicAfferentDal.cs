using MusicBox.Model.Concrete;
using System.Threading.Tasks;

namespace MusicBox.DataAccess.Abstract
{
    public interface IMusicAfferentDal
    {
        Task<string> BringMusics(SearchModel model);
    }
}