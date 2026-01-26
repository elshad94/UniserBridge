using Project.Core.DataAccess.Concrete.EntityFramework.Contexts;
using Project.Core.Entities.Models;

namespace Project.Core.Utilities.Logging.Loggers
{
    public class DefaultLogger : ILogger
    {
        public string Log(string content, LogType logType)
        {
            var log = new SystemLog
            {
                Content = content,
                CreateDate = DateTime.Now,
                RequestUrl = CurrentScopeDataContainer.Instance.RequestUrl,
                UserId = CurrentScopeDataContainer.Instance.UserId,
                Type = logType.ToString()
            };

            using var context = new ProjectAppDbContext();
            context.SystemLogs.Add(log);
            context.SaveChanges();

            return log.Id.ToString();
        }
    }
}
