using Microsoft.Extensions.Logging;

namespace ConsoleApp.Services
{
    public class RandomFontConsoleService : IOutputService
    {
        private IFontService _fontService;
        public RandomFontConsoleService(IEnumerable<IFontService> fontServices, ILogger<RandomFontConsoleService> logger)
        {
            Thread.Sleep(1000);
            var value = new Random(DateTime.Now.Second).Next(0, fontServices.Count());
            logger.LogInformation(value.ToString());
            _fontService = fontServices.Skip(value).First();
            logger.LogInformation("RandomFontConsoleService");
        }

        public void WriteLine(string input)
        {
            Console.WriteLine(_fontService.Render(input));
        }
    }
}
