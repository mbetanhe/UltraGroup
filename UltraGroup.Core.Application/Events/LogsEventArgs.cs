namespace UltraGroup.Core.Application.Events
{
    public class LogsEventArgs : EventArgs
    {
        public string Message { get; set; }

        public bool Error { get; set; }

        public LogsEventArgs(string message, bool error)  => (Message, Error) = (message, error);   
    }
}
