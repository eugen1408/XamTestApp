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

        private static readonly IContactsCache _contactsCache = new SQLiteContactsCache(Consts.DatabasePath, Consts.DatabaseFlags); 
        #endregion

        public static async Task<IEnumerable<Contact>> GetContactsFromSourceAsync(bool forceReload)
        {
            const string LastGetContactsTime = "LastGetContactsTime";
            var lastGetContactsTime = Preferences.Get(LastGetContactsTime, DateTime.MinValue);
            IEnumerable<Contact> output;
            // принудительное обновление или вышло время жизни кэша
            if (forceReload || DateTime.UtcNow.Subtract(_cacheTTL) > lastGetContactsTime)
            {
                output = await _sourceCollection.GetContactsAsync();
                await _contactsCache.SaveContactsAsync(output);
                Preferences.Set(LastGetContactsTime, DateTime.UtcNow);
                return output.OrderBy(p => p.Name);
            }
            // иначе берем из кэша
            else
            {
                return await GetContactsFromCacheAsync();
            }
        }

        public static async Task<IEnumerable<Contact>> GetContactsFromCacheAsync()
        {
            IEnumerable<Contact> output;
            output = await _contactsCache.GetContactsAsync();
            return output.OrderBy(p => p.Name);
        }
    }
}