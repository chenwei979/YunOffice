using System;

namespace YunOffice.Common.DataAccess
{
    public interface IEntity
    {
        long Id { get; set; }
        long ApplicationId { get; set; }

        DateTime? CreateDate { get; set; }
        long? CreatorId { get; set; }
        string CreatorName { get; set; }

        DateTime? UpdateDate { get; set; }
        long? UpdaterId { get; set; }
        string UpdaterName { get; set; }
    }

    public interface IMultiLanguageEntity : IEntity
    {
        int? Language { get; set; }
    }
}
