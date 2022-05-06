using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface ICustomGenericServiceAsync<T>
    {
        Task<int> CreateObjectAsync(T? entity);
        Task<int> DeleteObjectAsync(T? entity);
        Task<int> UpdateObjectAsync(T? entity);
        Task<T> GetObjectByIdAsync(int? id);
        Task<IEnumerable<T>> GetAllObjectsAsync();
    }
}
