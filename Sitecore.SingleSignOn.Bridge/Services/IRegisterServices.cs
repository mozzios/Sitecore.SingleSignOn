using Sitecore.SingleSignOn.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.SingleSignOn.Bridge.Services
{
    public interface IRegisterServices
    {
        bool IsUsernameExist(RegisterModels member);
        bool Register(RegisterModels register);
    }
}
