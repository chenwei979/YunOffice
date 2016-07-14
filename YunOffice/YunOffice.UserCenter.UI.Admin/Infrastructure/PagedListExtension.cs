using YunOffice.UserCenter.UI.Admin.Infrastructure;
using PagedList;
using System.Web.Mvc;

namespace YunOffice.UserCenter.UI.Admin
{
    public static class PagedListExtension
    {
        public static ActionResult GetJsonResult(this IPagedList<object> pagedItems)
        {
            return new JsonNetResult()
            {
                Data = new
                {
                    items = pagedItems,
                    pagedInfo = pagedItems.GetMetaData()
                },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}