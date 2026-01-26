namespace Project.Core.Utilities.Logging
{
    public interface ILogger
    {
        string Log(string content, LogType logType);
    }

    public enum LogType
    {
        Info,
        Exception, 
        BadRequest
    }
}
