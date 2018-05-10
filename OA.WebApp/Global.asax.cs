using OA.WebApp.Models;
using Spring.Web.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using log4net.Config;
using Common.Logging;

namespace OA.WebApp
{
    public class MvcApplication : SpringMvcApplication
    {
        protected void Application_Start()
        {
            XmlConfigurator.Configure();

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            string logPath = Server.MapPath("/Logs/");
            ThreadPool.QueueUserWorkItem((a) =>
            {
                while(true)
                {
                    if (MyExceptionAttribute.ExceptionQueue.Count > 0)
                    {
                        var ex = MyExceptionAttribute.ExceptionQueue.Dequeue();
                        if (ex != null)
                        {
                            //string fileName = DateTime.Now.ToString("yyyy-MM-dd");
                            //File.AppendAllText(logPath + fileName + ".txt", ex.ToString(), System.Text.Encoding.UTF8);
                            ILog logger = LogManager.GetLogger("errorMsg");
                            logger.Error(ex.ToString());
                        }
                        else
                        {
                            Thread.Sleep(3000);
                        }
                    }
                    else
                    {
                        Thread.Sleep(3000);
                    }
                }
            }, logPath);
        }
    }
}
