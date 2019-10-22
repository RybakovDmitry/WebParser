

namespace WebParser.Core.Habra
{
    class HabraSettings : IWebParserSettings
    {
        public string BaseUrl { get; set; } = "https://habrahabr.ru";

        public string Prefix { get; set; } = "page{CurrentId}";

        public int StartPoint { get; set; }

        public int EndPoint { get; set; }

    }
}
