using CreateCustomer.API.Contracts;
using CreateCustomer.API.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateCustomer.API.Repositories
{
    public class GenericRepository<T> : IRepository<T>, IAsyncRepository<T> where T : BaseEntity
    {
        protected readonly CustomerContext _context;

        public GenericRepository(CustomerContext context)
        {
            _context = context;
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public ICollection<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public async Task<ICollection<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();

        }

        public int Add(T entity, bool persist = true)
        {
            _context.Set<T>().Add(entity);
            return persist ? _context.SaveChanges():0;            
        }

        public async Task<int> AddAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            return await _context.SaveChangesAsync();             
        }

        public void AddRange(ICollection<T> entities)
        {
            _context.Set<T>().AddRange(entities);
            _context.SaveChanges();
        }

        public async Task AddRangeAsync(ICollection<T> entities)
        {
            _context.Set<T>().AddRange(entities);
            await _context.SaveChangesAsync();
        }

        public void Update(T entity, bool persist = true)
        {
            _context.Entry(entity).State = EntityState.Modified;
            if(persist) _context.SaveChanges();
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public void Delete(T entity, bool persist = true)
        {
            _context.Set<T>().Remove(entity);
            if(persist) _context.SaveChanges();
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public int GetSurrogateKey()
        {
            var tableName = GetTableName(typeof(T), _context);

            var tableNameParameter = new SqlParameter() { ParameterName = "iTableName", Value = tableName, SqlDbType = System.Data.SqlDbType.VarChar };
            var keyParameter = new SqlParameter() { ParameterName = "oNewKey", Value = 0, SqlDbType = System.Data.SqlDbType.Int, Direction = System.Data.ParameterDirection.Output };

            var result = _context.Database.ExecuteSqlCommand("exec spGetNextSurrogateKey @iTableName, @oNewKey OUTPUT", tableNameParameter, keyParameter);

            return (int)keyParameter.Value;
        }

        public async Task<int> GetSurrogateKeyAsync()
        {
            var tableName = GetTableName(typeof(T), _context);

            var tableNameParameter = new SqlParameter() { ParameterName = "iTableName", Value = tableName, SqlDbType = System.Data.SqlDbType.VarChar };
            var keyParameter = new SqlParameter() { ParameterName = "oNewKey", Value = 0, SqlDbType = System.Data.SqlDbType.Int, Direction = System.Data.ParameterDirection.Output };

            var result = await _context.Database.ExecuteSqlCommandAsync("exec spGetNextSurrogateKey @iTableName, @oNewKey OUTPUT", tableNameParameter, keyParameter);

            return (int)keyParameter.Value;
        }

        public static string GetTableName(Type type, CustomerContext context)
        {
            var metadata = ((IObjectContextAdapter)context).ObjectContext.MetadataWorkspace;

            // Get the part of the model that contains info about the actual CLR types
            var objectItemCollection = ((ObjectItemCollection)metadata.GetItemCollection(DataSpace.OSpace));

            // Get the entity type from the model that maps to the CLR type
            var entityType = metadata
                    .GetItems<EntityType>(DataSpace.OSpace)
                    .Single(e => objectItemCollection.GetClrType(e) == type);

            // Get the entity set that uses this entity type
            var entitySet = metadata
                .GetItems<EntityContainer>(DataSpace.CSpace)
                .Single()
                .EntitySets
                .Single(s => s.ElementType.Name == entityType.Name);

            // Find the mapping between conceptual and storage model for this entity set
            var mapping = metadata.GetItems<EntityContainerMapping>(DataSpace.CSSpace)
                    .Single()
                    .EntitySetMappings
                    .Single(s => s.EntitySet == entitySet);

            // Find the storage entity set (table) that the entity is mapped
            var table = mapping
                .EntityTypeMappings.Single()
                .Fragments.Single()
                .StoreEntitySet;

            // Return the table name from the storage entity set
            return (string)table.MetadataProperties["Table"].Value ?? table.Name;
        }
    }

        
}
