using System.Collections.Generic;
using System.Threading.Tasks;
using XamTestAppDataLibrary.Models;

namespace XamTestAppDataLibrary.Services
{
    public interface IContactDataSourceCollection
    {
        Task<List<Contact>> GetContactsAsync();
    }
}