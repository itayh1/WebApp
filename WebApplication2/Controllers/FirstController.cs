using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2;
using Newtonsoft.Json;

namespace WebApplication2
{
    public class FirstController : Controller
    {
       static MainPageModel mainPage= new MainPageModel();
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
            return View(mainPage);
        }

        [HttpGet]
        public ActionResult Configurations()
        {
            return View(configModel);
        }

        [HttpPost]
        public ActionResult engageHandler(string handler)
        {
            rmvHandlerModel.handler = handler;
            return View(rmvHandlerModel);
        }

        [HttpPost]
        public void handlerRemoved(string handler)
        {
            string[] args = { handler };
            CommandRecievedEventArgs msg = new CommandRecievedEventArgs((int)CommandEnum.CloseCommand, args, string.Empty);
            ClientConn.Instance.sendMessage(JsonConvert.SerializeObject(msg));
        }

        /// POST: First/Create
        [HttpPost]
        public ActionResult Create(Student st)
        {
            //try
            //{
            //    employees.Add(emp);

            //    return RedirectToAction("Details");
            //}
            //catch
            //{
            return View();
            //}
        }

        // GET: First/Edit/5
        public ActionResult Edit(int id)
        {
            //foreach (Employee emp in employees) {
            //    if (emp.ID.Equals(id)) { 
            //        return View(emp);
            //    }
            //}
            return View("Error");
        }

        // POST: First/Edit/5
        [HttpPost]
        public ActionResult Logs(FormCollection frm)
        {
            string type = frm["filter"].ToString();
            List<LogObject> filtered = logsModel.filter(type);
            return View(filtered);
        }
        
    }
}
