using Microsoft.AspNetCore.Components;
using SeizureTrackerBlazer.Models;
using SeizureTrackerBlazer.Services;

namespace SeizureTrackerBlazer.Pages;

public partial class ManageLogs : ComponentBase
{
    private bool _initialized, _fetch, _detailsFetched;
    private int _count;
    private string _loadingSpinnerColor;
    private List<SeizureActivityHeader> _seizureActivityHeaders;
    private List<SeizureActivityDetail> _seizureActivityDetails;
    
    [Inject]
    public StateContainer StateContainer { get; set; }
    protected override async Task OnInitializedAsync()
    {
        _loadingSpinnerColor = "text-info";
        
        _seizureActivityHeaders = await StateContainer.GetActivityHeaders();
    }

    protected override void OnParametersSet()
    {
        _initialized = true;
    }

    private async Task GetActivityDetails(int headerId)
    {
        try
        {
            _seizureActivityDetails = await StateContainer.GetActivityDetailsByHeaderId(headerId);
            
            _detailsFetched = _seizureActivityDetails?.Count > 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
}