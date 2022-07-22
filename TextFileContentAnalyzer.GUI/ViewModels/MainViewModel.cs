using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextFileContentAnalyzer.Core.Mediator;
using TextFileContentAnalyzer.GUI.Messages;

namespace TextFileContentAnalyzer.GUI.ViewModels;

public class MainViewModel : BaseViewModel
{
    private BaseViewModel? activeViewModel;

    public BaseViewModel? ActiveViewModel
    { 
        get => activeViewModel; 
        set 
        {
            var oldvm = activeViewModel;
            if (NotifyPropertyChanged(ref activeViewModel, value))
                oldvm?.Dispose();
        }
    }

    private readonly IMediator<ApplicationClosing> _mediator;

    public MainViewModel(BaseViewModel defaultView, IMediator<ApplicationClosing> mediator)
    {
        ActiveViewModel = defaultView;
        this._mediator = mediator;
        _mediator.Subsrcibe(OnAppClosing);
    }

    public override void Dispose()
    {
        activeViewModel?.Dispose();
    }

    public void OnAppClosing(ApplicationClosing e) 
    {
        Dispose();
    }

}
