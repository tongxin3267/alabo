using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Alabo.Industry.Shop.Activitys.Extensions
{
    /// <summary>
    /// </summary>
    public class ActivityContext
    {
        /// <summary>
        ///     The logger factory
        /// </summary>
        private ILoggerFactory _loggerFactory;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ActivityContext" /> class.
        /// </summary>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        public ActivityContext(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        ///     Gets the HTTP context accessor.
        /// </summary>
        /// <value>
        ///     The HTTP context accessor.
        /// </value>
        public IHttpContextAccessor HttpContextAccessor { get; }

        /// <summary>
        ///     Gets the logger factory.
        /// </summary>
        /// <value>
        ///     The logger factory.
        /// </value>
        public ILoggerFactory LoggerFactory
        {
            get
            {
                if (_loggerFactory == null) {
                    _loggerFactory = HttpContextAccessor.HttpContext.RequestServices.GetService<ILoggerFactory>();
                }

                return _loggerFactory;
            }
        }
    }
}