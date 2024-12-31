namespace SeizureTrackerBlazer.Constants
{
    internal static class UIConstants
    {
        internal const string MultiSelectShowText = "Choose seizure type";
        internal const string MultiSelectHideText = "Hide seizure types";
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
    }

    internal static class InputDataTypes
    {
        internal const string Text = "text";
        internal const string Date = "date";
        internal const string Time = "time";
        internal const string TextArea = "textarea";
        internal const string Email = "email";
        internal const string Number = "number";
    }

    internal static class InputLabels
    {
        internal const string DateOfSeizure = "Date of seizure";
        internal const string TimeOfSeizure = "Time of seizure";
        internal const string Intensity = "Intensity";
        
    }

    internal static class PlaceHolderLabels
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
    }
};