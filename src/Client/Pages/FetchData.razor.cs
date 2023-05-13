using Client.Services.Interfaces;
using GSN.Domain;
using Microsoft.AspNetCore.Components;

namespace Client.Pages;

public partial class FetchData
{
    [Inject] private IDataService<WeatherForecast, int> _dataService { get; set; } = null!;

    private IEnumerable<WeatherForecast> _forecasts;

    protected override async Task OnInitializedAsync()
    {
        _forecasts = await _dataService.GetAllAsync();
    }
}
