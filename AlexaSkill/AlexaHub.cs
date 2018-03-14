using Microsoft.AspNet.SignalR;

namespace AlexaSkill
{
    public class AlexaHub : Hub
    {
        public void UpdateFormVisibility(string formId)
        {
            Clients.All.updateFormVisibility(formId);
        }

        public void UpdateFirstNameInputField(string firstName)
        {
            Clients.All.updateFirstNameInputField(firstName);
        }

        public void Hello()
        {
            Clients.All.hello();
        }
    }
}