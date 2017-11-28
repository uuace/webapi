using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SwaggerWebApiSample.Filter
{
    /// <summary>
    /// HttpBasic验证连接器
    /// </summary>
    public class HttpBasicAuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {


        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            //if (actionContext.Request.Method == System.Net.Http.HttpMethod.Options)
            //{
            //    var challengeMessage = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.OK);
            //    //challengeMessage.Headers.Add("WWW-Authenticate", "Basic");
            //    throw new System.Web.Http.HttpResponseException(challengeMessage);
            //}

            if (actionContext.Request.Headers.Authorization != null)
            {
                string userInfo = Encoding.Default.GetString(Convert.FromBase64String(actionContext.Request.Headers.Authorization.Parameter));
                //用户验证逻辑
                bool _bool = false;
                _bool = string.Equals(userInfo, string.Format("{0}:{1}", "admin", "123456"));
    
                if (_bool)
                {
                    IsAuthorized(actionContext);
                }
                else
                {
                    HandleUnauthorizedRequest(actionContext);
                }
            }
            else
            {
                HandleUnauthorizedRequest(actionContext);
            }

            //HandleUnauthorizedRequest(actionContext);
        }

        protected override void HandleUnauthorizedRequest(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            var challengeMessage = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            challengeMessage.Headers.Add("WWW-Authenticate", "Basic");
            throw new System.Web.Http.HttpResponseException(challengeMessage);
        }
    }
}