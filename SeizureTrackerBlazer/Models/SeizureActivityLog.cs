namespace SeizureTrackerBlazer.Models
{
    public class SeizureActivityLog
    {
        public int Id { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? SeizureDate { get; set; }
        public string? SeizureDescription { get; set; }
        public string? SeizureType { get; set; }

        public bool? ShortLog { get; set; }
        public string? Duration { get; set; }
    }
}