using System.Collections.Generic;

namespace Ticketmanagement.BusinessLogic.Interfaces
{
    public interface ICrud<T>
        where T : class
    {
        IEnumerable<T> GetAllElements(); // get all objects

        T GetElementById(int id); // get one object

        void CreateElement(T item); // add object

        void UpdateElement(T item); // change object

        void DeleteElement(int id); // delete object
    }
}
