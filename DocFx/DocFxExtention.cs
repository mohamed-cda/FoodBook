using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
namespace DocFx
{
    public static class DocFxExtension
    {
        public static IApplicationBuilder UseDocFx(
            this IApplicationBuilder app,
            Action<DocFxSettings> configure = null
            )
        {
            DocFxSettings config = new DocFxSettings();
            if (configure == null)
            {
                config.rootPath = "/doc";
            }
            else
            {
                configure.Invoke(config);
            }
            app.UseFileServer(new FileServerOptions
            {
                RequestPath = new PathString(config.rootPath),
                FileProvider = new EmbeddedFileProvider(typeof(DocFxExtension).GetTypeInfo().Assembly, "DocFx._site"),
            });
            return app;
        }
    }
}