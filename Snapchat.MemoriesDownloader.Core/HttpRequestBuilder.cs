using System;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Hosting;

namespace Snapchat.MemoriesDownloader.Core
{
    public class HttpRequestBuilder
    {
        private readonly HttpRequestMessage _request;
        private string _content;
        private string _mediaType;
        private Encoding _encoding;

        private HttpRequestBuilder(HttpRequestMessage request) => _request = request;

        internal static HttpRequestBuilder PostRequest(Uri route)
        {
            var request = new HttpRequestMessage {Method = HttpMethod.Post, RequestUri = route};
            request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            return new HttpRequestBuilder(request);
        }

        internal static HttpRequestBuilder GetRequest(Uri route)
        {
            var request = new HttpRequestMessage {Method = HttpMethod.Get, RequestUri = route};
            return new HttpRequestBuilder(request);
        }

        internal HttpRequestBuilder WithContent(string content)
        {
            _content = content;
            return this;
        }

        internal HttpRequestBuilder WithMediaType(string mediaType)
        {
            _mediaType = mediaType;
            return this;
        }

        internal HttpRequestBuilder WithEncoding(Encoding encoding)
        {
            _encoding = encoding;
            return this;
        }

        internal HttpRequestMessage Build()
        {
            if (!string.IsNullOrEmpty(_content) && _encoding != null && !string.IsNullOrEmpty(_mediaType))
                _request.Content = new StringContent(_content, _encoding, _mediaType);
            else if (!string.IsNullOrEmpty(_content) && _encoding != null)
                _request.Content = new StringContent(_content, _encoding);
            else if (!string.IsNullOrEmpty(_content))
                _request.Content = new StringContent(_content);
            return _request;
        }
    }
}