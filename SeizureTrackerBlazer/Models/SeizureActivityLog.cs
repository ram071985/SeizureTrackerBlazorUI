

namespace SeizureTrackerBlazer.Models
{
    public class SeizureActivityLog
    {
        public int Id { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? SeizureTime { get; set; }
        public string? SeizureDescription { get; set; }
        public string? SeizureType { get; set; }
        public string MedicationChange { get; set; }
        public string MedicationChangeExplanation { get; set; }
        public string SleepAmount { get; set; }
        public string SeizureIntensity { get; set; }
        public string AdditionalNotes { get; set; }

        
        // public string KetonesLevel { get; set; }
        
        
        
        
 
    }
}