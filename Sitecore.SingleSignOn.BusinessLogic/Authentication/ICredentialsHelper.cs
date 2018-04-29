using Sitecore.SingleSignOn.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.SingleSignOn.BusinessLogic.Authentication
{
    public interface ICredentialsHelper
    {
        MemberModels AuthenticateMember(LoginModels login, MemberModels member);
    }
}
