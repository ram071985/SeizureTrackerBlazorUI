using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SeizureTrackerBlazer;
using SeizureTrackerBlazer.Services;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration["ApiBaseAddress"]) });
builder.Services.AddScoped<ISeizureTrackerService, SeizureTrackerService>();

builder.Services.AddMsalAuthentication(options =>
{
    builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
    options.ProviderOptions.DefaultAccessTokenScopes
        .Add("api://20129ded-8b40-4c7d-ab9e-1f4fc8b959b0/Seizure.Records.Read.All");
});

builder.Services.AddSingleton<StateContainer>();

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
await builder.Build().RunAsync();