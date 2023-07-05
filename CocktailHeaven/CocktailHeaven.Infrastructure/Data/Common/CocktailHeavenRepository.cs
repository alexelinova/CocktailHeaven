using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CocktailHeaven.Infrastructure.Data.Common
{
    public class CocktailHeavenRepository : IRepository
    {
        public CocktailHeavenRepository(CocktailHeavenDbContext context)
        {
            this.Context = context;
        }

		protected DbContext Context { get; set; }

        public IQueryable<T> All<T>()
            where T : class
        {
            return this.DbSet<T>().AsQueryable();
        }

        public IQueryable<T> All<T>(Expression<Func<T, bool>> search)
            where T : class
        {
            return this.DbSet<T>().Where(search).AsQueryable();
        }

        public IQueryable<T> AllReadonly<T>()
            where T : class
        {
            return this.DbSet<T>()
                .AsQueryable()
                .AsNoTracking();
        }

        public IQueryable<T> AllReadonly<T>(Expression<Func<T, bool>> search)
            where T : class
        {
            return this.DbSet<T>()
                .Where(search)
                .AsQueryable()
                .AsNoTracking();
        }

        public async Task<T> GetByIdAsync<T>(object id)
            where T : class
        {
            return await this.DbSet<T>().FindAsync(id);
        }

        public async Task<T> GetByIdsAsync<T>(object[] id)
            where T : class
        {
            return await this.DbSet<T>().FindAsync(id);
        }

        public async Task AddAsync<T>(T entity)
            where T : class
        {
            await this.DbSet<T>().AddAsync(entity);
        }

        public async Task AddRangeAsync<T>(IEnumerable<T> entities)
            where T : class
        {
            await this.DbSet<T>().AddRangeAsync(entities);
        }

        public void Update<T>(T entity)
            where T : class
        {
            this.DbSet<T>().Update(entity);
        }

        public void UpdateRange<T>(IEnumerable<T> entities)
            where T : class
        {
            this.DbSet<T>().UpdateRange(entities);
        }

        public async Task DeleteAsync<T>(object id)
            where T : class
        {
            T entity = await this.GetByIdAsync<T>(id);

            this.Delete<T>(entity);
        }

        public void Delete<T>(T entity)
            where T : class
        {
            EntityEntry entry = this.Context.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                this.DbSet<T>().Attach(entity);
            }

            entry.State = EntityState.Deleted;
        }

        public void DeleteRange<T>(IEnumerable<T> entities)
            where T : class
        {
            this.DbSet<T>().RemoveRange(entities);
        }

        public void Detach<T>(T entity)
            where T : class
        {
            EntityEntry entry = this.Context.Entry(entity);

            entry.State = EntityState.Detached;
        }

        public int SaveChanges()
        {
            return this.Context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await this.Context.SaveChangesAsync();
        }

        public void DeleteRange<T>(Expression<Func<T, bool>> deleteWhereClause)
            where T : class
        {
            var entities = this.All<T>(deleteWhereClause);
            this.DeleteRange(entities);
        }

        public void Dispose()
        {
            this.Context.Dispose();
        }

        protected DbSet<T> DbSet<T>()
            where T : class
        {
            return this.Context.Set<T>();
        }
    }
}
