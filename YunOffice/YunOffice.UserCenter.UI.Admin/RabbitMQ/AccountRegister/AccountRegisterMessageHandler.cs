using YunOffice.UserCenter.BusnissLogic;
using YunOffice.UserCenter.Entities;

namespace YunOffice.UserCenter.UI.Admin.RabbitMQ.AccountRegister
{
    public class AccountRegisterMessageHandler : MessageHandler<UserEntity>
    {
        public UserBusnissLogic BusnissLogic { get; set; }

        public AccountRegisterMessageHandler(UserBusnissLogic busnissLogic)
        {
            BusnissLogic = busnissLogic;
        }

        public override void Hand(UserEntity message)
        {
            BusnissLogic.Save(message);
        }
    }
}
