using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SQLite;
using XamTestAppDataLibrary.Helpers;
using XamTestAppDataLibrary.Models;

namespace XamTestAppDataLibrary.Services
{
    public class SQLiteContactsDataStorage : IContactsDataStorage
    {
        private readonly string _databasePath;
        private readonly SQLiteOpenFlags _flags;
        private readonly Lazy<SQLiteAsyncConnection> _lazyInitializer;

        public SQLiteContactsDataStorage(string databasePath, SQLiteOpenFlags flags)
        {
            _databasePath = databasePath ?? throw new ArgumentNullException(nameof(databasePath));
            _flags = flags;
            // lazy-инициализация БД взята из примера: https://docs.microsoft.com/en-us/xamarin/xamarin-forms/data-cloud/data/databases
            _lazyInitializer = new Lazy<SQLiteAsyncConnection>(() => new SQLiteAsyncConnection(_databasePath, _flags));
            InitializeAsync().SafeFireAndForget(false);
        }

        private SQLiteAsyncConnection Connection => _lazyInitializer.Value;
        private bool _initialized = false;

        private async Task InitializeAsync()
        {
            if (!_initialized)
            {
                if (!Connection.TableMappings.Any(m => m.MappedType.Name == typeof(Contact).Name))
                {
                    await Connection.CreateTablesAsync(CreateFlags.None, typeof(Contact)).ConfigureAwait(false);
                }
                _initialized = true;
            }
        }



        public Task<List<Contact>> GetContactsAsync() => Connection.Table<Contact>().ToListAsync();

        public async Task<int> SaveContactsAsync(IEnumerable<Contact> contacts)
        {
            await Connection.DeleteAllAsync<Contact>();
            return await Connection.InsertAllAsync(contacts);
        }
    }
}
