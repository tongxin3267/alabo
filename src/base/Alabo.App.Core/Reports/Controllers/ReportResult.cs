using Newtonsoft.Json;
using System;

namespace Alabo.App.Core.Reports.Controllers {

    public class ReportResult<T> {

        private ReportResult() {
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public T Result { get; private set; }

        public bool Succeeded { get; private set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; private set; }

        public static ReportResult<T> Success(T result) {
            if (result == null) {
                throw new ArgumentNullException(nameof(result));
            }

            return new ReportResult<T> {
                Result = result,
                Message = null,
                Succeeded = true
            };
        }

        public static ReportResult<T> Failed() {
            return new ReportResult<T> {
                Succeeded = false,
                Message = null
            };
        }

        public static ReportResult<T> Failed(string message) {
            return new ReportResult<T> {
                Succeeded = false,
                Message = message
            };
        }
    }
}