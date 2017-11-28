using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Http;
namespace SwaggerWebApiSample.Filter
{
    /// <summary>
    /// token验证
    /// </summary>
    public class AuthFilterOutside:AuthorizeAttribute
    {
        /// <summary>
        /// 重写基类的验证方式，加入我们自定义的Ticket验证
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            Debug.WriteLine("11");
            //url获取token  
            var content = actionContext.Request.Properties["MS_OwinContext"] as HttpContextBase;
            //var token2 = actionContext.Request.Headers.Expect[""];
            var token = content.Request.Headers["Token"];
            if (!string.IsNullOrEmpty(token))
            {
                Console.WriteLine(token);
                //解密用户ticket,并校验用户名密码是否匹配  
                //if (ValidateTicket(token))
                //{
                //    base.IsAuthorized(actionContext);
                //}
                //else
                //{
                //    HandleUnauthorizedRequest(actionContext);
                //}
            }
            //如果取不到身份验证信息，并且不允许匿名访问，则返回未验证401  
            else
            {
                Console.WriteLine("没有");
                var attributes = actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().OfType<AllowAnonymousAttribute>();
                bool isAnonymous = attributes.Any(a => a is AllowAnonymousAttribute);
                if (isAnonymous) base.OnAuthorization(actionContext);
                else HandleUnauthorizedRequest(actionContext);
            }
        }

        //校验票据（数据库数据匹配）  
        private bool ValidateTicket(string encryptToken)
        {
            bool flag = false;
            try
            {
                //获取数据库Token  
                //Dec.Models.TicketAuth model = Dec.BLL.TicketAuth.GetTicketAuthByToken(encryptToken);
                //if (model.Token == encryptToken) //存在  
                //{
                //    //未超时  
                //    flag = (DateTime.Now <= model.ExpireDate) ? true : false;
                //}
            }
            catch (Exception ex)
            {

            }
            return flag;
        }
    }
}