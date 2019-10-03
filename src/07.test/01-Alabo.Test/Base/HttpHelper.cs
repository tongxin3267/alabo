using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Alabo.Test.Base
{
    /// <summary>
    ///     Http Client 的帮助函数
    /// </summary>
    public static class HttpHelper
    {
        /// <summary>
        ///     使用Get方法获取字符串结果（没有加入Cookie）
        /// </summary>
        /// <param name="url"></param>
        public static async Task<string> HttpGetAsync(string url, Encoding encoding = null)
        {
            var httpClient = new HttpClient();
            var data = await httpClient.GetByteArrayAsync(url);
            var ret = encoding.GetString(data);
            return ret;
        }

        /// <summary>
        ///     Http Get 同步方法
        /// </summary>
        /// <param name="url"></param>
        /// <param name="encoding"></param>
        public static string HttpGet(string url)
        {
            HttpClient httpClient;
            if (ClientAuthContainer.CurrentHandler != null) {
                httpClient = new HttpClient(ClientAuthContainer.CurrentHandler);
            } else {
                httpClient = new HttpClient();
            }

            var t = httpClient.GetByteArrayAsync(url);
            t.Wait();
            var ret = Encoding.UTF8.GetString(t.Result);
            return ret;
        }

        /// <summary>
        ///     通过表单更新内容
        /// </summary>
        /// <param name="url"></param>
        /// <param name="dictionary">字典数据</param>
        public static Tuple<bool, string> Post(string url, Dictionary<string, string> dictionary)
        {
            HttpClient client;
            if (ClientAuthContainer.CurrentHandler != null) {
                client = new HttpClient(ClientAuthContainer.CurrentHandler);
            } else {
                client = new HttpClient();
            }

            var ms = new MemoryStream();
            dictionary.FillFormDataStream(ms); //填充formData
            HttpContent hc = new StreamContent(ms);
            hc.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/html"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xhtml+xml"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml", 0.9));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("image/webp"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*", 0.8));
            hc.Headers.Add("UserAgent",
                "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.57 Safari/537.36");
            hc.Headers.Add("KeepAlive", "true");
            var t = client.PostAsync($"{url}", hc);
            t.Wait();
            var t2 = t.Result.Content.ReadAsByteArrayAsync();
            var result = HttpUtility.HtmlDecode(Encoding.UTF8.GetString(t2.Result));
            var status = t.Result.StatusCode == HttpStatusCode.OK;
            return Tuple.Create(status, result);
        }

        // <summary>
        /// POST 异步
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postStream"></param>
        /// <param name="encoding"></param>
        /// <param name="timeOut"></param>
        public static async Task<string> HttpPostAsync(string url, Dictionary<string, string> formData = null,
            Encoding encoding = null, int timeOut = 10000)
        {
            HttpClient client;
            if (ClientAuthContainer.CurrentHandler != null) {
                client = new HttpClient(ClientAuthContainer.CurrentHandler);
            } else {
                client = new HttpClient();
            }

            var ms = new MemoryStream();
            formData.FillFormDataStream(ms); //填充formData
            HttpContent hc = new StreamContent(ms);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/html"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xhtml+xml"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml", 0.9));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("image/webp"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*", 0.8));
            hc.Headers.Add("UserAgent",
                "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.57 Safari/537.36");
            hc.Headers.Add("Timeout", timeOut.ToString());
            hc.Headers.Add("KeepAlive", "true");

            var r = await client.PostAsync(url, hc);
            var tmp = await r.Content.ReadAsByteArrayAsync();

            return encoding.GetString(tmp);
        }

        /// <summary>
        ///     POST 同步
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postStream"></param>
        /// <param name="encoding"></param>
        /// <param name="timeOut"></param>
        public static string HttpPost(string url, Dictionary<string, string> formData = null, int timeOut = 10000)
        {
            HttpClient client;
            if (ClientAuthContainer.CurrentHandler != null) {
                client = new HttpClient(ClientAuthContainer.CurrentHandler);
            } else {
                client = new HttpClient();
            }

            var ms = new MemoryStream();
            formData.FillFormDataStream(ms); //填充formData
            HttpContent hc = new StreamContent(ms);

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/html"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xhtml+xml"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml", 0.9));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("image/webp"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*", 0.8));
            hc.Headers.Add("UserAgent",
                "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.57 Safari/537.36");
            hc.Headers.Add("Timeout", timeOut.ToString());
            hc.Headers.Add("KeepAlive", "true");

            var t = client.PostAsync(url, hc);
            t.Wait();
            var t2 = t.Result.Content.ReadAsByteArrayAsync();
            var result = Encoding.UTF8.GetString(t2.Result);
            return result;
        }

        /// <summary>
        ///     组装QueryString的方法
        ///     参数之间用&连接，首位没有符号，如：a=1&b=2&c=3
        /// </summary>
        /// <param name="formData"></param>
        public static string GetQueryString(this Dictionary<string, string> formData)
        {
            if (formData == null || formData.Count == 0) {
                return "";
            }

            var sb = new StringBuilder();

            var i = 0;
            foreach (var kv in formData)
            {
                i++;
                sb.AppendFormat("{0}={1}", kv.Key, kv.Value);
                if (i < formData.Count) {
                    sb.Append("&");
                }
            }

            return sb.ToString();
        }

        /// <summary>
        ///     填充表单信息的Stream
        /// </summary>
        /// <param name="formData"></param>
        /// <param name="stream"></param>
        public static void FillFormDataStream(this Dictionary<string, string> formData, Stream stream)
        {
            var dataString = GetQueryString(formData);
            var formDataBytes = formData == null ? new byte[0] : Encoding.UTF8.GetBytes(dataString);
            stream.Write(formDataBytes, 0, formDataBytes.Length);
            stream.Seek(0, SeekOrigin.Begin); //设置指针读取位置
        }
    }
}