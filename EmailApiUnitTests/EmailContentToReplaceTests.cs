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
                "lalala {{text}} fdasfdas",
                new Dictionary<string, string>()
                {
                    {"text", "text"}
                },
                "lalala text fdasfdas"
            },
            new object[]
            {
                "lalala {{test}} fdasfdas",
                new Dictionary<string, string>()
                {
                    {"text", "text"}
                },
                "lalala  fdasfdas"
            },
            new object[]
            {
                "lalala {{test}} fdasfdas",
                new Dictionary<string, string>()
                {
                    {"text", "text"},
                    {"test", "test"}
                },
                "lalala test fdasfdas"
            },
            new object[]
            {
                "lalala {{test}} fdasfdas {text} {{text}}",
                new Dictionary<string, string>()
                {
                    {"text", "text"},
                    {"test", "test"}
                },
                "lalala test fdasfdas {text} text"
            }
        };

        
        [Theory]
        [MemberData(nameof(TestData))]
        public async Task EmailContent_Replaced(string input, Dictionary<string, string> replacements, string expectedString)
        {
            //Arrange
            //Act
            var data = await MailKitService.ReplaceTemplateValueContent(input, replacements);
            //Assert
            Assert.Equal(data, expectedString);
        }
    }
}