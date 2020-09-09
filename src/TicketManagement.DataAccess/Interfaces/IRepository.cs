using System.Collections.Generic;

namespace TicketManagement.DataAccess.Interfaces
{
    public interface IRepository<T>
        where T : class
    {
        IEnumerable<T> GetAll(); // get all objects

        T GetById(int id); // get one object

        void Create(T item); // add object

        void Update(T item); // change object

        void Delete(int id); // delete object
    }
}
