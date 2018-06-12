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
       public static PhotosModel photosModel = new PhotosModel();

        [HttpGet]
        public ActionResult AjaxView() {
            return View(mainPageModel);
        }

        [HttpGet]
        public ActionResult Configurations() {
            return View(configModel);
        }


        public ActionResult RemoveHandler(string handler) {
            rmvHandlerModel.handler = handler;
            return View(rmvHandlerModel);
        }
        

        public ActionResult HandlerDeleted(string handler) {
            configModel.manualResetEvent.Reset();
            // update server handler was removed
            CommandRecievedEventArgs msg = new CommandRecievedEventArgs((int)CommandEnum.CloseCommand, new string[] { handler }, string.Empty);
            ClientConn.Instance.sendMessage(JsonConvert.SerializeObject(msg));
            // wait for response from server
            configModel.manualResetEvent.WaitOne();
            return RedirectToActionPermanent("Configurations");
        }

        [HttpGet]
        public ActionResult Logs() {
            return View(logsModel);
        }


        // POST: First/Edit/5
        [HttpPost]
        public ActionResult Logs(FormCollection frm) {
            string type = frm["filter"].ToString();
            List<LogObject> filtered = logsModel.filter(type);
            LogsModel tempModel = new LogsModel();
            tempModel.logs = filtered;
            return View(tempModel);
        }

        public ActionResult Photos() {
            photosModel.Thumbnails.Clear();       
            //photosModel.OutputDir = configModel.outputDir;
            photosModel.SetPhotos();
            return View(photosModel);
        }

        public ActionResult ThumbnailView(string path) {
            Thumbnail thumbnail = new Thumbnail(path);
            return View(thumbnail);
        }

    
        public ActionResult ThumbnailDelete(string path) {
            Thumbnail thumbnail = new Thumbnail(path);
            return View(thumbnail);
        }

        
        public ActionResult DeleteAfterViewed(string path) {
            photosModel.DeletePhoto(path);
            return RedirectToAction("Photos");
        }
    }
}
