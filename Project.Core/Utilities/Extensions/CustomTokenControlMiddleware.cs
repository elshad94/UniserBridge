using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;
using Microsoft.Data.SqlClient;
using Project.Core.Entities.SPModels;

namespace Project.Core.Utilities.Extensions
{

    public class CustomTokenControlMiddleware
    {
        private readonly RequestDelegate _next;


        public CustomTokenControlMiddleware(RequestDelegate next)
        {
            _next = next;
        }


        public Task Invoke(HttpContext context)
        {
            try
            {
                var path = context.Request.Path.ToString().ToLower();

                var pathStateList = new List<bool> {
                   path.Contains("/auth/login".ToLower()),
                   path.Contains("/auth/RefreshTokenLogin".ToLower()),

                   path.Contains("GetSystemLanguages".ToLower()),
                   path.Contains("GetLoginLanguageContent".ToLower()),
                   //path.Contains("/uploads/".ToLower()),

                };

                var pathCheck = pathStateList.Count(x => x) > 0;
                var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

                if (pathCheck)
                {
                    return _next(context);
                }

                if (!string.IsNullOrEmpty(token))
                {
                    #region Custom Token control
                    //if (context.Request.Path.ToString().ToLower().Contains("uploads"))
                    //{
                    //    if (context.Request.Query.TryGetValue("token", out var fileToken))
                    //    {
                    //        if (fileToken.ToString().ToLower() != CommonTools.GenerateTokenByCurrentDate())
                    //            return HandleAsync(context);
                    //    }

                    //    return HandleAsync(context);
                    //}
                    #endregion

                    #region Endpoint control
                    var pageUrl = context.Request.Headers["PageUrl"].ToString();  //request url of client (page url)

                    if (!string.IsNullOrEmpty(pageUrl)) // client (Angular app) must send page url to make this control work
                    {
                        List<SqlParameter> parameters = new();
                        parameters.AddUserIdParam();
                        parameters.Add(new("requestUrl", pageUrl.Trim()));  // Ex:  "/modules/accounting/invoce"

                        var runProcedure = EfDbTools.ExecuteProcedure<SP_CheckIfUserHasAccessToMenu>("OBJ.SP_CheckIfUserHasAccessToMenu", parameters);

                        if (!runProcedure.First().HasAccess) return HandleAsync(context); //return Unauthorized
                    }

                    #endregion


                    #region Token control
                    //using var dbContext = new ProjectAppDbContext();
                    ////Requestden gelen tokeni db-deki ile qarshilashdir
                    //var userTokenData = dbContext.UserTokens.FirstOrDefault(x => x.AccessToken == token &&
                    //!x.LogOut);

                    //if (userTokenData != null)
                    //{
                    //    return _next(context);
                    //}
                    //else
                    //{
                    //    return HandleAsync(context);
                    //}
                    #endregion

                    return _next(context);
                }
                else
                {
                    return HandleAsync(context);
                }

            }
            catch (Exception)
            {
                return HandleAsync(context);
            }
        }

        private Task HandleAsync(HttpContext httpContext)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

            return httpContext.Response.WriteAsync(
                JsonConvert.SerializeObject(ResultDataGenerator.Generate(ResultInfo.Unauthorized)));
        }
    }
}