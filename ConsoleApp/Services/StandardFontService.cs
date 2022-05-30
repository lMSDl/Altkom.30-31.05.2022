using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Services
{
    public class StandardFontService : IFontService
    {
        public StandardFontService(ILogger<StandardFontService> logger)
        {
            logger.LogInformation("StandardFontService");
        }

        public string Render(string input)
        {
            return Figgle.FiggleFonts.Standard.Render(input);
        }
    }
}
