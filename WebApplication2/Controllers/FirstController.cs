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
        
        public static ConfigModel configModel = new ConfigModel();
        public static RemoveHandlerModel rmvHandlerModel = new RemoveHandlerModel();
        public static LogsModel logsModel = new LogsModel();
        public static PhotosModel photosModel = new PhotosModel();
        static MainPageModel mainPageModel = new MainPageModel();


        [HttpGet]
        public ActionResult Configurations()
        {
            return View(configModel);
        }

        public ActionResult Photos()
        {
            photosModel.OutputDir = configModel.outputDir;
            photosModel.SetPhotos();
            return View(photosModel);
        }


        [HttpGet]
        public ActionResult AjaxView()
        {
            mainPageModel.numOfImages = Thumbnail.count;
            ViewBag.numOfImages = mainPageModel.numOfImages;
            return View(mainPageModel);
        }


        public ActionResult RemoveHandler(string handler)
        {
            rmvHandlerModel.handler = handler;
            return View(rmvHandlerModel);
        }


        public ActionResult HandlerDeleted(string handler)
        {
            configModel.manualResetEvent.Reset();
            // update server handler was removed
            CommandRecievedEventArgs msg = new CommandRecievedEventArgs((int)CommandEnum.CloseCommand, new string[] { handler }, string.Empty);
            ClientConn.Instance.sendMessage(JsonConvert.SerializeObject(msg));
            // wait for response from server
            configModel.manualResetEvent.WaitOne();
            return RedirectToActionPermanent("Configurations");
        }

        [HttpGet]
        public ActionResult Logs()
        {
            return View(logsModel);
        }


        [HttpPost]
        public ActionResult Logs(FormCollection frm)
        {
            string type = frm["filter"].ToString();
            List<LogObject> filtered = logsModel.filter(type);
            LogsModel tempModel = new LogsModel();
            tempModel.logs = filtered;
            return View(tempModel);
        }



        public ActionResult ThumbnailView(string path)
        {
            Thumbnail thumbnail = new Thumbnail(path);
            return View(thumbnail);
        }


        public ActionResult ThumbnailDelete(string path)
        {
            Thumbnail thumbnail = new Thumbnail(path);
            return View(thumbnail);
        }


        public ActionResult DeleteAfterViewed(string path)
        {
            photosModel.DeletePhoto(path);
            Thumbnail.count--;
            return RedirectToAction("Photos");
        }
    }
}
