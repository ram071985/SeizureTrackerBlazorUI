using Microsoft.AspNetCore.Components;
using SeizureTrackerBlazer.Models;
using SeizureTrackerBlazer.Services;

namespace SeizureTrackerBlazer.Pages;

public partial class ManageLogs : ComponentBase
{
    private bool _initialized;
    private bool _fetch;
    private int _count;
    private string _loadingSpinnerColor;
    private List<SeizureActivityHeader> _seizureActivityHeaders;
    
    [Inject]
    public StateContainer StateContainer { get; set; }
    protected override async Task OnInitializedAsync()
    {
        _loadingSpinnerColor = "text-info";
        
        _seizureActivityHeaders = await StateContainer.GetActivityHeaders();
        _initialized = true;
    }
}