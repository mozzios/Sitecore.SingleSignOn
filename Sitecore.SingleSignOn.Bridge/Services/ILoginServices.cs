using Sitecore.SingleSignOn.Models.Account;

namespace Sitecore.SingleSignOn.Bridge.Services
{
    public interface ILoginServices
    {
        MemberModels Authenticate(LoginModels member);
    }
}
