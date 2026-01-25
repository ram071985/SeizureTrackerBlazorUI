using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SeizureTrackerBlazer;
using SeizureTrackerBlazer.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using SeizureTrackerBlazer.Constants;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Configuration.AddEnvironmentVariables();

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient<ISeizureTrackerService, SeizureTrackerService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration[AppSettings.ApiBaseAddress]);
});

builder.Services.AddTransient<CookieHandler>();

builder.Services.AddHttpClient<AccountClient>(client =>
    {
        client.BaseAddress = new Uri(builder.Configuration[AppSettings.ApiBaseAddress]);
    })
    .AddHttpMessageHandler<CookieHandler>();

builder.Services.AddSingleton<StateContainer>();

builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<IdentityAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp =>
    sp.GetRequiredService<IdentityAuthenticationStateProvider>());

builder.Services.AddCascadingAuthenticationState();

await builder.Build().RunAsync();