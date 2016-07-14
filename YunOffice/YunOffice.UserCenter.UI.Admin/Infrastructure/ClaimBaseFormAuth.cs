using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Collections.Generic;
using System.Security.Claims;
using System.Web;

namespace YunOffice.UserCenter.UI.Admin.Infrastructure
{
    public class ClaimBaseFormAuth
    {
        public static ClaimBaseFormAuth Singleton { get; private set; }

        static ClaimBaseFormAuth()
        {
            Singleton = new ClaimBaseFormAuth();
        }

        protected IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.Current.GetOwinContext().Authentication; }
        }

        public void SignIn(string userName, string role)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, userName));
            claims.Add(new Claim(ClaimTypes.Role, role));

            var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie, ClaimTypes.Name, ClaimTypes.Role);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = false }, identity);
        }

        public void SingOut()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }
    }
}