using System.Transactions;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Raven.Client;
using StructureMap;

namespace Arbeidsbok
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new TransactionFilter());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            BundleTable.Bundles.RegisterTemplateBundles();

            var container = IoC.Initialize();
            DependencyResolver.SetResolver(new SmDependencyResolver(container));
        }
    }

    public class TransactionFilter : IActionFilter
    {
        private TransactionScope _transaction;

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _transaction = new TransactionScope();
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            ObjectFactory.GetInstance<IDocumentSession>().SaveChanges();
            _transaction.Complete();
            _transaction.Dispose();
        }
    }
}