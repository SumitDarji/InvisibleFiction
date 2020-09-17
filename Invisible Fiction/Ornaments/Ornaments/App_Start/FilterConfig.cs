using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Ornaments
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        //CUSTOM ATTRIBUTE FOR AUTHORIZE FILTER
        //CHECKED REPLICANT USER IS LOGGED IN OR NOT
        public sealed class CheckedLoggedIn : AuthorizeAttribute
        {
            protected override bool AuthorizeCore(HttpContextBase httpContext)
            {
                bool authorize = true;
                if (httpContext.Session["UserID"] == null)
                {
                    authorize = false; /* return true if Entity has current user(active)*/
                }
                return authorize;
            }

            //IF ADMIN USER IS NOT AUTHORIZED OR NOT LOGGED IN
            //THEN HANDLE UN-AUTHORIZED REQUEST FOR AUTHORIZED FILTER ATTRIBUTE HERE - REDIRECT TO ROUTE RESULT (I.E. REDIRECT TO ACTION)
            protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
            {
                //filterContext.Result = new HttpUnauthorizedResult();
                filterContext.Result = new RedirectToRouteResult(
                                       new RouteValueDictionary
                                       {
                                       { "action", "Login" },
                                       { "controller", "Account" }
                                       });
            }

            //CUSTOM ATTRIBUTES FOR ALLOW ANONYMOUS - NOT REQUIRED NOW
            public override void OnAuthorization(AuthorizationContext filterContext)
            {
                bool skipAuthorization = filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)
                || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true);
                if (!skipAuthorization)
                {
                    base.OnAuthorization(filterContext);
                }
            }
        }
    }
}
