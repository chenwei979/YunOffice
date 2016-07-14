using System.Web.Mvc;
using YunOffice.UserCenter.BusnissLogic;
using YunOffice.UserCenter.UI.Admin.RabbitMQ.AccountRegister;
using YunOffice.UserCenter.Entities;

namespace YunOffice.UserCenter.UI.Admin.Controllers
{
    public class AccountController : Controller
    {
        public UserBusnissLogic BusnissLogic { get; set; }
        public AccountRegisterMessagePublisher MessagePublisher { get; set; }
        public AccountRegisterMessageHandler MessageHandler { get; set; }

        public AccountController(UserBusnissLogic busnissLogic
            , AccountRegisterMessagePublisher messagePublisher
            , AccountRegisterMessageHandler messageHandler)
        {
            BusnissLogic = busnissLogic;
            MessagePublisher = messagePublisher;
            MessageHandler = messageHandler;
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
            MessagePublisher.Push(new UserEntity()
            {
                Account = username,
                Password = password,
                DisplayName = displayname
            });

            return Redirect("/Home/Index/");
        }
    }
}
