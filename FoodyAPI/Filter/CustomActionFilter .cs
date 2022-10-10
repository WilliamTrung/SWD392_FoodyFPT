using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Service.Services.IService;
using FoodyAPI.Helper;

namespace FoodyAPI.Filter
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class CustomActionFilter : ActionFilterAttribute, IActionFilter
    {
        IUserService _userService;
        private void SetService(FilterContext context)
        {
            var services = context.HttpContext.RequestServices;
            _userService = services.GetRequiredService<IUserService>();
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            SetService(context);
            var session = context.HttpContext.Session;
            var login_user = SessionExtension.Get<Service.DTO.User>(session, "login-user");

            //not yet login
            //context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context) {
            base.OnActionExecuted(context);
        }
    }
}
