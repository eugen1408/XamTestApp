using System.Collections.Generic;
using System.Threading.Tasks;
using XamTestAppDataLibrary.Models;

namespace XamTestAppDataLibrary.Services
{
    public class ContactDataSourceCollection : List<IContactsDataSource>, IContactDataSourceCollection
    {
        public async Task<List<Contact>> GetContactsAsync()
        {
            var output = new List<Contact>();

            foreach (var contactsDataSource in this)
            {
                var contacts = await contactsDataSource.GetContactsAsync();
                output.AddRange(contacts);
            }

            return output;
        }
    }
}
