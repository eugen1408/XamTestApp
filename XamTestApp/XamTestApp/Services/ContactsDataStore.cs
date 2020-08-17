using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using XamTestAppDataLibrary.Models;
using XamTestAppDataLibrary.Services;

namespace XamTestApp.Services
{
    public static class ContactsDataStore
    {
        #region Fields

        private static readonly TimeSpan _cacheTTL = TimeSpan.FromMinutes(Consts.CacheTTLMinutes);

        private static readonly IContactDataSourceCollection _sourceCollection = new ContactDataSourceCollection
            {
                new HttpJsonContactsDataSource("https://raw.githubusercontent.com/Newbilius/ElbaMobileXamarinDeveloperTest/master/json/generated-01.json"),
                new HttpJsonContactsDataSource("https://raw.githubusercontent.com/Newbilius/ElbaMobileXamarinDeveloperTest/master/json/generated-02.json"),
                new HttpJsonContactsDataSource("https://raw.githubusercontent.com/Newbilius/ElbaMobileXamarinDeveloperTest/master/json/generated-03.json")
            };

        private static readonly IContactsDataStorage _contactsDataStorage = new SQLiteContactsDataStorage(Consts.DatabasePath, Consts.DatabaseFlags); 
        #endregion

        public static async Task<IEnumerable<Contact>> GetContactsAsync(LoadContactsMethod loadContactsMethod)
        {
            const string LastGetContactsTime = "LastGetContactsTime";
            var lastGetContactsTime = Preferences.Get(LastGetContactsTime, DateTime.MinValue);
            IEnumerable<Contact> output;
            if (loadContactsMethod == LoadContactsMethod.ReloadFromCache)
            {
                output = await _contactsDataStorage.GetContactsAsync();
            }
            // принудительное обновление или вышло время жизни кэша
            else if (loadContactsMethod == LoadContactsMethod.ForceReload || DateTime.UtcNow.Subtract(_cacheTTL) > lastGetContactsTime)
            {
                output = await _sourceCollection.GetContactsAsync();
                await _contactsDataStorage.SaveContactsAsync(output);
                Preferences.Set(LastGetContactsTime, DateTime.UtcNow);
            }
            // иначе берем из кэша
            else
            {
                output = await _contactsDataStorage.GetContactsAsync();
            }
            return output.OrderBy(p => p.Name);
        }
    }
}