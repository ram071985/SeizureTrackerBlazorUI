using Microsoft.AspNetCore.Components;
using SeizureTrackerBlazer.Services;

namespace SeizureTrackerBlazer.Pages;

public partial class LogArchives : ComponentBase
{
    [Inject] private NavigationManager Navigation { get; set; }

    private List<DailySummary> DailySummaries = new List<DailySummary>
    {
        // Replace this with data fetched from your database
        new DailySummary { Date = new DateTime(2026, 2, 7), TotalSeizures = 3 },
        new DailySummary { Date = new DateTime(2026, 2, 5), TotalSeizures = 1 },
        new DailySummary { Date = new DateTime(2026, 2, 3), TotalSeizures = 2 }
    };

    private void NavigateToDay(DateTime date)
    {
        // Pass the selected date to the detail page route
        Navigation.NavigateTo($"/day-details/{date:yyyy-MM-dd}");
    }

    public class DailySummary
    {
        public DateTime Date { get; set; }
        public int TotalSeizures { get; set; }
    }
}