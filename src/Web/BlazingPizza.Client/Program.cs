using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazingPizza.Client
{
    public class ApiGatewayAuthorizationMessageHandler : AuthorizationMessageHandler
    {
        public ApiGatewayAuthorizationMessageHandler(IAccessTokenProvider provider,
            NavigationManager navigationManager)
            : base(provider, navigationManager)
        {
            ConfigureHandler(
                authorizedUrls: new[] { "http://localhost:5202" },
                scopes: new[] { "basket" });
        }
    }

    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddScoped<ApiGatewayAuthorizationMessageHandler>();

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddHttpClient<BasketClient>(client => client.BaseAddress = new Uri("http://localhost:5202/b/api/v1/basket/"))
                .AddHttpMessageHandler<ApiGatewayAuthorizationMessageHandler>();

            builder.Services.AddHttpClient<CatalogClient>(client => client.BaseAddress = new Uri("http://localhost:5202/c/api/v1/catalog/"));

            builder.Services.AddHttpClient<OrdersClient>(client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            builder.Services.AddScoped<OrderState>();

            // Add auth services
            builder.Services.AddApiAuthorization<PizzaAuthenticationState>(options =>
            {
                options.AuthenticationPaths.LogOutSucceededPath = "";
            });

            await builder.Build().RunAsync();
        }
    }
}
