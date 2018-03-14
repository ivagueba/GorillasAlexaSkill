using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace AlexaSkill
{
    public class AlexaHub : Hub
    {
        public void UpdateFormVisibility(string formId)
        {
            Clients.All.updateFormVisibility(formId);
        }
        public void Hello()
        {
            Clients.All.hello();
        }
    }
}