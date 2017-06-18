using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using VideoRentBL;

namespace VideoRent
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //Mapper.Initialize(c => c.AddProfile<MappingProfile>());
            Mapper.Initialize(c =>
            {
                c.AddProfile<MappingProfileMVC>();
                c.AddProfile<MappingProfile>();

            });
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
