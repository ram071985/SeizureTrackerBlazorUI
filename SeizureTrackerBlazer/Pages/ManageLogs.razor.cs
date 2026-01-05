using Microsoft.AspNetCore.Components;

namespace SeizureTrackerBlazer.Pages;

public partial class ManageLogs : ComponentBase
{
    private bool _isLoading;

    protected override void OnInitialized()
    {
        _isLoading = true;
    }
    
    
}