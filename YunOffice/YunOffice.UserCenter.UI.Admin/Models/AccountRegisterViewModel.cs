namespace YunOffice.UserCenter.UI.Admin.Models
{
    [ProtoBuf.ProtoContract]
    public class AccountRegisterViewModel
    {
        [ProtoBuf.ProtoMember(1)]
        public string Account { get; set; }

        [ProtoBuf.ProtoMember(2)]
        public string Password { get; set; }

        [ProtoBuf.ProtoMember(3)]
        public string DisplayName { get; set; }
    }
}