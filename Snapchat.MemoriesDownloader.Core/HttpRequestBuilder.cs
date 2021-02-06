using System;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;

namespace Snapchat.MemoriesDownloader.Core
{
    public static class HttpRequestBuilder
    {
        internal static HttpRequestMessage GetRequest(Uri uri)
        {
            var request = new HttpRequestMessage {Method = HttpMethod.Get, RequestUri = uri};

            request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            return request;
        }

        internal static HttpRequestMessage PostRequest(Uri uri)
        {
            var request = new HttpRequestMessage {Method = HttpMethod.Post, RequestUri = uri};

            request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            return request;
        }

        internal static HttpRequestMessage PatchRequest(Uri uri)
        {
            var request = new HttpRequestMessage {Method = new HttpMethod("PATCH"), RequestUri = uri};

            request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            return request;
        }

        internal static HttpRequestMessage DeleteRequest(Uri uri)
        {
            var request = new HttpRequestMessage {Method = HttpMethod.Delete, RequestUri = uri};

            request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            return request;
        }
    }
}