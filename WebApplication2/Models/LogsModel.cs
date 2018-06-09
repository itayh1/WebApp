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
                this.logs = new List<LogObject>() { new LogObject(MessageTypeEnum.INFO.ToString(), "asddfsgsff"),
                new LogObject(MessageTypeEnum.WARNING.ToString(), "abcdef")};
                //ClientConn client = ClientConn.Instance;
                //client.OnCommandRecieved += this.OnCommandRecieved;
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

        public List<LogObject> filter(string st)
        {
            List<LogObject> newList = new List<LogObject>();
            foreach (LogObject log in this.logs)
            {
                if (log.Type == st)
                    newList.Add(log);
            }
            return newList;
        }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "logs")]
        public List<LogObject> logs;
    }
}