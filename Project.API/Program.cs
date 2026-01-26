using BasicAuthExample.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.OpenApi.Models;
using Project.Business.Utilities.DependencyResolvers;
using Project.Core.Utilities.DependencyResolvers;
using Project.Core.Utilities.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApplicationCorsOrigins(); // adds origins from settings.json

builder.Services.AddCoreDependencies();
builder.Services.AddProjectDependencies();





//app.UseSwagger();

//app.UseSwaggerUI(o =>
//{
//    o.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
//});

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Basic", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Enter credentials",
        Scheme = "Basic",
        Type = SecuritySchemeType.Http,
        In = ParameterLocation.Header
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id="Basic",
                    Type=ReferenceType.SecurityScheme
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddAuthentication("BasicAuthentication")
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

var app = builder.Build();
//app.ConfigurePathBase("/api");


// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseCors();

app.UseRouting();
app.UseStaticAppFiles();   //Getting files from  Project.Core/Uploads

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseCoreMiddlewares();

app.MapControllers();

app.Run();
