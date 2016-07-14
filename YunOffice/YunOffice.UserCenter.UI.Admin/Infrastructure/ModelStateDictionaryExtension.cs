using YunOffice.UserCenter.UI.Admin.Infrastructure;
using System.Linq;

namespace System.Web.Mvc
{
    public static class ModelStateDictionaryExtension
    {
        public static ActionResult GetErrorResult(this ModelStateDictionary ModelState)
        {
            var errorMessage = ModelState.Values.SelectMany(item => item.Errors.Select(error => error.ErrorMessage));
            return new JsonNetResult
            {
                Data = new
                {
                    Success = false,
                    ErrorMessages = string.Join("<br/>", errorMessage)
                }
            };
        }
    }
}