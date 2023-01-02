using Microsoft.EntityFrameworkCore;
using VuelosAPI.Models;

namespace VuelosAPI.Repositories
{
    public class VeulosAPIRepository<T> where T : class
    {
        public sistem21_aeromexicoContext Context { get; }

        public VeulosAPIRepository(sistem21_aeromexicoContext context)
        {
            Context = context;
        }


        public DbSet<T> Get()
        {
            return Context.Set<T>();
        }

        public T? Get(object id)
        {
            return Context.Find<T>(id);
        }
        public void Insert(T entidad)
        {
            Context.Add(entidad);
            Context.SaveChanges();
        }

        public void Update(T entidad)
        {
            Context.Update(entidad);
            Context.SaveChanges();
        }

        public void Delete(T entidad)
        {
            Context.Remove(entidad);
            Context.SaveChanges();
        }
    }
}
