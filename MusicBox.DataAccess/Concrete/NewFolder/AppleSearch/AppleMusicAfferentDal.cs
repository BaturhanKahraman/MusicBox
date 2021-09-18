using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using MusicBox.DataAccess.Abstract;
using MusicBox.Model.Concrete;

namespace MusicBox.DataAccess.Concrete.NewFolder.AppleSearch
{
    public class AppleMusicAfferentDal:IMusicAfferentDal
    {
        private const string ApiBaseAddress = "https://itunes.apple.com/search?";
        private readonly HttpClient _client;

        public AppleMusicAfferentDal(HttpClient client)
        {
            _client = client;
        }

        public async Task<string> BringMusics(SearchModel model)
        {
            string endPointAddress = ApiBaseAddress;
            bool isFirst = true;
            foreach (var propertyInfo in model.GetType().GetProperties())
            {
                var propertyValue =propertyInfo.GetValue(model)?.ToString();
                if (!string.IsNullOrWhiteSpace(propertyValue))
                {
                    if(!isFirst)
                        endPointAddress += '&';
                    endPointAddress += propertyInfo.Name.ToLower() + "=" + propertyValue.Trim();
                    isFirst = false;
                }
            }
            var result = await _client.GetAsync(endPointAddress);
            var musics =await result.Content.ReadAsStringAsync();
            return musics;
        }
    }
}