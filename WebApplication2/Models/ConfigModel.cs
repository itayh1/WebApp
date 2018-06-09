using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using Newtonsoft.Json;

namespace WebApplication2
{
    public class ConfigModel
    {
        public ManualResetEvent manualResetEvent = new ManualResetEvent(false);
        public static bool handlerHasDeleted = false; 

        public ConfigModel()
        {
            ClientConn client = ClientConn.Instance;
            client.OnCommandRecieved += this.OnCommandRecieved;
        }

        public void OnCommandRecieved(object sender, CommandRecievedEventArgs e)
        {
            if (e.CommandID == (int)CommandEnum.GetConfigCommand)
            {
                this.SetSettings(e);
            }
            else if (e.CommandID == (int)CommandEnum.CloseCommand)
            {
                this.Remove(e.Args[0]);
            }
        }

        private void SetSettings(CommandRecievedEventArgs e)
        {
            ConfigurationData cd = JsonConvert.DeserializeObject<ConfigurationData>(e.Args[0]);
            this.outputDir = cd.outputDir;
            this.logName = cd.logName;
            this.sourceName = cd.sourceName;
            this.thumbnailSize = cd.thumbnailSize.ToString();
            this.handlers = new List<string>(cd.handlers);
        }

        public void Remove(string handler)
        {
            // delete from configModel
            if (this.handlers.Contains(handler))
            {
                this.handlers.Remove(handler);
            }
            // update page handler has removed
           // this.manualResetEvent.Set();
        }


        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "outputDir")]
        public string outputDir;

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "logName")]
        public string logName;

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "sourceName")]
        public string sourceName;

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "thumbnailSize")]
        public string thumbnailSize;

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "handlers")]
        public List<string> handlers = new List<string> { "h1", "h2", "h3"};
    }
}