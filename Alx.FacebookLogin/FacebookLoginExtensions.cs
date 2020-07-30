using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Alx.FacebookLogin.Data;
using Alx.FacebookLogin.Data.Dtos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Alx.FacebookLogin
{
    public static class FacebookLoginExtensions
    {
        public static IServiceCollection AddFacebookLogin(this IServiceCollection services, Action<FacebookLoginOptions> optionsAction = null)
        {
            var fbOptions = new FacebookLoginOptions();

            optionsAction?.Invoke(fbOptions);

            return services
                .AddSingleton(fbOptions)
                .AddScoped<FacebookLoginService>();
        }

        public static void UseFacebookLogin(this IApplicationBuilder app)
        {
            var fbOptions = app.ApplicationServices.GetService<FacebookLoginOptions>();
            app.UseWhen(context => context.Request.Method == "GET" && context.Request.Path.Equals(fbOptions.Route), appBuilder =>
            {
                appBuilder.UseMiddleware<FacebookLoginMiddleware>();
            });
        }

        internal static async Task<TValue> SendAsyncEx<TValue>(this HttpClient client, HttpRequestMessage message)
        {
            var response = await client.SendAsync(message);
            using var responseStream = await response.Content.ReadAsStreamAsync();

            if (!response.IsSuccessStatusCode)
            {
                var error = await JsonSerializer.DeserializeAsync<ErrorDto>(responseStream);
                throw new Exception(error.Error.Message);
            }

            return await JsonSerializer.DeserializeAsync<TValue>(responseStream);
        }
    }
}
