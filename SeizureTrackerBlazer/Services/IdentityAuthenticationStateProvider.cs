using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using System.Security.Claims;

namespace SeizureTrackerBlazer.Services;

public class IdentityAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly AccountClient _client;
    private readonly ClaimsPrincipal _anonymous = new(new ClaimsIdentity());

    public IdentityAuthenticationStateProvider(AccountClient client)
    {
        _client = client;
    }

    // This is called by Blazor whenever it needs to know if the user is authorized
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            // .NET 10 Identity API includes an /info endpoint to get user data
            var userResponse = await _client.GetUserInfoAsync();

            if (userResponse != null)
            {
                var identity = new ClaimsIdentity([
                    new Claim(ClaimTypes.Email, userResponse.Data.Email),
                    new Claim(ClaimTypes.NameIdentifier, userResponse.Data.UserId)
                ], "Identity.Application");

                return new AuthenticationState(new ClaimsPrincipal(identity));
            }
        }
        catch (Exception)
        {
            /* Handle network errors */
        }

        return new AuthenticationState(_anonymous);
    }

    // Call this after a successful Passkey login to refresh the UI
    public void NotifyUserAuthentication(string email)
    {
        var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, email) }, "Identity.Application");
        var user = new ClaimsPrincipal(identity);
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }

    // Call this during logout
    public void NotifyUserLogout()
    {
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_anonymous)));
    }
}

public record UserInfo(string UserId, string Email, bool IsEmailConfirmed);