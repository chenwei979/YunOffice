using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using YunOffice.UserCenter.BusnissLogic;

namespace YunOffice.UserCenter.UI.Admin.Controllers
{
    public class AccountController : Controller
    {
        public UserBusnissLogic BusnissLogic { get; set; }

        public AccountController(UserBusnissLogic busnissLogic)
        {
            BusnissLogic = busnissLogic;
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var logined = BusnissLogic.Login(username, password);

            if (logined) return Redirect("/Home/Index/");
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(string displayname, string username, string password)
        {
            var success = BusnissLogic.Register(displayname, username, password);

            if (success) return Redirect("/Home/Index/");
            return View();
        }
    }
}
