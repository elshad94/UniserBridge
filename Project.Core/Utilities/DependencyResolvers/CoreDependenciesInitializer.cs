using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Project.Core.Settings;
using Project.Core.Utilities.Logging;
using Project.Core.Utilities.Logging.Loggers;
using Project.Core.Utilities.Security.Jwt;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using Project.Core.Utilities.SwaggerOperationFilters;

namespace Project.Core.Utilities.DependencyResolvers
{
    public static class CoreDependenciesInitializer
    {

        public static void AddCoreDependencies(this IServiceCollection services)
        {
            var jwtOptions = AppSettings.Settings.JwtOptions;

            services.AddSwaggerGen(opt =>
                {

                    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "Uniser Bridge API", Version = "v1" });
                //    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                //    {
                //        In = ParameterLocation.Header,
                //        Description = "Please enter token",
                //        Name = "Authorization",
                //        Type = SecuritySchemeType.Http,
                //        BearerFormat = "JWT",
                //        Scheme = "bearer"
                //    });
                //    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                //{
                //{
                //new OpenApiSecurityScheme
                //{
                //Reference = new OpenApiReference
                //{
                //Type=ReferenceType.SecurityScheme,
                //Id="Bearer"
                //}
                //},
                //new string[]{}
                //}
                //});

                    try
                    {
                        var xmlFilename = $"{Assembly.GetEntryAssembly().GetName().Name}.xml";
                        opt.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

                        // Register custom filter that adds descriptions for TableValuedFunctionRequest endpoints
                        opt.OperationFilter<TableValuedFunctionRequestOperationFilter>();
                    }
                    catch (Exception)
                    {

                   
                    }


                });

            services.AddScoped<IJwtService, JwtManager>();
            services.AddSingleton<ILogger, DefaultLogger>();

            services.AddAutoMapper(typeof(MappingProfile));

            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //    .AddJwtBearer(options =>
            //    {
            //        options.TokenValidationParameters = new TokenValidationParameters
            //        {
            //            ValidateIssuer = true,
            //            ValidateAudience = true,
            //            ValidateLifetime = true,
            //            ValidIssuer = jwtOptions.Issuer,
            //            ValidAudience = jwtOptions.Audience,
            //            ValidateIssuerSigningKey = true,
            //            ClockSkew = TimeSpan.Zero,
            //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecurityKey)),

            //            //IssuerSigningKeys = new List<SymmetricSecurityKey> {
            //            //new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecurityKey)),
            //            //new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.RefreshTokenSecurityKey))
            //            //}
            //        };
            //    });


            services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
            });


            services.Configure<ApiBehaviorOptions>(options
              => options.SuppressModelStateInvalidFilter = true);

        }
    }
}