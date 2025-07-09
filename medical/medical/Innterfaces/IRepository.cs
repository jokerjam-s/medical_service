using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medical
{
    interface IRepository<T>
    {
        void Delete(T entity);

        void Update(T entity);

        void Add(T entity);

        ObservableCollection<T> GetAll();

    }
}
