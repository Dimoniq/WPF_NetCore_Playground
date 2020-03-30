using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Prism.Commands;
using Prism.Events;
using ProgressBar.BusinessObjects.Events;
using ProgressBar.Contracts;
using ProgressBar.PresentationLayer.Annotations;

namespace ProgressBar.PresentationLayer.ViewModels
{
  public class MainWindowViewModel : INotifyPropertyChanged
  {
    private readonly IReportManager reportManager;
    private double currentProgress;
    private bool infiniteProgressBar = true;
    private string jobTitle;

    public MainWindowViewModel(IEventAggregator ea, IReportManager reportManager)
    {
      this.reportManager = reportManager;
      this.DoWorkCommand = new DelegateCommand(this.PerformWork);
      ea.GetEvent<ProgressUpdatedEvent>().Subscribe(payload =>
      {
        this.CurrentProgress = payload.Progress;
        this.JobTitle = payload.JobTitle;
      }, ThreadOption.UIThread);
    }

    public ICommand DoWorkCommand { get; set; }

    public double CurrentProgress
    {
      get => this.currentProgress;
      set
      {
        if (Math.Abs(this.currentProgress - value) < 0.01)
        {
          return;
        }

        this.currentProgress = value;
        this.OnPropertyChanged();
      }
    }

    public string JobTitle
    {
      get => this.jobTitle;
      set
      {
        if (this.jobTitle == value)
        {
          return;
        }

        this.jobTitle = value;
        this.OnPropertyChanged();
      }
    }

    public bool InfiniteProgressBar
    {
      get => this.infiniteProgressBar;
      set
      {
        if (this.infiniteProgressBar == value)
        {
          return;
        }

        this.infiniteProgressBar = value;
        this.OnPropertyChanged();
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    private void PerformWork()
    {
      this.reportManager.DoSomeJob(20);
      this.InfiniteProgressBar = false;
    }

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}