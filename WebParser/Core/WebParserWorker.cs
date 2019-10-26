using AngleSharp.Html.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebParser.Core
{
    class WebParserWorker<T> where T : class
    {
        IWebParser<T> _parser;
        IWebParserSettings _parserSettings;
        bool _isActive;

        HtmlLoader loader;
        

        public IWebParser<T> Parser
        {
            get
            {
                return _parser;
            }
            set
            {
                _parser = value;
            }
        }
        public IWebParserSettings ParserSettings
        {
            get
            {
                return _parserSettings;
            }
            set
            {
                _parserSettings = value;
                loader = new HtmlLoader(value);
            }
        }
        public bool IsActive
        {
            get
            {
                return _isActive;
            }
        }

        public event Action<object, T> OnNewData;
        public event Action<object> OnCompleted;

        public WebParserWorker(IWebParser<T> parser)
        {
            this._parser = parser;
        }

        public WebParserWorker(IWebParser<T> parser, IWebParserSettings settings):this(parser)
        {
            this._parserSettings = settings;
        }

        public void Start()
        {
            _isActive = true;
            Worker();
        }
        public void Abort()
        {
            _isActive = false;
        }

        private async void Worker()
        {
            for (int i = _parserSettings.StartPoint; i <= _parserSettings.EndPoint; i++)
            {
                if (!_isActive)
                {
                    OnCompleted?.Invoke(this);
                    return;
                }

                var source = await loader.GetSourceByPageId(i);
                var domParser = new HtmlParser();

                var document = await domParser.ParseDocumentAsync(source);

                var result =_parser.Parse(document);

                OnNewData?.Invoke(this, result);
            }
            OnCompleted?.Invoke(this);
            _isActive = false;
        }
    }
}
