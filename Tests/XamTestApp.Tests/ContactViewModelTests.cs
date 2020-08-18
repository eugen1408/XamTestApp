using System;
using System.Collections.Generic;
using System.Text;
using XamTestApp.ViewModels;
using XamTestAppDataLibrary.Models;
using Xunit;

namespace XamTestApp.Tests
{
    public class ContactViewModelTests
    {
        [Theory]
        [InlineData("7 922 222 10 82", "79222221082")]
        [InlineData("7 (922) 222      10-82", "79222221082")]
        [InlineData("+7 922 222-10-82", "79222221082")]
        [InlineData("rhgrewh 7 922 222 10 82 afedafa", "79222221082")]
        public void PhoneDigits_PhoneStrings_ShouldFormatPhone(string phone, string expected)
        {
            // Arrange
            var contact = new Contact()
            {
                Phone = phone
            };
            var contactViewModel = new ContactViewModel(contact);

            // Act
            var actual = contactViewModel.PhoneDigits;

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
