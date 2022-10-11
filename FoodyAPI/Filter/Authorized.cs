using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Service.Services.IService;
using FoodyAPI.Helper;

namespace FoodyAPI.Filter
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class Authorized : ActionFilterAttribute, IActionFilter
    {
        IUserService? _userService;
        string? _roles = null;
        public Authorized()
        {

        }
        public Authorized(string role)
        {
            _roles = role;
        }
        private void SetService(FilterContext context)
        {
            var services = context.HttpContext.RequestServices;
            _userService = services.GetRequiredService<IUserService>();
        }
        private bool Authorizing(Service.DTO.User login_user, string[] roles)
        {
            bool isAuthorized = false;
            if(login_user == null && roles.Length == 0)
            {
                //allow anonymous
                isAuthorized = true;
            } else if(login_user != null && roles.Length > 0)
            {
                //check login user with passed roles
                if(login_user.Role != null)
                {
                    if (roles.Any(r => r == login_user.Role.Name))
                    {
                        //is valid
                        isAuthorized = true;
                    }
                }
                
            } 
            return isAuthorized;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                bool flag = false;
                SetService(context);
                var session = context.HttpContext.Session;
                var login_user = SessionExtension.Get<Service.DTO.User>(session, "login-user");
                var rd = context.HttpContext.GetRouteData().Values;
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                var action = rd["action"].ToString();
                var controller = rd["controller"].ToString();
#pragma warning disable CS8604 // Possible null reference argument.
                var roles = _roles.Split(",");
                flag = Authorizing(login_user, roles);
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8602 // Dereference of a possibly null reference.


                if (!flag)
                {
                    context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
                }
            } catch
            {
                context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
            }
            
            //not yet login
            //context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context) {
            base.OnActionExecuted(context);
        }
    }
}
