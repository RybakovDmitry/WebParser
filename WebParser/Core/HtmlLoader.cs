using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebParser.Core
{
    class HtmlLoader
    {
        readonly HttpClient Client;
        readonly string Url;

        public HtmlLoader(IWebParserSettings settings)
        {
            Client = new HttpClient();
            Url = $"{settings.BaseUrl}/{settings.Prefix}";
        }

        public async Task<string> GetSourceByPageId(int id)
        {
            string source = null;
            var currentUrl = Url.Replace("{CurrentId}", id.ToString());

            var response = await Client.GetAsync(currentUrl);

            if (response != null && response.StatusCode == HttpStatusCode.OK)
            {
                source = await response.Content.ReadAsStringAsync();
            }

            return source;
        }
    }
}
