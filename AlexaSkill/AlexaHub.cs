using Microsoft.AspNet.SignalR;

namespace AlexaSkill
{
    public class AlexaHub : Hub
    {
        public void UpdateFormVisibility(string formId)
        {
            Clients.All.updateFormVisibility(formId);
        }

        public void SubmitForm()
        {
            Clients.All.submitForm();
        }

        public void UpdateFirstNameInputField(string firstName)
        {
            Clients.All.updateFirstNameInputField(firstName);
        }

        public void UpdateAgeInputField(string age)
        {
            Clients.All.updateAgeInputField(age);
        }

        public void UpdateCountryInputField(string country)
        {
            Clients.All.updateCountryInputField(country);
        }

        public void UpdateBudgetInputField(int budget)
        {
            Clients.All.UpdateBudgetInputField(budget);
        }

        public void UpdateServicesInputField(int service)
        {
            Clients.All.updateServicesInputField(service);
        }

        public void UpdateDateInputField(string date)
        {
            Clients.All.updateDateInputField(date);
        }

        public void Hello()
        {
            Clients.All.hello();
        }
    }
}