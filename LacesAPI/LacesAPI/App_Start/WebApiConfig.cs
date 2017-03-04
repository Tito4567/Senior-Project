﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LacesAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
           config.Routes.MapHttpRoute(name: "UserApi", routeTemplate: "{controller}/{action}/{id}", defaults: new { id = RouteParameter.Optional });
        }
    }
}
