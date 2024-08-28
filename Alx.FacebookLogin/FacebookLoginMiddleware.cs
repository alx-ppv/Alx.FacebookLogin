using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Alx.FacebookLogin.Data;
using Alx.FacebookLogin.Data.Dtos;
using static System.Net.WebUtility;
using Microsoft.AspNetCore.Http;

namespace Alx.FacebookLogin
{
    public class FacebookLoginMiddleware(RequestDelegate next, IConfiguration configuration, IHttpClientFactory clientFactory)
    {
        public async Task InvokeAsync(HttpContext context, FacebookLoginService fbService, FacebookLoginOptions fbOptions)
        {
            try
            {
                var code = context.Request.Query["code"].ToString();

                if (string.IsNullOrEmpty(code))
                {
                    throw new ArgumentException("Query parameter 'code' does not exist");
                }

                var graphVersion = configuration[fbOptions.ConfigurationKeys.APIVersion] ?? "20.0";

                var accessTokenRequest = new HttpRequestMessage(HttpMethod.Get,
                    string.Format("https://graph.facebook.com/v{0}/oauth/access_token?client_id={1}&client_secret={2}&redirect_uri={3}&code={4}",
                        graphVersion,
                        configuration[fbOptions.ConfigurationKeys.AppId],
                        configuration[fbOptions.ConfigurationKeys.AppSecret],
                        UrlEncode(configuration[fbOptions.ConfigurationKeys.RedirectUri]),
                        code
                   ));

                var httpClient = clientFactory.CreateClient();
                var accessTokenResponse = await httpClient.SendAsyncEx<AccessTokenDto>(accessTokenRequest);
                var fields = string.Join(",", fbOptions.Fields);

                var userDataRequest = new HttpRequestMessage(HttpMethod.Get,
                    $"https://graph.facebook.com/me?fields={fields}&access_token={accessTokenResponse.AccessToken}");

                fbService.UserData = await httpClient.SendAsyncEx<UserDataDto>(userDataRequest);
            }
            catch (Exception e)
            {
                fbService.ErrorMessage = e.Message;
            }

            await next(context);
        }
    }
}
