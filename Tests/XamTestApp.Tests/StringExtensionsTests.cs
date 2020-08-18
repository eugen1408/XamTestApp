using System;
using System.Collections.Generic;
using System.Text;
using XamTestApp.Helpers;
using Xunit;

namespace XamTestApp.Tests
{
    public class StringExtensionsTests
    {
        [Theory]
        [InlineData("tEsT", "fjiefjeifTESTewgjg")]
        [InlineData("test", "пкпкп", "eftestelab")]
        [InlineData("test test2", "пкпеуыекп", "eftest2lab")]
        [InlineData("test aa5", "пкпеуыекп", "wgTESTjr", null, "wjkggaa5")]
        public void FoundInAny_SimpleStrings_ShouldSearchSubstringsCaseInsensitive(string searchString, params string[] arrayToSearch)
        {
            // Arrange


            // Act

            var actual = searchString.FoundInAny(arrayToSearch);

            // Assert

            Assert.True(actual);
        }

        [Theory]
        [InlineData("7 (922 222 1082", "79222221082")]
        [InlineData("922222", "пкпкп", "79222221082")]
        [InlineData("test 792-2-2-2-2-(1)0 8 2", "TEST", "79222221082")]
        [InlineData("7(922)222-1082", "test", "", "   ", null, "79222221082")]
        public void FoundInAny_PhoneDigitStrings_ShouldSearchSubstringsAnyFormat(string searchString, params string[] arrayToSearch)
        {
            // Arrange


            // Act

            var actual = searchString.FoundInAny(arrayToSearch);

            // Assert

            Assert.True(actual);
        }
    }
}
