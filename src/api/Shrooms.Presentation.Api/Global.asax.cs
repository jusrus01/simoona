﻿//using System.Web.Mvc;

//namespace Shrooms.Presentation.Api
//{
//    // ReSharper disable once SA1649
//    public class WebApiApplication : System.Web.HttpApplication
//    {
//        protected void Application_Start()
//        {
//            MvcHandler.DisableMvcResponseHeader = true;
//        }

//        protected void Application_PreSendRequestHeaders()
//        {
//            Response.Headers.Remove("Server");
//            Response.Headers.Remove("X-AspNet-Version");
//            Response.Headers.Remove("X-AspNetMvc-Version");
//        }
//    }
//}