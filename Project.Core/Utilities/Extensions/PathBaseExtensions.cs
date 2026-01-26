using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Project.Core.Utilities.Extensions
{
    public static class PathBaseExtensions
    {
        public static IApplicationBuilder ConfigurePathBase(this IApplicationBuilder app, string pathBase)
        {
            app.UsePathBase(new PathString(pathBase));

            app.Use((context, next) =>
            {
                context.Request.PathBase = new PathString(pathBase);
                return next();
            });

            return app;
        }
    }
}
