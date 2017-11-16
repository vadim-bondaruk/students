using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsApp.Interfaces
{
    public interface IDataContext<T>
    {
        ObservableCollection<T> GetAll();
        void AddOrUpdate(T item);
        void Delete(T item);
        void SaveChanges();
    }
}
