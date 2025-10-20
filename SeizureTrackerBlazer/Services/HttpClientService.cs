namespace SeizureTrackerBlazer.Services;

public class HttpClientService(HttpClient client)
{
    public async Task PostAsync(string url, string content)
    {
        try
        {

            await client.PostAsync(url, new StringContent(content));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }
    
    
}