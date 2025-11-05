namespace SeizureTrackerBlazer.Constants
{
    internal static class UIConstants
    {
        internal const string MultiSelectShowText = "Choose seizure type";
        internal const string MultiSelectHideText = "Hide seizure types";
    }

    internal static class AppSettings
    {
        internal const string SeizureTrackerAPIName = "APIName";
    }

    internal static class MedChange
    {
        internal const string Yes = "Yes";
        internal const string No = "No";
    }

    internal static class SeizureTypes
    {
        internal const string Partial = "Partial";
        internal const string Complex = "Complex";
        internal const string Aura = "Aura";
        internal const string GrandMal = "Grand Mal";
        internal const string Musicogenic = "Musicogenic/Sensory";
        internal const string SmellSensory = "Smell/Sensory";
        internal const string HeatSensory = "Heat/Sensory";
        internal const string Anxiety = "Anxiety";
        internal const string MedicationChange = "Medication Change";
    }

    internal static class InputTypes
    {
        internal const string Input = "Input";
        internal const string Checkbox = "Checkbox";
        internal const string Select = "Select";
        internal const string TextArea = "TextArea";
    }

    internal static class InputDataTypes
    {
        internal const string Text = "text";
        internal const string Date = "date";
        internal const string Time = "time";
        internal const string DateTime = "datetime-local";
        internal const string TextArea = "textarea";
        internal const string Email = "email";
        internal const string Number = "number";
        internal const string Decimal = "decimal";
    }

    internal static class InputLabels
    {
        internal const string SeizureDescription = "Seizure description";
        internal const string DateAndTime = "Date and time of seizure";
        internal const string Intensity = "Intensity";
        internal const string MedicationChange = "Medication change";
        internal const string MedicationChangeDescription = "Medication change description";
        internal const string Notes = "Additional notes";
        internal const string SleepAmount = "Sleep amount";
        internal const string SeizureType = "Seizure type";
    }

    internal static class PlaceholderLabels
    {
        internal const string Intensity = "e.g. 10 = strong, 1 = light";
        internal const string KetonesLevel = "e.g. 3.5";
    }

    internal static class AriaLabels
    {
        internal const string SeizureType = "Enter multiple seizure types";
    }

    internal static class OptionLabels
    {
        internal const string SeizureType = "Enter 1 or more types";
        internal const string MedChange = "Enter yes or no";
    }
};