using System.Collections.Generic;
using System.Threading.Tasks;
using EmailApi.Services;
using Xunit;

namespace EmailApiUnitTests
{
    public class EmailContentToReplaceTests
    {
        public static IEnumerable<object[]> TestData => new List<object[]>()
        {
            new object[]
            {
                "foo {{text}} bar",
                new Dictionary<string, string>()
                {
                    {"text", "text"}
                },
                "foo text bar"
            },
            new object[]
            {
                "foo {{test}} bar",
                new Dictionary<string, string>()
                {
                    {"text", "text"}
                },
                "foo  bar"
            },
            new object[]
            {
                "foo {{test}} bar",
                new Dictionary<string, string>()
                {
                    {"text", "text"},
                    {"test", "test"}
                },
                "foo test bar"
            },
            new object[]
            {
                "foo {{test}} bar {text} {{text}}",
                new Dictionary<string, string>()
                {
                    {"text", "text"},
                    {"test", "test"}
                },
                "foo test bar {text} text"
            }
        };

        
        [Theory]
        [MemberData(nameof(TestData))]
        public async Task EmailContent_Replaced(string input, Dictionary<string, string> replacements, string expectedString)
        {
            //Arrange
            //Act
            var data = MailKitService.ReplaceTemplateValueContent(input, replacements);
            //Assert
            Assert.Equal(data, expectedString);
        }
    }
}