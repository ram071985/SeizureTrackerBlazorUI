using SeizureTrackerBlazer.Models;

namespace SeizureTrackerBlazer.Services;

public interface ISeizureTrackerService
{
    public Task AddSeizureActivityLog(string body);
    public Task<List<SeizureActivityHeader>> GetActivityHeaders();
}