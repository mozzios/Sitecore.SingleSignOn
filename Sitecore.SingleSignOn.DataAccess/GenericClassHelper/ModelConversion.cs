using Sitecore.SingleSignOn.DataAccess.DatabaseModels;
using Sitecore.SingleSignOn.Models.Account;

namespace Sitecore.SingleSignOn.DataAccess.GenericClassHelper
{
    public static class ModelConversion
    {
        public static MemberModels GetMemberModels(Member member)
        {
            return new MemberModels()
            {
                MemberId = member.Id,
                Fullname = member.Fullname,
                Username = member.Username,
                Password = member.Password,
                CreatedAt = member.CreatedAt,
                UpdatedAt = member.UpdatedAt
            };
        }
    }
}
