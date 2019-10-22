using AngleSharp.Html.Dom;

namespace WebParser.Core
{
    interface IWebParser<T> where T : class
    {
        T Parse(IHtmlDocument document);
    }
}
