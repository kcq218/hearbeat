using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace HeartBeat
{
    public class HeartBeat
    {
        private readonly ILogger _logger;

        public HeartBeat(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<HeartBeat>();
        }

        [Function("HearBeat")]
        public void Run([TimerTrigger("*/30 * * * *")] TimerInfo myTimer)
        {
            try
            {
                _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

                if (myTimer.ScheduleStatus is not null)
                {
                    _logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
                }

                var context = new DbAll01ProdUswest001Context();

                var read = context.UrlMappings.First();
                _logger.LogInformation($"result of read: Id {read.Id}");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error in HeartBeat");
            }
        }
    }
}
