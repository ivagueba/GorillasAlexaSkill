using AlexaSkillGorillas.BL.Services;
using Microsoft.AspNet.SignalR;

namespace AlexaSkill.Helper
{
    public class SignalRClientMethodsHelper
    {
        private readonly IHubContext _hubContext;
        private readonly EmployeeService _employeeService;
        private readonly ProjectService _projectService;

        public SignalRClientMethodsHelper()
        {
            _hubContext = GlobalHost.ConnectionManager.GetHubContext<AlexaHub>();
            _employeeService = new EmployeeService();
            _projectService = new ProjectService();
        }

        public void RefreshEmployeesList()
        {
            var employees = _employeeService.GetEmployees();
            _hubContext.Clients.All.GetEmployeesList(employees);
        }

        public void RefreshProjectsList()
        {
            var projects  = _projectService.GetProjects();
            _hubContext.Clients.All.GetEmployeesList(projects);
        }

        public void SubmitForm(string form)
        {
            _hubContext.Clients.All.SubmitForm(form);
        }

        public void ShowForm(string form)
        {
            _hubContext.Clients.All.UpdateFormVisibility(form);
        }

        public void UpdateFormField(string fieldName, string value)
        {
            _hubContext.Clients.All.UpdateFormField(fieldName, value);
        }
    }
}