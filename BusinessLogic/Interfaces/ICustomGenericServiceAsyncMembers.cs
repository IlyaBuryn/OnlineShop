using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface ICustomGenericServiceAsyncMembers<T>
    {
        Task<int> CreateObjectAsync(T? entity);
        Task<int> DeleteObjectAsync(T? entity);
        Task<int> UpdateObjectAsync(T? entity);
        Task<T> GetObjectByIdAsync(int? id);
        Task<T> GetObjectByNameAsync(string name);
        Task<IEnumerable<T>> GetAllObjectsAsync();
    }
}
