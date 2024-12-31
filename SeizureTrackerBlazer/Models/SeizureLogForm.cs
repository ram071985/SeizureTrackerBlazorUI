

namespace SeizureTrackerBlazer.Models
{
    public class SeizureLogForm
    {
        public int Id { get; set; }
        public string SeizureDate { get; set; }
        public string SeizureTime { get; set; }
        public string SeizureIntensity { get; set; }
        public string KetonesLevel { get; set; }
        public string SeizureType { get; set; }
        public string SleepAmount { get; set; }
        public string MedicationChangeExplanation { get; set; }
        public string Notes { get; set; }
    }
}