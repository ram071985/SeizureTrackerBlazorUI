

namespace SeizureTrackerBlazer.Models
{
    public class SeizureLog
    {
        public int Id { get; set; }
        public string SeizureDate { get; set; }
        public string SeizureTime { get; set; }
        public int SeizureIntensity { get; set; }
        public int KetonesLevel { get; set; }
        public string SeizureType { get; set; }
        public int SleepAmount { get; set; }
        public string MedicationChangeExplanation { get; set; }
    }
}