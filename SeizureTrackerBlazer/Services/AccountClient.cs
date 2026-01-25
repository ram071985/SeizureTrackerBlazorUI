using System.Net.Http.Json;
using SeizureTrackerBlazer.Models;
using SeizureTrackerBlazer.Pages;

namespace SeizureTrackerBlazer.Services;

public class AccountClient
{
    private readonly HttpClient _client;

    public AccountClient(HttpClient client)
    {
        _client = client;
    }

    // Used by AuthenticationStateProvider
    public async Task<ServiceResult<UserInfoResponse>> GetUserInfoAsync()
    {
        try 
        {
            // The CookieHandler now automatically adds 'Include Credentials' to this call
            var result = await _client.GetFromJsonAsync<ServiceResult<UserInfoResponse>>("api/auth/info");
            return result ?? ServiceResult<UserInfoResponse>.Fail("Could not parse user info.");
        }
        catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            return ServiceResult<UserInfoResponse>.Fail("Unauthorized: Please log in.");
        }
        catch (Exception ex)
        {
            return ServiceResult<UserInfoResponse>.Fail($"Internal Error: {ex.Message}");
        }
    }

    // Used by Login.razor
    public async Task<ServiceResult<string>> GetPasskeyOptionsAsync(string email)
    {
        try
        {
            var response = await _client.GetAsync($"api/auth/passkey-options?email={email}");

            if (response.IsSuccessStatusCode)
            {
                var jsonOptions = await response.Content.ReadAsStringAsync();

                return ServiceResult<string>.Ok(jsonOptions);
            }

            return ServiceResult<string>.Fail("Account not found. Please register first.");
        }
        catch (HttpRequestException ex)
        {
            var message = ex.StatusCode switch
            {
                System.Net.HttpStatusCode.NotFound => "Server endpoint not found.",
                System.Net.HttpStatusCode.ServiceUnavailable => "Server is currently busy.",
                _ => "Network error. Please check your internet connection."
            };
            return ServiceResult<string>.Fail(message);
        }
        catch (Exception ex)
        {
            return ServiceResult<string>.Fail($"Hardware handshake failed: {ex.Message}");
        }
    }

    public async Task<ServiceResult<bool>> LoginWithPasskeyAsync(string credentialJson)
    {
        try
        {
            var response = await _client.PostAsJsonAsync("api/auth/passkey-login", credentialJson);

            if (response.IsSuccessStatusCode)
                return ServiceResult<bool>.Ok(true);

            return ServiceResult<bool>.Fail("Login failed. Check your credentials.");
        }
        catch (HttpRequestException ex)
        {
            // 2026 Best Practice: Check the StatusCode to provide better UI feedback
            var message = ex.StatusCode switch
            {
                System.Net.HttpStatusCode.NotFound => "Server endpoint not found.",
                System.Net.HttpStatusCode.ServiceUnavailable => "Server is currently busy.",
                _ => "Network error. Please check your internet connection."
            };
            return ServiceResult<bool>.Fail(message);
        }
        catch (Exception ex)
        {
            return ServiceResult<bool>.Fail($"An unexpected error occurred");
        }
    }

    public async Task<ServiceResult<bool>> RegisterUserAsync(RegisterModel regModel)
    {
        try
        {
            // Calls the native .NET 10 Identity API endpoint: /register
            var response = await _client.PostAsJsonAsync($"api/auth/register", regModel);

            if (response.IsSuccessStatusCode)
                return ServiceResult<bool>.Ok(true);

            return ServiceResult<bool>.Fail("Register failed. Check your credentials.");
        }
        catch (HttpRequestException ex)
        {
            var message = ex.StatusCode switch
            {
                System.Net.HttpStatusCode.NotFound => "Server endpoint not found.",
                System.Net.HttpStatusCode.ServiceUnavailable => "Server is currently busy.",
                _ => "Network error. Please check your internet connection."
            };
            return ServiceResult<bool>.Fail(message);
        }
        catch (Exception ex)
        {
            return ServiceResult<bool>.Fail("An unexpected error occurred.");
        }
    }
    public async Task<ServiceResult<bool>> LoginWithPasswordAsync(string email, string password)
    {
        try
        {
            // Construct the payload for the API
            var loginRequest = new { Email = email, Password = password, RememberMe = true };

            // Send request to your AuthController login endpoint
            var response = await _client.PostAsJsonAsync("api/auth/login", loginRequest);

            if (response.IsSuccessStatusCode)
            {
                return ServiceResult<bool>.Ok(true);
            }

            // Handle specific status codes with custom messages
            return response.StatusCode switch
            {
                System.Net.HttpStatusCode.Unauthorized => 
                    ServiceResult<bool>.Fail("Invalid email or password."),
                System.Net.HttpStatusCode.Locked => 
                    ServiceResult<bool>.Fail("Account is locked. Please try again in 15 minutes."),
                _ => 
                    ServiceResult<bool>.Fail("An error occurred during login. Please try again.")
            };
        }
        catch (Exception ex)
        {
            return ServiceResult<bool>.Fail($"Connection error: {ex.Message}");
        }
    }
}