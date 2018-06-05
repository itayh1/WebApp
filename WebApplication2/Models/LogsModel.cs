using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace WebApplication2
{
    public class LogsModel
    {
        public LogsModel()
        {
            try
            {
                ClientConn client = ClientConn.Instance;
                client.OnCommandRecieved += this.OnCommandRecieved;
            }
            catch (Exception ex)
            {

            }
        }

        private void OnCommandRecieved(object sender, CommandRecievedEventArgs e)
        {
            try
            {
                if (e == null) return;
                if (e.CommandID == (int)CommandEnum.GetListLogCommand)
                {
                    List<LogObject> temp = JsonConvert.DeserializeObject<List<LogObject>>(e.Args[0]);
                    this.logs.AddRange(temp);
                }
                else if (e.CommandID == (int)CommandEnum.LogCommand)
                {
                    LogObject newLog = new LogObject(e.Args[0], e.Args[1]);
                    this.logs.Add(newLog);
                }
            }
            catch (Exception ex)
            {

            }
        }

        [Display(Name = "logs")]
        public List<LogObject> logs;
    }
}