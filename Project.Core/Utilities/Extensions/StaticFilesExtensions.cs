using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;

namespace Project.Core.Utilities.Extensions
{
    public static class StaticFilesExtensions
    {
        public static IApplicationBuilder UseStaticAppFiles(this IApplicationBuilder app)
        {
            var uploadsFolder = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "Project.Core", "Uploads");

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(uploadsFolder),
                RequestPath = "/Uploads"
            });

            return app;
        }
    }
}
