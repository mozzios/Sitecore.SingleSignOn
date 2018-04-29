using System;
using System.Runtime.Caching;
using Sitecore.SingleSignOn.BusinessLogic.Caching;
using Sitecore.SingleSignOn.Models.Account;
using Sitecore.SingleSignOn.Utility.Enums;
using Sitecore.SingleSignOn.Utility.Security;

namespace Sitecore.SingleSignOn.BusinessLogic.Authentication
{
    public class CredentialsHelper : ICredentialsHelper
    {
        IEncryptionHelper encryptionHelper = new EncryptionHelper();

        public MemberModels AuthenticateMember(LoginModels login, MemberModels member)
        {
            if (login.Username == member.Username &&
                encryptionHelper.DecryptString(login.Password, EncryptionTypeEnums.Member) == encryptionHelper.DecryptString(member.Password, EncryptionTypeEnums.Member))
            {
                if (!CacheHelper.MemberCache.Contains(login.Username))
                {
                    CacheHelper.MemberCache.Add(member.Username, member, new CacheItemPolicy(){ AbsoluteExpiration = DateTime.UtcNow.AddMinutes(30) });
                }

                return member;
            }
            else
                return null;
        }
    }
}
