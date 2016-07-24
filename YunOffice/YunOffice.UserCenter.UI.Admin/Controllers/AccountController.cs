using System.Web.Mvc;
using YunOffice.Common.Redis;
using YunOffice.UserCenter.BusnissLogic;
using YunOffice.UserCenter.UI.Admin.Models;
using YunOffice.UserCenter.UI.Admin.RabbitMQ.AccountRegister;

namespace YunOffice.UserCenter.UI.Admin.Controllers
{
    public class AccountController : Controller
    {
        public UserBusnissLogic BusnissLogic { get; set; }
        public AccountRegisterMessagePublisher MessagePublisher { get; set; }
        public RedisDataAccess<AccountRegisterViewModel> RedisDataAccess { get; set; }

        public AccountController(UserBusnissLogic busnissLogic
            , AccountRegisterMessagePublisher messagePublisher
            , RedisDataAccess<AccountRegisterViewModel> redisDataAccess)
        {
            BusnissLogic = busnissLogic;
            MessagePublisher = messagePublisher;
            RedisDataAccess = redisDataAccess;
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string account, string password)
        {
            var loginEntity = RedisDataAccess.Get(account);

            var logined = false;
            if (loginEntity != null)
            {
                logined = loginEntity.Password == password;
            }
            else
            {
                logined = BusnissLogic.Login(account, password);
            }

            if (logined) return Redirect("/Home/Index/");
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(string displayname, string account, string password)
        {
            MessagePublisher.Push(new AccountRegisterViewModel()
            {
                Account = account,
                Password = password,
                DisplayName = displayname
            });

            return Redirect("/Home/Index/");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                BusnissLogic.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
