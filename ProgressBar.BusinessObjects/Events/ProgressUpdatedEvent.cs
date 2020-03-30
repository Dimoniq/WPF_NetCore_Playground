using Prism.Events;
using ProgressBar.BusinessObjects.EventsPayload;

namespace ProgressBar.BusinessObjects.Events
{
  public class ProgressUpdatedEvent : PubSubEvent<ProgressUpdatePayload>
  {
    
  }
}