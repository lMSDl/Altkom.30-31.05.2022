namespace ConsoleApp.Services
{
    public class ConsoleService : IOutputService
    {
        private IFontService _fontService;

        public ConsoleService(IFontService fontService)
        {
            _fontService = fontService;
        }

        public void WriteLine(string input)
        {
            Console.WriteLine(_fontService.Render(input));
        }
    }
}
