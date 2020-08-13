using System.Collections.Generic;
using System.Threading.Tasks;
using XamTestAppDataLibrary.Models;

namespace XamTestAppDataLibrary.Services
{
    public class ContactDataSourceCollection : List<IContactsDataSource>
    {
        public async Task<List<Contact>> GetContactsAsync()
        {
            var output = new List<Contact>();

            // можно было бы сделать параллельное получение данных, но не стал усложнять
            foreach (var contactsDataSource in this)
            {
                var contacts = await contactsDataSource.GetContactsAsync();
                output.AddRange(contacts);
            }

            return output;
        }
    }
}
