using SeizureTrackerBlazer.Constants;

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