using Sitecore.SingleSignOn.Utility.Enums;
using System;
using System.Security.Principal;

namespace Sitecore.SingleSignOn.WebApp.Models
{
    public class MemberPrincipal : IPrincipal
    {
        public Guid MemberId { get; set; }
        public string Fullname { get; set; }
        public string Username { get; set; }


        public IIdentity Identity
        {
            get; private set;
        }

        public MemberPrincipal(string username, int loginMethod)
        {
            Identity = new GenericIdentity(username, GetAuthenticationType(loginMethod));
        }

        public bool IsInRole(string role)
        {
            throw new NotImplementedException();
        }

        public string GetAuthenticationType(int value)
        {
            switch ((LoginMethodEnums)value)
            {
                case LoginMethodEnums.Cache:
                    return "Cache";
                case LoginMethodEnums.Database:
                    return "Database";
                default:
                    return "Unknown";
            }
        }
    }
}