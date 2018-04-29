using Sitecore.SingleSignOn.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.SingleSignOn.BusinessLogic.Registration
{
    public interface IRegistrationHelper
    {
        MemberModels RegisterNewMember(RegisterModels register);
    }
}
