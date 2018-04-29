using Sitecore.SingleSignOn.BusinessLogic.Authentication;
using Sitecore.SingleSignOn.BusinessLogic.Caching;
using Sitecore.SingleSignOn.BusinessLogic.Registration;
using Sitecore.SingleSignOn.DataAccess.Repository;
using Sitecore.SingleSignOn.Models.Account;
using Sitecore.SingleSignOn.Utility.Constant;
using Sitecore.SingleSignOn.Utility.Enums;
using Sitecore.SingleSignOn.Utility.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace Sitecore.SingleSignOn.API.Controllers
{
    [RoutePrefix("api/Member")]
    public class MemberController : ApiController
    {
        [Route("Login")]
        [AcceptVerbs("POST")]
        public MemberModels Login([FromBody]string value)
        {
            try
            {
                ICredentialsHelper credentialsHelper = new CredentialsHelper();
                IMemberRepository memberRepository = new MemberRepository();

                LoginModels postModel = new JavaScriptSerializer().Deserialize<LoginModels>(value);

                MemberModels member = new MemberModels();
                if (CacheHelper.MemberCache.Contains(postModel.Username))
                {
                    member = (MemberModels)CacheHelper.MemberCache.Where(x => x.Key == postModel.Username).FirstOrDefault().Value;
                    member.LoginMethod = LoginMethodEnums.Cache.GetHashCode();
                }
                else
                {
                    member = memberRepository.GetByUsername(postModel.Username);
                    member.LoginMethod = LoginMethodEnums.Database.GetHashCode();
                }
                
                return credentialsHelper.AuthenticateMember(postModel, member);
            }
            catch(Exception ex)
            {
                LogHelper.WriteLog(StaticKeyHelper.API, StaticKeyHelper.Login, ex.Message);
                return null;
            }
        }

        [Route("Register")]
        [AcceptVerbs("POST")]
        public MemberModels Register([FromBody]string value)
        {
            try
            {
                IRegistrationHelper registrationHelper = new RegistrationHelper();

                RegisterModels postModel = new JavaScriptSerializer().Deserialize<RegisterModels>(value);

                return registrationHelper.RegisterNewMember(postModel);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(StaticKeyHelper.API, StaticKeyHelper.Register, ex.Message);
                return null;
            }
        }

        [Route("IsUsernameExist")]
        [AcceptVerbs("POST")]
        public MemberModels IsUsernameExist([FromBody]string value)
        {
            try
            {
                IMemberRepository memberRepository = new MemberRepository();

                RegisterModels postModel = new JavaScriptSerializer().Deserialize<RegisterModels>(value);

                return memberRepository.GetByUsername(postModel.Username);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(StaticKeyHelper.API, StaticKeyHelper.IsUsernameExist, ex.Message);
                return null;
            }
        }
    }
}
