using Sitecore.SingleSignOn.DataAccess.Repository;
using Sitecore.SingleSignOn.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.SingleSignOn.BusinessLogic.Registration
{
    public class RegistrationHelper : IRegistrationHelper
    {
        public MemberModels RegisterNewMember(RegisterModels register)
        {
            IMemberRepository memberRepository = new MemberRepository();

            MemberModels member = new MemberModels()
            {
                Fullname = register.Fullname,
                Username = register.Username,
                Password = register.Password
            };

            if (memberRepository.Insert(member))
                return memberRepository.GetByUsername(member.Username);

            return null;
        }
    }
}
