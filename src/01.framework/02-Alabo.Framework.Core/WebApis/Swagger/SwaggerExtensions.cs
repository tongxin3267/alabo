using System.IO;
using System.Linq;
using Alabo.Core.WebApis.Controller;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;

namespace Alabo.Core.WebApis.Swagger {

    public static class SwaggerExtensions {

        /// <summary>
        ///     添加 Swagger Api 接口服务
        /// </summary>
        /// <param name="services">The services.</param>
        public static IServiceCollection AddSwaggerService(this IServiceCollection services) {
            services.AddSwaggerGen(options => {
                options.CustomSchemaIds(x => x.FullName);
                options.SwaggerDoc("api", new Info { Title = "Alabo Api 文档", Version = "v12" });
                options.ResolveConflictingActions(b => b.First());
                options.DocInclusionPredicate((doc, a) =>
                    a.ActionDescriptor is ControllerActionDescriptor &&
                    typeof(ApiBaseController).IsAssignableFrom((a.ActionDescriptor
                        as ControllerActionDescriptor).ControllerTypeInfo));
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                foreach (var item in Directory.GetFiles(basePath, "*.xml")) {
                    if (Path.GetFileName(item).Contains("Alabo")) {
                        options.IncludeXmlComments(item);
                    }
                }
            });

            return services;
        }

        /// <summary>
        ///     添加 Swagger Api 接口配置
        /// </summary>
        /// <param name="app">The services.</param>
        /// <param name="env"></param>
        public static IApplicationBuilder AddSwaggerConfig(this IApplicationBuilder app, IHostingEnvironment env) {
            app.UseSwagger();

            app.UseSwaggerUI(c => {
                //c.RoutePrefix = "ApiDoc";
                c.SwaggerEndpoint("/swagger/api/swagger.json", "v12");
                c.DocumentTitle = "Alabo Api 文档";
            });
            return app;
        }
    }
}