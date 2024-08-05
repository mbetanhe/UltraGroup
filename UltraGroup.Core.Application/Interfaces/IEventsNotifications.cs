using UltraGroup.Core.Application.Events;

namespace UltraGroup.Core.Application.Interfaces
{
    public interface IEventsNotifications
    {
        public event EventHandler<LogsEventArgs> logInfoEvent;
    }
}
