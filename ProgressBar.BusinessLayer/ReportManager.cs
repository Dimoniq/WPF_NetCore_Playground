using System;
using System.Threading;
using System.Threading.Tasks;
using Prism.Events;
using ProgressBar.BusinessObjects.Events;
using ProgressBar.BusinessObjects.EventsPayload;
using ProgressBar.Contracts;

namespace ProgressBar.BusinessLayer
{
  public class ReportManager : IReportManager
  {
    private readonly IEventAggregator ea;

    public ReportManager(IEventAggregator ea)
    {
      this.ea = ea;
    }
    public async Task DoSomeJob(int days)
    {
      await Task.Run(() =>
      {
        var random = new Random();
        for (int i = 1; i <= 100; i++)
        {
          Thread.Sleep(random.Next(50,250));
          this.UpdateProgress(i, $"Processing file for day {i}");
        }
        this.UpdateProgress(100, $"Job done !");
      });
    }

    private void UpdateProgress(double progress, string jobTitle)
    {
      this.ea.GetEvent<ProgressUpdatedEvent>().Publish(new ProgressUpdatePayload{Progress = progress, JobTitle = jobTitle});
    }
  }
}