using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrismDemo.Services
{
    /// <summary>
    /// Common local database action
    /// </summary>
    /// <typeparam name="T">POCO class</typeparam>
    /// <Modified>
    /// Name Date Comments
    /// annv3 16/08/2022 created
    /// </Modified>
    public interface IDBServices<T> where T : class
    {
        Task InitDB();
        Task<bool> Create(T item);
        Task<bool> Create(IEnumerable<T> item);
        Task<bool> Delete(T item);
        Task<bool> Update(T item);
        Task<bool> Update(IEnumerable<T> item);
        Task<T> GetData(object key);
        Task<IEnumerable<T>> GetData(int skip = 0, int take = 0);
    }
}
