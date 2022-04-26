using Files.Controllers;
using Files.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Files.Filter
{
    public class VerificationSession : ActionFilterAttribute
    {

        //private User _user;
        private AppDbContext _dbContext;

        public VerificationSession(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                base.OnActionExecuting(context);
                //int? userId;

                //userId = context.HttpContext.Session.GetInt32("userId");
                if (context.HttpContext.Session.GetInt32("userId") is null)
                {
                    if (context.Controller is LoginController == false)
                    {
                        context.HttpContext.Response.Redirect("/Login/Index");
                    }
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}
