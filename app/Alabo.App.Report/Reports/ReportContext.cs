using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Alabo.App.Core.Reports {

    public sealed class ReportContext {
        private ILoggerFactory _loggerFactory;

        public ReportContext(IHttpContextAccessor httpContextAccessor) {
            HttpContextAccessor = httpContextAccessor;
        }

        public IHttpContextAccessor HttpContextAccessor { get; }

        public ILoggerFactory LoggerFactory {
            get {
                if (_loggerFactory == null) {
                    _loggerFactory = HttpContextAccessor.HttpContext.RequestServices.GetService<ILoggerFactory>();
                }

                return _loggerFactory;
            }
        }
    }
}