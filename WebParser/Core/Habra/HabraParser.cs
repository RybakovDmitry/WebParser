using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Html.Dom;

namespace WebParser.Core.Habra
{
    class HabraParser : IWebParser<string[]>
    {
        public string[] Parse(IHtmlDocument document)
        {
            List<string> Titles = new List<string>();
            var items = document.QuerySelectorAll("a").Where(x => x.ClassName != null && x.ClassName.Contains("post__title_link"));

            foreach (var item in items)
            {
                Titles.Add(item.TextContent);
            }

            return Titles.ToArray();
        }
    }
}
