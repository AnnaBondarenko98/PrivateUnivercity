using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Anna_Bondarenko_FinalTask.DAL.Interfaces
{
    public interface IСommitteeContext
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        int SaveChanges();
    }
}
