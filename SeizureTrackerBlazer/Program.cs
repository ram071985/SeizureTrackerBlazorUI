using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SeizureTrackerBlazer;
using SeizureTrackerBlazer.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Configuration.AddEnvironmentVariables();

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient<ISeizureTrackerService, SeizureTrackerService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiBaseAddress"]);
})
.AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

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