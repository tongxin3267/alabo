using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alabo.RestfulApi.Clients {

    public class HttpClient : IHttpClient {

        public T Get<T>(string apiUrl, IDictionary<string, string> para = null) {
            throw new NotImplementedException();
        }

        public Task<T> GetAsync<T>(string apiUrl, IDictionary<string, string> para = null) {
            throw new NotImplementedException();
        }

        public T Post<T>(string apiUrl, IDictionary<string, string> para = null) {
            throw new NotImplementedException();
        }

        public Task<T> PostAsync<T>(string apiUrl, IDictionary<string, string> para = null) {
            throw new NotImplementedException();
        }
    }
}