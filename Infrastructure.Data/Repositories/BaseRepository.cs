using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly IMongoContext Context;
        protected IMongoCollection<TEntity> DbSet;

        protected BaseRepository(IMongoContext context)
        {
            Context = context;
            DbSet = context.GetCollection<TEntity>();
        }

        public virtual async Task AddAsync(TEntity obj) 
            => await DbSet.InsertOneAsync(obj);

        public virtual async Task<TEntity> GetByIdAsync(Guid id)
        {
            var data = await DbSet.FindAsync(Builders<TEntity>.Filter.Eq("_id", id));
            return data.SingleOrDefault();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var all = await DbSet.FindAsync(Builders<TEntity>.Filter.Empty);
            return all.ToList();
        }

        public virtual async Task RemoveAsync(Guid id) 
            => await DbSet.DeleteOneAsync(Builders<TEntity>.Filter.Eq("_id", id));

        public void Dispose() 
            => Context?.Dispose();
    }
}
