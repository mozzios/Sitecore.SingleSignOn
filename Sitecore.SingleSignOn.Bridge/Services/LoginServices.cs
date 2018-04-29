using System.Net.Http;
using System.Web.Script.Serialization;
using Sitecore.SingleSignOn.Bridge.API;
using Sitecore.SingleSignOn.Bridge.Constant;
using Sitecore.SingleSignOn.Models.Account;

namespace Sitecore.SingleSignOn.Bridge.Services
{
    public class LoginServices : ILoginServices
    {
        public MemberModels Authenticate(LoginModels member)
        {
            string data = new JavaScriptSerializer().Serialize(member);

            HttpResponseMessage response = APIHelper.Post(GlobalKeyHelper.MemberLoginUrl, data);
            MemberModels result = response.Content.ReadAsAsync<MemberModels>().Result;

            return result;
        }
    }
}
