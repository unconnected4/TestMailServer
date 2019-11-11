using NUnit.Framework;
using TestMailServer.Core.Common;

namespace TestMailServer.UnitTests.Common
{
    /// <summary>
    /// Test cases for the EmailAddress class.  These tests
    /// also covers the InvalidEmailAddressException class.
    /// </summary>
    /// <remarks>
    /// Created By: Eric Daugherty
    /// </remarks>
    [TestFixture]	
	public class EmailAddressTest
	{
		[Test]
        [TestCase(null, "mydomain.com")]
        [TestCase("", "mydomain.com")]
        [TestCase("m@e", "mydomain.com")]
        [TestCase("m<e", "mydomain.com")]
        [TestCase("m:e", "mydomain.com")]
        [TestCase("user", null)]
        [TestCase("user", "")]
        [TestCase("user", "mydom]in.com")]
        [TestCase("user", "mydom;in.com")]
        [TestCase("user", "mydom\"in.com")]
        public void EmailAddress_Throws_ByUserDomain(string username, string domain)
		{
			Assert.Throws<InvalidEmailAddressException>(()=> { new EmailAddress(username, domain); });
		}

        [Test]
        [TestCase("@mydomain.com")]
        [TestCase("user@")]
        [TestCase("usermydomain.com")]
        [TestCase("us@er@mydomain.com")]
        [TestCase("user@mydo:main.com")]
        public void EmailAddress_Throws_ByEmail(string email)
        {
            Assert.Throws<InvalidEmailAddressException>(() => { new EmailAddress(email); });
        }

        [Test]
        [TestCase("us er@mydomain.com", "us er", "mydomain.com")]
        [TestCase("eric@ericdomain.com", "eric", "ericdomain.com")]
        [TestCase("my_name@my_domain.com", "my_name", "my_domain.com")]
        [TestCase("my_name100@mydomain.com", "my_name100", "mydomain.com")]
        public void EmailAddress_Parses_ValidUserAndDomain(string email, string username, string domain)
		{
            EmailAddress address = new EmailAddress(username, domain);
            Assert.AreEqual(username, address.Username, "Username test");
            Assert.AreEqual(domain, address.Domain, "Domain test");
            Assert.AreEqual(email, address.Address, "Address test");
            Assert.AreEqual(email, address.ToString(), "ToString test");
		}
		
		
        [Test]
        [TestCase("user@mydomain.com", "user", "mydomain.com")]
        [TestCase("eric@ericdomain.com", "eric", "ericdomain.com")]
        public void EmailAddress_ParsesValidEmail(string email, string username, string domain)
        {
            EmailAddress address = new EmailAddress(email);
            Assert.AreEqual(username, address.Username, "Username test");
            Assert.AreEqual(domain, address.Domain, "Domain test");
            Assert.AreEqual(email, address.Address, "Address test");
            Assert.AreEqual(email, address.ToString(), "ToString test");
        }
	}
}
