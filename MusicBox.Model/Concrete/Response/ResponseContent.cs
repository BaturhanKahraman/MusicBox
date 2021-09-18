using MusicBox.Model.Abstract;

namespace MusicBox.Model.Concrete.Response
{

    public class ResponseContent
    {
        public int ResultCount { get; set; }
        public Result[] Results { get; set; }
    }
}