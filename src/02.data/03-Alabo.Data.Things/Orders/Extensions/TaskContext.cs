using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Alabo.Data.Things.Orders.Extensions {

    public class TaskContext {
        private ILoggerFactory _loggerFactory;

        public TaskContext(IHttpContextAccessor httpContextAccessor) {
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