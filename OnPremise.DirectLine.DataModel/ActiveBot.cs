using System;
using System.Collections.Generic;
using System.Text;

namespace OnPremise.DirectLine.DataModel
{
    public class ActiveBot
    {
        public string BotId { get; set; }
        public string ServiceUrl { get; set; }
        public string msaAppId { get; set; }
        public string msaPassword { get; set; }
        public string CurrentUserId { get; set; }
        public Userbyid UserById { get; set; }
        public Conversation Conversation { get; set; }
        public static ActiveBot CreateActiveBot(string serviceUrl)
        {
            ActiveBot currentBot = new ActiveBot();
            //currentBot.BotId = "EVETUDEBOT";
            currentBot.ServiceUrl = serviceUrl;
            //currentBot.CurrentUserId = "default-user";
            //currentBot.UserById = new Userbyid();
            //currentBot.UserById.Id = currentBot.CurrentUserId;
            //currentBot.UserById.Name = "User";

            return currentBot;

        }
    }

    public class Userbyid
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
