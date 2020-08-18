using System.Collections.Generic;
using System.Threading.Tasks;
using XamTestAppDataLibrary.Models;

namespace XamTestAppDataLibrary.Services
{
    public interface IContactsCache : IContactsDataSource
    {
        Task<int> SaveContactsAsync(IEnumerable<Contact> contacts);
    }
}
