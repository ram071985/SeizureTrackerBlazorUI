using System.Text.Json;
using SeizureTrackerBlazer.Constants;
using SeizureTrackerBlazer.Models;

namespace SeizureTrackerBlazer.Services;

public class SeizureTrackerService : ISeizureTrackerService
{
    private readonly HttpClient _client;
    private readonly string? _apiBaseAddress;
    private readonly string? _apiName;

    public SeizureTrackerService(IConfiguration config, IHttpClientFactory httpClientFactory)
    {
        _client = httpClientFactory.CreateClient();
        _apiBaseAddress = config.GetValue<string>("ApiBaseAddress");
        _apiName = config.GetValue<string>(AppSettings.SeizureTrackerAPIName);

        _client.BaseAddress = new Uri(_apiBaseAddress);
    }

    public async Task<List<SeizureActivityHeader>> GetActivityHeaders()
    {
        var uri = $"{_apiName}/{ApiEndpoints.GetHeaders}";
        
        try
        {
            var response = await _client.GetAsync(uri);
            
            return JsonSerializer.Deserialize<List<SeizureActivityHeader>>(await response.Content.ReadAsStringAsync());
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);

            throw;
        }
    }
    
    public async Task<List<SeizureActivityDetail>> GetActivityDetailsByHeaderId(int headerId)
    {
        var uri = $"{_apiName}/{ApiEndpoints.GetDetailsByHeaderId}/{headerId}";
        try
        {
            var response = await _client.GetAsync(uri);
            
            return JsonSerializer.Deserialize<List<SeizureActivityDetail>>(await response.Content.ReadAsStringAsync());
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);

            throw;
        }
    }

    public async Task AddSeizureActivityLog(string body)
    {
        try
        {
            await _client.PostAsync(_apiName, new StringContent(body, System.Text.Encoding.UTF8, "application/json"));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            
            throw;
        }
    }
}