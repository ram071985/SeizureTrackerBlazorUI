using Microsoft.AspNetCore.Components.WebAssembly.Http;

namespace SeizureTrackerBlazer.Services
{
    public class CookieHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            // This ensures the browser includes the .AspNetCore.Identity.Application cookie
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

            return base.SendAsync(request, cancellationToken);
        }
    }
}