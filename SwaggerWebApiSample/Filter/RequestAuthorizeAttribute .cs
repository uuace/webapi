using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Security;

namespace SwaggerWebApiSample.Filter
{
    /// <summary>
    /// Basic 方式验证
    /// </summary>
    public class BasicAuthenticationAttribute : AuthorizeAttribute
    {
        //重写基类的验证方式，加入我们自定义的Ticket验证
        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            //从http请求的头里面获取身份验证信息，验证是否是请求发起方的ticket
            var authorization = actionContext.Request.Headers.Authorization;
            if ((authorization != null) && (authorization.Parameter != null))
            {
                //解密用户ticket,并校验用户名密码是否匹配
                var encryptTicket = authorization.Parameter;
                if (ValidateTicket(encryptTicket))
                {
                    base.IsAuthorized(actionContext);
                }
                else
                {
                    HandleUnauthorizedRequest(actionContext);
                }
            }
            //如果取不到身份验证信息，并且不允许匿名访问，则返回未验证401
            else
            {
                HandleUnauthorizedRequest(actionContext);
            }
        }

        //校验用户名密码（正式环境中应该是数据库校验）
        private bool ValidateTicket(string encryptTicket)
        {
            //解密Ticket
            var ticket= FormsAuthentication.Decrypt(encryptTicket);
            var strTicket = FormsAuthentication.Decrypt(encryptTicket).UserData;
            var timeTicket = FormsAuthentication.Decrypt(encryptTicket).Expired;
            //是否过期
            if (timeTicket)
            {
                return false;
            }
            //从Ticket里面获取用户名和密码
            var index = strTicket.IndexOf("&");
            string strUser = strTicket.Substring(0, index);
            string strPwd = strTicket.Substring(index + 1);

            if (strUser == "admin" && strPwd == "123456")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        protected override void HandleUnauthorizedRequest(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            var challengeMessage = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            challengeMessage.Headers.Add("WWW-Authenticate", "Basic");
            //FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(0, strUser, DateTime.Now,
            //               DateTime.Now.AddHours(1), true, string.Format("{0}&{1}", strUser, strPwd),
            //               FormsAuthentication.FormsCookiePath);
            ////返回登录结果、用户信息、用户验证票据信息
            //var oUser = new { bRes = true, UserName = strUser, Password = strPwd, Ticket = FormsAuthentication.Encrypt(ticket) };
            //challengeMessage.Headers.Add("Authorization", "Basic");
            var re= new System.Web.Http.HttpResponseException(challengeMessage);
            throw re;
        }
    }
}