using System.Net.Http;
using System.Web.Script.Serialization;
using Sitecore.SingleSignOn.Bridge.API;
using Sitecore.SingleSignOn.Bridge.Constant;
using Sitecore.SingleSignOn.Models.Account;

namespace Sitecore.SingleSignOn.Bridge.Services
{
    public class RegisterServices : IRegisterServices
    {
        public bool IsUsernameExist(RegisterModels member)
        {
            string data = new JavaScriptSerializer().Serialize(member);

            HttpResponseMessage response = APIHelper.Post(GlobalKeyHelper.MemberIsUsernameExistUrl, data);
            MemberModels result = response.Content.ReadAsAsync<MemberModels>().Result;

            return result != null;
        }

        public bool Register(RegisterModels register)
        {
            string data = new JavaScriptSerializer().Serialize(register);

            HttpResponseMessage response = APIHelper.Post(GlobalKeyHelper.MemberRegisterUrl, data);
            MemberModels result = response.Content.ReadAsAsync<MemberModels>().Result;

            return result != null;
        }
    }
}
