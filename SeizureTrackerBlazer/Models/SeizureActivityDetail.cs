namespace SeizureTrackerBlazer.Models;

public class SeizureActivityDetail
{
    public int SeizureId { get; set; }
    public int LogId { get; set; }
    public string SeizureTime { get; set; }
    public string SeizureType { get; set; }
    public string? Comments { get; set; }
    public bool? IsEditingType { get; set; }
    public bool? IsEditingComment { get; set; }
}