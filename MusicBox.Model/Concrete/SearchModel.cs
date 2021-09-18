using System.ComponentModel.DataAnnotations;

namespace MusicBox.Model.Concrete
{
    public class SearchModel
    {
        public string Term { get; set; }
        public string Country { get; set; }
        public string Media { get; set; }
        public string Entity { get; set; }
        public string Attribute { get; set; }
        public string Callback { get; set; }
        public string Lang { get; set; }
        public string Limit { get; set; } = "200";
        public string NextKey { get; set; }
    }
}