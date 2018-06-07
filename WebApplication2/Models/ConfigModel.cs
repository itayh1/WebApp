using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace WebApplication2
{
    public class ConfigModel
    {
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
            //else if (e.CommandID == (int)CommandEnum.CloseCommand)
            //{
            //    this.Removehandler(e.Args[0]);
            //}
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

        //public void Removehandler(string handler)
        //{
        //    try {
        //        // update server handler was removed
        //        CommandRecievedEventArgs command = new CommandRecievedEventArgs(
        //            (int)CommandEnum.CloseCommand, new string[] { handler }, string.Empty);
        //        var serializedData = JsonConvert.SerializeObject(command);
        //        ClientConn.Instance.sendMessage(serializedData);
        //    } catch (Exception ex) {
        //        Console.WriteLine(ex.Message + ", cannot remove this handler");
        //    }
        //}


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

        [Display(Name = "handlers")]
        public List<string> handlers = new List<string> { "h1", "h2", "h3"};
    }
}