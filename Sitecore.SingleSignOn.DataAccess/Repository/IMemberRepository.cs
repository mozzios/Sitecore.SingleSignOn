using Sitecore.SingleSignOn.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.SingleSignOn.DataAccess.Repository
{
    public interface IMemberRepository
    {
        bool Insert(MemberModels member);
        MemberModels GetByUsername(string username);
    }
}
