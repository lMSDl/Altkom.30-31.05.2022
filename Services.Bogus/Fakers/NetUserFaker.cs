using Bogus;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Bogus.Fakers
{
    public class WebUserFaker : EntityFaker<WebUser>
    { 
        public WebUserFaker()
        {
            StrictMode(true);

            RuleFor(x => x.UserName, x => x.Internet.UserName());
            RuleFor(x => x.Email, x => x.Person.Email);
            RuleFor(x => x.Password, x => x.Internet.Password());
        }
    }
}
