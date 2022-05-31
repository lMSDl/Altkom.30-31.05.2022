using Bogus;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Bogus.Fakers
{
    public abstract class EntityFaker<T> : Faker<T> where T : Entity
    {
        protected EntityFaker() : base("pl")
        {
            StrictMode(true);

            RuleFor(x => x.Id, x => x.UniqueIndex);
        }
    }
}
