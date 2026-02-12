namespace SeizureTrackerBlazer.Models;

public class ToasterConfig(string borderColor, string textColor, string message)
{
    public string BorderColor { get; set; } = borderColor;
    public string TextColor { get; set; } = textColor;
    public string Message { get; set; } = message;
}