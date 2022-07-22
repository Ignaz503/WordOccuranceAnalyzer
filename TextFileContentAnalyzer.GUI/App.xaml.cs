using TextFileContentAnalyzer.Core.ServiceProvider;
using System.Windows;
using TextFileContentAnalyzer.GUI.ViewModels;

using TextFileContentAnalyzer.Core.Mediator;
using TextFileContentAnalyzer.GUI.Messages;
using TextFileContentAnalyzer.GUI.Services;
using TextFileContentAnalyzer.Core.DataAnalyzer;
using TextFileContentAnalyzer.Core.DataAnalyzers.WordOccurrence.ExecutionContexts;
using System;
using TextFileContentAnalyzer.GUI.Services.WordOccurrenceAnalyzerRunners;
using TextFileContentAnalyzer.Core.DataAnalyzers.WordOccurence;
using TextFileContentAnalyzer.Core.DataAnalyzer.WordOccurrence;

namespace TextFileContentAnalyzer.GUI;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
#pragma warning disable CS8618
    public TextFileContentAnalyzer.Core.ServiceProvider.IServiceProvider ServiceProvider { get; private set; }
#pragma warning restore
    protected override void OnStartup(StartupEventArgs e)
    {
        var builder = new ServiceProviderBuilder();

        builder.AddSingleton(this);

        //AddAsyncDataAnalyzationServices(builder);

        //use threaded cause long running tasks shouldn't use async
        AddThreadedDataAnalyzationServices(builder);

        builder.AddSingleton<IMediator<ApplicationClosing>, Publisher<ApplicationClosing>>();

        builder.AddTransient(p =>            
            new MainWindow() { DataContext = new MainViewModel(p.GetService<WordOccurrenceAnalyzerViewModel>(), p.GetService<IMediator<ApplicationClosing>>()) }
        , typeof(WordOccurrenceAnalyzerViewModel), typeof(IMediator<ApplicationClosing>));

        builder.AddTransient<WordOccurrenceAnalyzerViewModel>();

        ServiceProvider = builder.Build();

        MainWindow = ServiceProvider.GetService<MainWindow>();
        MainWindow.Show();
        base.OnStartup(e);
    }

    protected override void OnExit(ExitEventArgs e)
    {
        ServiceProvider.GetService<IMediator<ApplicationClosing>>().Publish(new());
    }

    static void AddAsyncDataAnalyzationServices(ServiceProviderBuilder builder) 
    {
        builder.AddSingleton<IWordOccurrenceAnalyzationRunner, AsyncWordOccurrenceAnalyzerRunner>();
        builder.AddSingleton<IAsyncDataAnalyzer<AsyncWordOccurrenceCounterExecutionContext>, AsyncWordOccurrenceAnalyzer>();

        builder.AddSingleton<IWordOccurrenceCounterFactory, DictionaryWordOccurrenceCounterFactory>();
    }
    static void AddThreadedDataAnalyzationServices(ServiceProviderBuilder builder)
    {
        builder.AddSingleton<IWordOccurrenceAnalyzationRunner, ThreadedWordOccurrenceAnalyzerRunner>();
        builder.AddSingleton<IDataAnalyzer<WordOccurrenceCounterExecutionContext>, WordOccurrenceAnalyzer>();
        builder.AddSingleton<IProgressFrequencyProvider>(sp => new ProgressFrequencyProvider() { Frequency = TimeSpan.FromSeconds(0.5) });
        builder.AddSingleton<IWordOccurrenceCounterFactory, ConcurrentDictionaryWordOccurrenceCounterFactory>();
    }

}
