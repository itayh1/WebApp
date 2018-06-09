using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;
using WebApplication2;
using Newtonsoft.Json;

namespace WebApplication2
{
    public class FirstController : Controller
    {
       static MainPageModel mainPageModel = new MainPageModel();
       public static ConfigModel configModel = new ConfigModel();
       public static RemoveHandlerModel rmvHandlerModel = new RemoveHandlerModel();
       public static LogsModel logsModel = new LogsModel();

        // GET: First
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AjaxView()
        {
            return View(mainPageModel);
        }

        [HttpGet]
        public ActionResult Configurations()
        {
            return View(configModel);
        }

        //[HttpPost]
        public ActionResult RemoveHandler(string handler)
        {
            rmvHandlerModel.handler = handler;
            return View(rmvHandlerModel);
        }

        [HttpPost]
        public void handlerDeleted(string handler)
        {
            configModel.manualResetEvent.Reset();
            // update server handler was removed
            CommandRecievedEventArgs msg = new CommandRecievedEventArgs((int)CommandEnum.CloseCommand, new string[] { handler }, string.Empty);
            ClientConn.Instance.sendMessage(JsonConvert.SerializeObject(msg));
            // wait for response from server
            configModel.manualResetEvent.WaitOne();
        }

        [HttpGet]
        public ActionResult Logs()
        {
            return View(logsModel);
        }
        // POST: First/Edit/5
        [HttpPost]
        public ActionResult Logs(FormCollection frm)
        {
            string type = frm["filter"].ToString();
            List<LogObject> filtered = logsModel.filter(type);
            LogsModel tempModel = new LogsModel();
            tempModel.logs = filtered;
            return View(tempModel);
        }
        
    }
}
