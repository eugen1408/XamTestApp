using System;
using XamTestAppDataLibrary.Services;
using Xunit;

namespace XamTestAppDataLibrary.Tests
{
    public class HttpJsonContactsDataSourceTests
    {
        [Fact]
        public async void GetContactsAsync_ValidSource_ShouldReturnContacts()
        {
            // Arrange
            var httpJsonContactsDataSource = new HttpJsonContactsDataSource("https://raw.githubusercontent.com/Newbilius/ElbaMobileXamarinDeveloperTest/master/json/generated-01.json");

            // Act
            var actual = await httpJsonContactsDataSource.GetContactsAsync();

            // Assert
            Assert.NotEmpty(actual);
        }


        [Fact]
        public async void GetContactsAsync_ValidSource_ContactShouldContainData()
        {
            // Arrange
            var httpJsonContactsDataSource = new HttpJsonContactsDataSource("https://raw.githubusercontent.com/Newbilius/ElbaMobileXamarinDeveloperTest/master/json/generated-01.json");

            // Act
            var actual = await httpJsonContactsDataSource.GetContactsAsync();

            // Assert
            foreach (var contact in actual)
            {
                Assert.NotNull(contact.Id);
                Assert.NotNull(contact.Biography);
                Assert.NotNull(contact.EducationPeriod);
                Assert.NotNull(contact.Name);
                Assert.NotEqual(0, contact.Height);
            }
        }

        [Fact]
        public async void GetContactsAsync_InvalidSource_ShouldThrowException()
        {
            // Arrange
            var httpJsonContactsDataSource = new HttpJsonContactsDataSource("https://raw.githubusercontent.com/Newbilius/ElbaMobileXamarinDeveloperTest/master/json/generated-111111.json");

            // Assert
            await Assert.ThrowsAnyAsync<Exception>(async () => await httpJsonContactsDataSource.GetContactsAsync());
        }
    }
}
