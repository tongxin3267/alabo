using Microsoft.AspNetCore.Builder;

namespace Alabo.Core.Localize
{
    public static class LocalizeExtensions
    {
        public static IApplicationBuilder RegisterAllTranslateProviders(this IApplicationBuilder app)
        {
            return app;
        }
    }
}