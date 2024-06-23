using System.Reactive.Disposables;
using System.Reactive.Linq;
using Microsoft.AspNetCore.Components;

namespace LightsOn.WebApp.Views.Bases;

public partial class ProgressBarBase : IDisposable
{
    [Parameter] public required double Minimum { get; set; }
    [Parameter] public required double Maximum { get; set; }
    [Parameter] public required double Value { get; set; }
    
    private CompositeDisposable compositeDisposable = new CompositeDisposable();

    public ProgressBarBase()
    {
        Value = 0;
    }

    protected override void OnInitialized()
    {
        var timer = Observable.Interval(TimeSpan.FromSeconds(1))
            .Subscribe(_ =>
            {
                InvokeAsync(() =>
                {
                    Value = (Value + 10) % Maximum;
                    StateHasChanged();
                });
            });

        compositeDisposable.Add(timer);
    }

    public void Dispose()
    {
        compositeDisposable.Dispose();
    }
}