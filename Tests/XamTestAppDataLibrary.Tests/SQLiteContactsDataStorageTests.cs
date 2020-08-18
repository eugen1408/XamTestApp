using System.IO;
using SQLite;
using XamTestAppDataLibrary.Services;
using Xunit;

namespace XamTestAppDataLibrary.Tests
{
    public class SQLiteContactsDataStorageTests
    {
        [Fact]
        public async void SaveContactsAsync_ShouldInsertToDB()
        {
            // Arrange
            var httpJsonContactsDataSource = new HttpJsonContactsDataSource("https://raw.githubusercontent.com/Newbilius/ElbaMobileXamarinDeveloperTest/master/json/generated-01.json");
            var SQLiteContactsDataStorage = new SQLiteContactsCache(Path.Combine(Path.GetTempPath(), "XamTestEvaganov.db"), SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache);

            // Act
            var contacts = await httpJsonContactsDataSource.GetContactsAsync();
            var actual = await SQLiteContactsDataStorage.SaveContactsAsync(contacts);

            // Assert
            Assert.NotEqual(0, actual);
        }

        [Fact]
        public async void GetContactsAsync_ShouldReadFromDB()
        {
            // Arrange
            var SQLiteContactsDataStorage = new SQLiteContactsCache(Path.Combine(Path.GetTempPath(), "XamTestEvaganov.db"), SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache);

            // Act
            var actual = await SQLiteContactsDataStorage.GetContactsAsync();

            // Assert
            foreach (var contact in actual)
            {
                Assert.NotNull(contact.Id);
                Assert.NotNull(contact.Biography);
                Assert.NotNull(contact.EducationPeriod);
                Assert.NotNull(contact.Name);
                Assert.NotNull(contact.Phone);
                Assert.NotEqual(0, contact.Height);
            }
        }
    }
}
