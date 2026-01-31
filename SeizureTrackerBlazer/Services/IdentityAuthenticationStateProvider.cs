using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using System.Security.Claims;

namespace SeizureTrackerBlazer.Services;

public class IdentityAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly AccountClient _client;
    private readonly ClaimsPrincipal _anonymous = new(new ClaimsIdentity());
    private AuthenticationState _cachedState; // Store it here!

    public IdentityAuthenticationStateProvider(AccountClient client)
    {
        _client = client;
    }

    // This is called by Blazor whenever it needs to know if the user is authorized
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        // 1. If we already have a logged-in user in memory, return it. No API call!
        if (_cachedState != null && _cachedState.User.Identity.IsAuthenticated)
        {
            return _cachedState;
        }

        try
        {
            // .NET 10 Identity API includes an /info endpoint to get user data
            var userResponse = await _client.GetUserInfoAsync();

            if (userResponse.Data == null || !userResponse.Data.IsAuthenticated)
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            var claims = new List<Claim>
            {
                new(ClaimTypes.Email, userResponse.Data.Email),
                new(ClaimTypes.NameIdentifier, userResponse.Data.UserId)
            };

            foreach (var role in userResponse.Data.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var identity = new ClaimsIdentity(claims, "Identity.Application");
            _cachedState = new AuthenticationState(new ClaimsPrincipal(identity));
        }
        catch (Exception)
        {
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        return _cachedState;
    }

    // Call this after a successful Passkey login to refresh the UI
    public void NotifyUserAuthentication(string email)
    {
        var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, email) }, "Identity.Application");
        var user = new ClaimsPrincipal(identity);
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }

    public void NotifyUserLogout()
    {
        var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
        var authState = Task.FromResult(new AuthenticationState(anonymousUser));
        NotifyAuthenticationStateChanged(authState);
    }
}

public record UserInfo(string UserId, string Email, bool IsEmailConfirmed);