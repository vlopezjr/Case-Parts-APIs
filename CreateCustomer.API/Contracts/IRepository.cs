

using CreateCustomer.API.Entities;
using System.Collections.Generic;

namespace CreateCustomer.API.Contracts
{
    public interface IRepository<T> where T : BaseEntity
    {
        T GetById(int id);
        ICollection<T> GetAll();
        int Add(T entity, bool persist = true);
        void AddRange(ICollection<T> entities);
        void Update(T entity, bool persist = true);
        void Delete(T entity, bool persist = true);
        int GetSurrogateKey();
    }
}
