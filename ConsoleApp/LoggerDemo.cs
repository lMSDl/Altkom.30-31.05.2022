using Microsoft.Extensions.Logging;

namespace ConsoleApp
{
    public class LoggerDemo
    {
        private ILogger<LoggerDemo> _logger;

        public LoggerDemo(ILogger<LoggerDemo> logger)
        {
            _logger = logger;
        }

        public void Work()
        {
            _logger.LogTrace("Begin Work");

            using (var loggerScope = _logger.BeginScope("for counter"))
            {

                for (int i = 0; i < 10; i++)
                {
                    using (var loggerInnerScope = _logger.BeginScope($"Inner: {i}"))
                    {
                        try
                        {
                            _logger.LogDebug(i.ToString());
                            if (i == 5)
                                throw new IndexOutOfRangeException($"Index {5}");
                        }
                        catch (Exception e)
                        {
                            _logger.LogError(e, "Wyjątek");
                        }
                    }
                }
            }


            _logger.LogTrace("End Work");
        }
    }
}
