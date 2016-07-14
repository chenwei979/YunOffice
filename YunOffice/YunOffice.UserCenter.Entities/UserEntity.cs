using Dapper.Contrib.Extensions;
using System;
using YunOffice.Common.DataAccess;

namespace YunOffice.UserCenter.Entities
{
    [Table("[dbo].[User]")]
    public class UserEntity : IEntity
    {
        [Key]
        public long Id { get; set; }
        public long ApplicationId { get; set; }
        public DateTime? CreateDate { get; set; }
        public long? CreatorId { get; set; }
        public string CreatorName { get; set; }
        public DateTime? UpdateDate { get; set; }
        public long? UpdaterId { get; set; }
        public string UpdaterName { get; set; }

        public string Account { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }

        public long? DepartmentId { get; set; }
        public string DepartmentName { get; set; }

        public long? RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
