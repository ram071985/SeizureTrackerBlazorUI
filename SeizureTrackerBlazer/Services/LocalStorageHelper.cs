using Microsoft.JSInterop;
using System.Text.Json;
using System.Text.Json;

namespace SeizureTrackerBlazer.Services;

public class LocalStorageHelper(IJSRuntime js)
{
    public async Task SetItemAsync<T>(string key, T value)
    {
        var json = JsonSerializer.Serialize(value);
        await js.InvokeVoidAsync("localStorage.setItem", key, json);
    }

    public async Task<T?> GetItemAsync<T>(string key)
    {
        var json = await js.InvokeAsync<string?>("localStorage.getItem", key);
        return json == null ? default : JsonSerializer.Deserialize<T>(json);
    }

    public async Task RemoveItemAsync(string key) => 
        await js.InvokeVoidAsync("localStorage.removeItem", key);
}