using System.Text.Json;
using SeizureTrackerBlazer.Constants;
using SeizureTrackerBlazer.Models;

namespace SeizureTrackerBlazer.Services;

public class SeizureTrackerService : ISeizureTrackerService
{
    private readonly HttpClient _client;
    private readonly string? _route; 

    public SeizureTrackerService(HttpClient client, IConfiguration config)
    {
        _client = client;
        _route = config[AppSettings.TrackerApiRoute]?.TrimStart('/') ?? "";
    }

    public async Task<List<SeizureActivityHeader>> GetActivityHeaders()
    {
        var path = $"{_route}/{ApiEndpoints.GetHeaders}";
        
        try
        {
            var response = await _client.GetAsync(path);
            
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
        var path = $"{_route}/{ApiEndpoints.Details}/{headerId}";
        try
        {
            var response = await _client.GetAsync(path);
            
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
            await _client.PostAsync(_route, new StringContent(body, System.Text.Encoding.UTF8, "application/json"));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            
            throw;
        }
    }

    public async Task PatchSeizureActivityDetail(SeizureActivityDetail seizureActivityDetail)
    {
        var path = $"{_route}/{ApiEndpoints.LogDetails}";
        
        try
        {
            var json = JsonSerializer.Serialize(seizureActivityDetail);
        
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        
            await _client.PatchAsync(path, content);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }

    }
}