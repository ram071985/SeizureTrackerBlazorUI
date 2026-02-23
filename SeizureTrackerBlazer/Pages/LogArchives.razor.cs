using Microsoft.AspNetCore.Components;
using SeizureTrackerBlazer.Models;
using SeizureTrackerBlazer.Services;

namespace SeizureTrackerBlazer.Pages;

public partial class LogArchives : ComponentBase
{
    [Inject] private NavigationManager Navigation { get; set; }
    [Inject] private ISeizureTrackerService SeizureTrackerService { get; set; }
    
    private List<SeizureActivityHeader> _headers = [];
    private bool _isLoading;
    private ToasterConfig _toasterConfig;
    private string? _displayDate;

    protected override async Task OnInitializedAsync()
    {
        _isLoading = true;
        
        _headers = await SeizureTrackerService.GetActivityHeaders();
        
        _isLoading = false;
    }

    private void NavigateToDay(int id)
    {
        _displayDate = _headers.FirstOrDefault(h => h.Id == id)?.Date;
        // Pass the selected date to the detail page route
        Navigation.NavigateTo($"/day-details/{id}/{_displayDate}");
    }
}