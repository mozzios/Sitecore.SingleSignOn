using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sitecore.SingleSignOn.BusinessLogic.Authentication;
using Sitecore.SingleSignOn.Models.Account;

namespace Sitecore.SingleSignOn.API.UnitTest.Credentials
{
    [TestClass]
    public class CredentialTest
    {
        [TestMethod]
        public void CredentialsHelper_MatchingUsernamePassword_Succeeds()
        {
            ICredentialsHelper credentialsHelper = new CredentialsHelper();

            LoginModels login = new LoginModels()
            {
                Username = "testing",
                Password = "l+Smu5YMqvEypLypoWPDtw=="
            };

            MemberModels member = new MemberModels()
            {
                Username = "testing",
                Password = "VnEbcTPKNYRzXyl5P4xQIQ=="
            };

            Assert.IsNotNull(credentialsHelper.AuthenticateMember(login, member));
        }

        [TestMethod]
        public void CredentialsHelper_MatchingUsernameWrongPassword_Fails()
        {
            ICredentialsHelper credentialsHelper = new CredentialsHelper();

            LoginModels login = new LoginModels()
            {
                Username = "testing",
                Password = "l+Smu5YMqvEypLypoWPDtw=="
            };

            MemberModels member = new MemberModels()
            {
                Username = "testing",
                Password = "bc5SatyogkbDCYFcqWjtaR8bks8WnHAArPhyd658Ux0="
            };

            Assert.IsNull(credentialsHelper.AuthenticateMember(login, member));
        }

        [TestMethod]
        public void CredentialsHelper_WrongUsernameMatchingPassword_Fails()
        {
            ICredentialsHelper credentialsHelper = new CredentialsHelper();

            LoginModels login = new LoginModels()
            {
                Username = "flamingo",
                Password = "l+Smu5YMqvEypLypoWPDtw=="
            };

            MemberModels member = new MemberModels()
            {
                Username = "testing",
                Password = "VnEbcTPKNYRzXyl5P4xQIQ=="
            };

            Assert.IsNull(credentialsHelper.AuthenticateMember(login, member));
        }
    }
}
