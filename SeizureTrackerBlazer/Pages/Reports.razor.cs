using System.Text;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SeizureTrackerBlazer.Models;

namespace SeizureTrackerBlazer.Pages;

public partial class Reports : ComponentBase
{
    private DateTime _startDate = DateTime.Now.AddMonths(-1);
    private DateTime _endDate = DateTime.Now;
    private List<SeizureActivityDetail>? _reportData;

    protected override async Task OnInitializedAsync() => await LoadReportData();

    private async Task LoadReportData()
    {
        // // Assuming your service can filter by date
        // var allData = await SeizureService.GetAllLogs();
        // _reportData = allData
        //     .Where(x => x.OccurredAt.Date >= _startDate.Date && x.OccurredAt.Date <= _endDate.Date)
        //     .OrderByDescending(x => x.OccurredAt)
        //     .ToList();
    }

    private string GetTopSeizureType()
    {
        if (_reportData == null || !_reportData.Any()) return "N/A";
        return _reportData.GroupBy(x => x.SeizureType)
            .OrderByDescending(g => g.Count())
            .First().Key;
    }

    private async Task ExportToCsv()
    {
        if (_reportData == null || !_reportData.Any()) return;

        var csv = new StringBuilder();
        csv.AppendLine("Date,Time,Type,Comments");

        // foreach (var item in _reportData)
        // {
        //     string date = item.OccurredAt.ToString("M/d/yy");
        //     string time = item.OccurredAt.ToString("h:mm tt");
        //     // Wrap comments in quotes to handle commas within notes
        //     string comment = $"\"{item.Comments?.Replace("\"", "\"\"")}\"";
        //     
        //     csv.AppendLine($"{date},{time},{item.SeizureType},{comment}");
        // }

        await JS.InvokeVoidAsync("downloadCsv", $"SeizureReport_{DateTime.Now:yyyyMMdd}.csv", csv.ToString());
    }
}