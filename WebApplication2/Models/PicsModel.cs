﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace WebApplication2
{
    public class PicsModel
    {
        private string outputDir;

        public PicsModel()
        {
            thumbnails = new List<ThumbnailPic>();
            ClientConn client = ClientConn.Instance;
            client.OnCommandRecieved += this.OnCommandRecieved;
        }

        private void OnCommandRecieved(object sender, CommandRecievedEventArgs e)
        {
            ConfigurationData cd = JsonConvert.DeserializeObject<ConfigurationData>(e.Args[0]);
            this.outputDir = cd.outputDir;
            this.setPics();
        }

        private void setPics()
        {

        }

        [Display(Name = "thumbnails")]
        public List<ThumbnailPic> thumbnails;
    }
}