using System.Threading.Tasks;

namespace ProgressBar.Contracts
{
  public interface IReportManager
  {
    Task DoSomeJob(int days);
  }
}