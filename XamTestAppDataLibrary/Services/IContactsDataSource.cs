using System.Collections.Generic;
using System.Threading.Tasks;
using XamTestAppDataLibrary.Models;

namespace XamTestAppDataLibrary.Services
{
    public interface IContactsDataSource
    {
        Task<List<Contact>> GetContactsAsync();
    }
}