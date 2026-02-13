using SeizureTrackerBlazer.Models;

namespace SeizureTrackerBlazer.Services;

public interface ISeizureTrackerService
{
    Task PatchSeizureActivityDetail(SeizureActivityDetail seizureActivityDetail);
    Task AddSeizureActivityLog(string body);
    Task<List<SeizureActivityHeader>> GetActivityHeaders();
    Task<List<SeizureActivityDetail>> GetActivityDetailsByHeaderId(int headerId);
}