using Microsoft.AspNetCore.Components;
using SeizureTrackerBlazer.Models;
using SeizureTrackerBlazer.Services;

namespace SeizureTrackerBlazer.Pages;

public partial class LogArchives : ComponentBase
{
    [Inject] private NavigationManager Navigation { get; set; }
    [Inject] private ISeizureTrackerService SeizureTrackerService { get; set; }
    
    private List<SeizureActivityHeader> _headers = [];

    protected override async Task OnInitializedAsync()
    {
        _headers = await SeizureTrackerService.GetActivityHeaders();
    }

    private void NavigateToDay(int id)
    {
        // Pass the selected date to the detail page route
        Navigation.NavigateTo($"/day-details/{id}");
    }

    public class DailySummary
    {
        public DateTime Date { get; set; }
        public int TotalSeizures { get; set; }
    }
}