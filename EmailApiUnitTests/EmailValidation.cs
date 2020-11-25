using Xunit;

namespace EmailApiUnitTests
{
    public class EmailValidation
    {
        [Theory]
        [InlineData("abc@gmail.com", true)]
        [InlineData("XD", false)]
        [InlineData("test_test@a.gh", true)]
        public void Check_Email_Valid(string input, bool expexted)
        {
        }
        
    }
}