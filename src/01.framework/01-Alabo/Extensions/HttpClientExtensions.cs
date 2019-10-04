using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Alabo.Extensions {

    public static class HttpClientExtensions {

        /// <summary>
        ///     传递当前页面的header到新的httpclient请求中
        /// </summary>
        /// <param name="client"></param>
        /// <param name="url">请求地址</param>
        /// <param name="header">当前的httpheader信息</param>
        public static async Task<string> GetStringAsync(this HttpClient client, string url, IHeaderDictionary header) {
            if (client == null) {
                throw new ArgumentNullException(nameof(client));
            }

            if (header == null) {
                throw new ArgumentNullException(nameof(header));
            }

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            foreach (var item in header) {
                request.Headers.Add(item.Key, item.Value.ToString());
            }

            var response = await client.SendAsync(request);
            return await response.Content.ReadAsStringAsync();
        }

        public static async Task<string> GetAsync(this HttpClient client, string url, HttpRequest req,
            string method = "Get", string context = "") {
            if (client == null) {
                throw new ArgumentNullException(nameof(client));
            }

            if (req == null) {
                throw new ArgumentNullException(nameof(req));
            }

            var request = method == "Get"
                ? new HttpRequestMessage(HttpMethod.Get, url)
                : new HttpRequestMessage(HttpMethod.Post, url);

            foreach (var item in req.Headers) {
                request.Headers.Add(item.Key, item.Value.ToString());
            }

            foreach (var item in req.Cookies) {
                request.Headers.Add("Cookie", item.Value);
            }

            var response = await client.SendAsync(request);
            return await response.Content.ReadAsStringAsync();
        }

        public static async Task<string> PostAsync(string url, HttpRequest req, KeyValuePair<string, string> context,
            string method = "GET") {
            var response = new HttpResponseMessage();

            if (req == null) {
                throw new ArgumentNullException(nameof(req));
            }

            var handler = new HttpClientHandler { UseCookies = false };
            var client = new HttpClient(handler);

            var request = new HttpRequestMessage(method == "GET" ? HttpMethod.Get : HttpMethod.Post, url);

            foreach (var item in req.Headers) {
                request.Headers.Add(item.Key, item.Value.ToString());
            }

            foreach (var item in req.Cookies) {
                request.Headers.Add("Cookie", item.Value);
            }

            var formContent = new FormUrlEncodedContent(new[] { context });

            response = await client.PostAsync(url, formContent);

            return await response.Content.ReadAsStringAsync();
        }
    }
}