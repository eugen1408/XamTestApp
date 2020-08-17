using System.Collections.Generic;
using System.Threading.Tasks;
using XamTestAppDataLibrary.Models;

namespace XamTestAppDataLibrary.Services
{
    public interface IContactsDataStorage : IContactsDataSource
    {
        Task<int> SaveContactsAsync(IEnumerable<Contact> contacts);
    }
}
