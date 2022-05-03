using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface ICustomGenericService<T>
    {
        int CreateObject(T? entity);
        int DeleteObject(T? entity);
        int UpdateObject(T? entity);
        T GetObjectById(int? id);
        IEnumerable<T> GetAllObjects();
    }
}
