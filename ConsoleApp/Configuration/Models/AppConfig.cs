using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Configuration.Models
{
    public class AppConfig
    {
        public int Repeat { get; set; }
        public Greetings Greetings { get; set; } = new();
    }
}
