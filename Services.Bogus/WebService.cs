using Models;
using Services.Bogus.Fakers;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Bogus
{
    public class WebService<T> : IWebService<T> where T : Entity
    {
        private ICollection<T> _entities;

        public WebService(EntityFaker<T> faker)
        {
            _entities = faker.Generate(new Random().Next(1, 100));
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await ReadAsync(id);
            if(entity != null)
                _entities.Remove(entity);
        }

        public Task<T?> ReadAsync(int id)
        {
            return Task.FromResult(_entities.SingleOrDefault(x => x.Id == id));
        }

        public Task<IEnumerable<T>> ReadAsync()
        {
            return Task.FromResult(_entities.AsEnumerable());
        }
    }
}
