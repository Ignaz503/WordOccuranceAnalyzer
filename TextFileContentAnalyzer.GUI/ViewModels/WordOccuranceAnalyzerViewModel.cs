using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Windows;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Win32;

using TextFileContentAnalyzer.GUI.Commands;
using TextFileContentAnalyzer.Core.DataAnalyzer;
using TextFileContentAnalyzer.GUI.Util;
using TextFileContentAnalyzer.Core.Optional;
using TextFileContentAnalyzer.Core.DataAnalyzers.WordOccurance.Collections;
using TextFileContentAnalyzer.GUI.Services;
using TextFileContentAnalyzer.GUI.Services.WordOccuranceAnalyzerRunners;

namespace TextFileContentAnalyzer.GUI.ViewModels;
public class WordOccuranceAnalyzerViewModel : BaseViewModel
{
    const string PlaceHolder = "Choose A File...";

    private string filePath;

    public string FilePath
    {
        get => filePath;
        set 
        {
            var oldValue = filePath;

            NotifyPropertyChanged(ref filePath, value, propagate: nameof(ShortPath));

            if (oldValue != PlaceHolder) 
            {
                //switching files
                HandleSwitchingFiles();
            }

        }
    }

    public string ShortPath => Path.GetFileName(filePath);

    private int currentProgress;
    public int CurrentProgress
    {
        get => currentProgress;
        set 
        {
            NotifyPropertyChanged(ref currentProgress, value);
        }
    }


    IWordOccuranceCounter wordOccuranceCounter;
    public IEnumerable<WordBucket> WordOccurances 
    {
        get => wordOccuranceCounter.EnumerateDescending();
    }

    bool resultVisibilty;
    public bool ResultVisibility 
    {
        get => resultVisibilty;
        set => NotifyPropertyChanged(ref resultVisibilty, value);
    }

    public int maxProgressValue = int.MaxValue;
    public int MaxProgressValue
    {
        get => maxProgressValue;
        set => NotifyPropertyChanged(ref maxProgressValue, value);
    }


    readonly AsyncCommand _analyzeFileCommand;
    readonly RelayCommand _cancelAnalyzation;


    ICommand _activeAnalyzerCommand;
    public ICommand ActiveAnalyzerCommand 
    {
        get => _activeAnalyzerCommand;
        private set => NotifyPropertyChanged(ref _activeAnalyzerCommand, value, propagate: nameof(AnalyzeButtonName));
    }

    public string AnalyzeButtonName 
    {
        get 
        {
            if (ActiveAnalyzerCommand == _analyzeFileCommand)
                return "Start";
            return "Cancel";
        }
    }

    public RelayCommand FilePicker { get; private set; }

    readonly IWordOccuranceAnalyzationRunner _occuranceAnalzyer;
    readonly PeriodicForcedAsyncExecutionProgressReport<long> progressTracker;

    //hacky context switch but safer than doing ui updates on any thread
    readonly IProgress<Result<Okay, Exception>> resultRecievedProgress;
    readonly ForcedAsyncExecutionStatefulProgressReport<int> beforeRunUpdate;
    readonly IWordOccuranceCounterFactory counterFactory;

    public WordOccuranceAnalyzerViewModel(IWordOccuranceAnalyzationRunner runner, IWordOccuranceCounterFactory counterFactory)
    {
        FilePicker = new(OpenFilePicker, FilePickerCanExecute);
        filePath = PlaceHolder;
        this._occuranceAnalzyer = runner;

        this._cancelAnalyzation = new RelayCommand(CancelAnalyzation, obj => true);
        this._analyzeFileCommand = new AsyncCommand(AnalyzerCanRun, RunAnalyzer, HandleUnkownException);

        this._activeAnalyzerCommand = _analyzeFileCommand;
        
        progressTracker = new(TimeSpan.FromSeconds(0.5), (val) => CurrentProgress = (int)val);
        resultRecievedProgress = new Progress<Result<Okay, Exception>>(FinishFileAnalyzation);
        beforeRunUpdate = new ForcedAsyncExecutionStatefulProgressReport<int>(HandleBeforeRun);
        this.counterFactory = counterFactory;
        this.wordOccuranceCounter = counterFactory.Get();
    }

    public override void Dispose()
    {
        _analyzeFileCommand?.Dispose();
    }

    void OpenFilePicker(object? param) 
    {
        var dialog = new OpenFileDialog();

        if(dialog.ShowDialog() == true)
            FilePath = dialog.FileName;
    }

    bool FilePickerCanExecute(object? param)
        => true;

    void CancelAnalyzation(object? param) 
    {
        _analyzeFileCommand.Cancel();
        ActiveAnalyzerCommand = _analyzeFileCommand;
        CurrentProgress = 0;
        MaxProgressValue = int.MaxValue;
    }

    bool AnalyzerCanRun(object? obj)
        => FilePath != PlaceHolder && Path.GetFileName(FilePath) != string.Empty && Path.IsPathFullyQualified(FilePath);


    async Task RunAnalyzer(AsyncCommandExecutionContext ctx)
    {
        wordOccuranceCounter = counterFactory.Get();

        FileStream stream = new(
            filePath,
            FileMode.Open, FileAccess.Read, FileShare.Read,
            bufferSize: 4096, useAsync: true);

        await beforeRunUpdate.Report((int)stream.Length);

        await _occuranceAnalzyer.Run(stream, wordOccuranceCounter, progressTracker, resultRecievedProgress, ctx.Token);


    }

    private void FinishFileAnalyzation(Result<Okay, Exception> res)
    {
        CurrentProgress = res.HasValue ? MaxProgressValue : CurrentProgress;
        res.Match(HandleSuccessfulAnalyzation, HandleUnsusccesfulAnalyzation);
    }

    void HandleBeforeRun(int max) 
    {
        MaxProgressValue = max;
        ActiveAnalyzerCommand = _cancelAnalyzation;
        ResultVisibility = false;
    }

    void HandleUnkownException(Exception ex) 
    {
        MessageBox.Show(ex.Message, "Unhandled exception occured", MessageBoxButton.OK, MessageBoxImage.Error);
    }

    void HandleSuccessfulAnalyzation(Okay ok) 
    {
        NotifyPropertyChanged(name: nameof(WordOccurances));
        ResultVisibility = true;
        ActiveAnalyzerCommand = _analyzeFileCommand;
    }

    void HandleUnsusccesfulAnalyzation(Exception? exception) 
    {
        if (exception is null)
        {
            MessageBox.Show("Unkown exception ", "Unhandled Exception Occured", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        switch (exception)
        {
            case TaskCanceledException _:
            case OperationCanceledException _:
                break;
            default:
                MessageBox.Show(exception.Message, "Unhandled Exception Occured", MessageBoxButton.OK, MessageBoxImage.Error);
                break;
        }
    }

    private void HandleSwitchingFiles()
    {
        if (_analyzeFileCommand.IsBusy) 
        {
            //active analyzer command is cancel in this case
            ActiveAnalyzerCommand.Execute(this);
        }
        MaxProgressValue = int.MaxValue;
        ResultVisibility = false;
        ActiveAnalyzerCommand = _analyzeFileCommand;
    }

}
