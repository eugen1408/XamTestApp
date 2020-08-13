using System.IO;
using System.Linq;
using SQLite;
using XamTestAppDataLibrary.Models;
using XamTestAppDataLibrary.Services;
using Xunit;

namespace XamTestAppDataLibrary.Tests
{
    public class SQLiteContactsDataStorageTests
    {
        private readonly SQLiteAsyncConnection _connection = new SQLiteAsyncConnection(Path.Combine(Path.GetTempPath(), "XamTestEvaganov.db"), SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);

        [Fact]
        public async void SaveContactsAsync_ShouldInsertToDB()
        {
            // Arrange
            var httpJsonContactsDataSource = new HttpJsonContactsDataSource("https://raw.githubusercontent.com/Newbilius/ElbaMobileXamarinDeveloperTest/master/json/generated-01.json");
            var SQLiteContactsDataStorage = new SQLiteContactsDataStorage(_connection);

            // Act
            if (!_connection.TableMappings.Any(m => m.MappedType.Name == typeof(Contact).Name))
            {
                await _connection.CreateTablesAsync(CreateFlags.None, typeof(Contact));
            }
            var contacts = await httpJsonContactsDataSource.GetContactsAsync();
            var actual = await SQLiteContactsDataStorage.SaveContactsAsync(contacts);

            // Assert
            Assert.NotEqual(0, actual);
        }

        [Fact]
        public async void GetContactsAsync_ShouldReadFromDB()
        {
            // Arrange
            var SQLiteContactsDataStorage = new SQLiteContactsDataStorage(_connection);

            // Act
            if (!_connection.TableMappings.Any(m => m.MappedType.Name == typeof(Contact).Name))
            {
                await _connection.CreateTablesAsync(CreateFlags.None, typeof(Contact));
            }
            var actual = await SQLiteContactsDataStorage.GetContactsAsync();

            // Assert
            Assert.NotEmpty(actual);
            foreach (var contact in actual)
            {
                Assert.NotNull(contact.Id);
                Assert.NotNull(contact.Biography);
                Assert.NotNull(contact.EducationPeriod);
                Assert.NotNull(contact.Name);
                Assert.NotEqual(0, contact.Height);
            }
        }
    }
}
