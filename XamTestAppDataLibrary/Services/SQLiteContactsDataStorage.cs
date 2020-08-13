using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using XamTestAppDataLibrary.Models;

namespace XamTestAppDataLibrary.Services
{
    public class SQLiteContactsDataStorage : IContactsDataSource, IContactsDataStorage
    {

        private readonly SQLiteAsyncConnection _connection;

        public SQLiteContactsDataStorage(SQLiteAsyncConnection connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }


        public Task<List<Contact>> GetContactsAsync() => _connection.Table<Contact>().ToListAsync();

        public async Task<int> SaveContactsAsync(IEnumerable<Contact> contacts)
        {
            await _connection.DeleteAllAsync<Contact>();
            return await _connection.InsertAllAsync(contacts);
        }
    }
}
