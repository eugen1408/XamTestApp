using System;
using XamTestAppDataLibrary.Services;
using Xunit;

namespace XamTestAppDataLibrary.Tests
{
    public class ContactDataSourceCollectionTests
    {
        [Fact]
        public async void GetContactsAsync_ValidSources_ShouldReturnContacts()
        {
            // Arrange
            var contactDataSourceCollection = new ContactDataSourceCollection()
            {
                new HttpJsonContactsDataSource("https://raw.githubusercontent.com/Newbilius/ElbaMobileXamarinDeveloperTest/master/json/generated-01.json"),
                new HttpJsonContactsDataSource("https://raw.githubusercontent.com/Newbilius/ElbaMobileXamarinDeveloperTest/master/json/generated-02.json")
            };

            // Act
            var contacts = await contactDataSourceCollection.GetContactsAsync();

            // Assert
            Assert.NotEmpty(contacts);
        }


        [Fact]
        public async void GetContactsAsync_InvalidSources_ShouldThrowException()
        {
            // Arrange
            var contactDataSourceCollection = new ContactDataSourceCollection()
            {
                new HttpJsonContactsDataSource("https://raw.githubusercontent.com/Newbilius/ElbaMobileXamarinDeveloperTest/master/json/generated-01.json"),
                new HttpJsonContactsDataSource("123")
            };

            // Assert
            await Assert.ThrowsAnyAsync<Exception>(async () => await contactDataSourceCollection.GetContactsAsync());
        }
    }
}
