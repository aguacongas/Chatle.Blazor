using Chatle.Blazor.Services;
using Microsoft.AspNetCore.Blazor.Browser.Http;
using Microsoft.AspNetCore.Blazor.Browser.Rendering;
using Microsoft.AspNetCore.Blazor.Browser.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

namespace Chatle.Blazor
{
    class Program
    {
        static void Main(string[] args)
        {
            BrowserHttpMessageHandler.DefaultCredentials = FetchCredentialsOption.Include;
            var serviceProvider = new BrowserServiceProvider(services =>
            {
                services.AddSingleton<Settings>()
                    .AddSingleton<LoginService>()
                    .AddSingleton(provider => new ChatleHttpMessageHandler(provider.GetRequiredService<Settings>()))
                    .AddTransient(provider =>
                    {
                        var settings = provider.GetRequiredService<Settings>();
                        var http = new HttpClient(provider.GetRequiredService<ChatleHttpMessageHandler>())
                        {
                            BaseAddress = new Uri(settings.ApiBaseUrl)
                        };
                        return http;
                    });
            });

            new BrowserRenderer(serviceProvider).AddComponent<App>("app");
        }
    }
}
