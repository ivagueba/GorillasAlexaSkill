using AlexaSkillGorillas.Data;
using System;
using System.Linq;
using System.Web.Http;
using AlexaSkill.Helper;
using AlexaSkillGorillas.BL.Services;

namespace AlexaSkill.Controllers
{
    public class AlexaController : ApiController
    {
        private readonly SignalRClientMethodsHelper _signalRClientMethodsHelper;
        private readonly EmployeeService _employeeService;
        private readonly SkillService _skillService;
        private readonly ProjectService _projectService;

        public AlexaController()
        {
            _signalRClientMethodsHelper = new SignalRClientMethodsHelper();
            _employeeService = new EmployeeService();
            _skillService = new SkillService();
            _projectService = new ProjectService();
        }

        [HttpPost, Route("api/alexa/command")]
        public dynamic AlexaRequestHandler(AlexaRequest alexaRequest)
        {
            var request = new Requests().Create(new Request
            {
                MemberId = (alexaRequest.Session.Attributes == null) ? 0 : alexaRequest.Session.Attributes.MemberId,
                Timestamp = alexaRequest.Request.Timestamp,
                Intent = (alexaRequest.Request.Intent == null) ? "" : alexaRequest.Request.Intent.Name,
                AppId = alexaRequest.Session.Application.ApplicationId,
                RequestId = alexaRequest.Request.RequestId,
                SessionId = alexaRequest.Session.SessionId,
                UserId = alexaRequest.Session.User.UserId,
                IsNew = alexaRequest.Session.New,
                Version = alexaRequest.Version,
                Type = alexaRequest.Request.Type,
                Reason = alexaRequest.Request.Reason,
                SlotsList = alexaRequest.Request.Intent.GetSlots(),
                DateCreated = DateTime.UtcNow
            });

            AlexaResponse response = null;

            switch (request.Type)
            {
                case "LaunchRequest":
                    response = LaunchRequestHandler(request);
                    break;
                case "IntentRequest":
                    response = IntentRequestHandler(request);
                    break;
                case "SessionEndedRequest":
                    response = SessionEndedRequestHandler(request);
                    break;
            }
            return response;
        }
        private AlexaResponse LaunchRequestHandler(Request request)
        {
            var response = new AlexaResponse("Welcome Gorilla to the Website Navigaton Skill for Alexa");
            response.Session.MemberId = request.MemberId;
            response.Response.Card.Title = "Gorillas";
            response.Response.Card.Content = "Hello\ncruel world!";
            response.Response.Reprompt.OutputSpeech.Text = "Please pick one form";
            response.Response.ShouldEndSession = false;

            return response;
        }
        private AlexaResponse IntentRequestHandler(Request request)
        {
            AlexaResponse response = null;

            switch (request.Intent)
            {
                case "ShowFormIntent":
                    response = ShowFormIntentHandler(request);
                    break;
                case "FillInputName":
                    response = FillInputNameHandler(request);
                    break;
                case "FillInputDate":
                    response = FillInputDateHandler(request);
                    break;
                case "FillInputAge":
                    response = FillInputAgeHandler(request);
                    break;
                case "FillInputCountry":
                    response = FillInputCountryHandler(request);
                    break;
                case "FillInputServices":
                    response = FillInputServiceHandler(request);
                    break;
                case "FillInputBudget":
                    response = FillInputBudgetHandler(request);
                    break;
                case "FillTrainingDay":
                    response = FillTrainingDayHandler(request);
                    break;
                case "FillInputGender":
                    response = FillGenderHandler(request);
                    break;
                case "SubmitForm":
                    response = SubmitForm(request);
                    break;
                case "AddSkillToEmployee":
                    response = AddSkillToEmployee(request);
                    break;
                case "AddEmployeeToProject":
                    response = AddEmployeeToProject(request);
                    break;
                case "AMAZON.CancelIntent":
                case "AMAZON.StopIntent":
                    response = CancelOrStopIntentHandler(request);
                    break;
                case "AMAZON.HelpIntent":
                    response = HelpIntent(request);
                    break;
            }
            return response;
        }

        private AlexaResponse AddEmployeeToProject(Request request)
        {
            var projectName = request.SlotsList.FirstOrDefault(s => s.Key == "projectName").Value;
            var employeeName = request.SlotsList.FirstOrDefault(s => s.Key == "employeeName").Value;

            var output = _projectService.AddEmployeeToProjectByName(employeeName, projectName);
            _signalRClientMethodsHelper.RefreshProjectsList();

            return new AlexaResponse(output, false);
        }

        private AlexaResponse AddSkillToEmployee(Request request)
        {
            var skillName = request.SlotsList.FirstOrDefault(s => s.Key == "skillName").Value;
            var employeeName = request.SlotsList.FirstOrDefault(s => s.Key == "employeeName").Value;

            var output = _employeeService.AddSkillToEmployeeByName(skillName, employeeName);
            _signalRClientMethodsHelper.RefreshEmployeesList();

            return new AlexaResponse(output, false);
        }

        private AlexaResponse SubmitForm(Request request)
        {
            var output = "Thanks.";
            _signalRClientMethodsHelper.SubmitForm();
            return new AlexaResponse(output);
        }
        private AlexaResponse HelpIntent(Request request)
        {
            var response = new AlexaResponse("To use the Gorillas skill, you can say,", false);
            response.Response.Reprompt.OutputSpeech.Text = "Please select one form";
            return response;
        }
        private AlexaResponse CancelOrStopIntentHandler(Request request)
        {
            return new AlexaResponse("Thanks for listening, let's talk again soon.", true);
        }
        private AlexaResponse ShowFormIntentHandler(Request request)
        {
            var formToLoad = GetStringSlot(request);
            var output = "Form " + formToLoad + " has been loaded for you, please fill the input fills as corresponding.";
            if (!formToLoad.ToLower().Equals("projects") && !formToLoad.ToLower().Equals("employees") && !formToLoad.ToLower().Equals("skills"))
            {
                output = "No Form with that Id is currently available";
            }
            else
            {
                _signalRClientMethodsHelper.ShowForm(formToLoad);
            }
            return new AlexaResponse(output, false);
        }
        private AlexaResponse FillInputNameHandler(Request request)
        {
            var firstName = GetStringSlot(request);
            var output = "We filled your name, Thanks, " + firstName;

            _signalRClientMethodsHelper.UpdateFormField("firstName", firstName);
            
            return new AlexaResponse(output, false);
        }
        private AlexaResponse FillInputDateHandler(Request request)
        {
            var date = GetStringSlot(request);
            var output = "We got you, Thanks. ";

            _signalRClientMethodsHelper.UpdateFormField("date", date);
            
            return new AlexaResponse(output, false);
        }
        private AlexaResponse FillInputAgeHandler(Request request)
        {
            var age = GetStringSlot(request);
            var output = "We filled your age, Thanks.";

            _signalRClientMethodsHelper.UpdateFormField("age", age);
            
            return new AlexaResponse(output.ToString(), false);
        }
        private AlexaResponse FillInputCountryHandler(Request request)
        {
            var country = GetStringSlot(request);
            var output = "Thank you, really beautiful country is " + country;

            _signalRClientMethodsHelper.UpdateFormField("country", country);

            return new AlexaResponse(output, false);
        }
        private AlexaResponse FillInputServiceHandler(Request request)
        {
            var serviceToChoose = Convert.ToInt32(GetStringSlot(request));
            var output = "We are experts on that, thanks!";
            if (serviceToChoose < 1 || serviceToChoose > 4)
            {
                output = "No Service with that value is currently available";
            }
            else
            {
                _signalRClientMethodsHelper.UpdateFormField("service", serviceToChoose.ToString());
            }
            return new AlexaResponse(output, false);
        }
        private AlexaResponse FillInputBudgetHandler(Request request)
        {
            var budget = Convert.ToInt32(GetStringSlot(request));

            var output = "You will get the best product for that price, thanks!";
            if (budget != 1500 && budget != 2000 && budget != 2500)
            {
                output = "No Budget with that value is currently available";
            }
            else
            {
                _signalRClientMethodsHelper.UpdateFormField("budget", budget.ToString());
            }
            return new AlexaResponse(output, false);
        }
        private AlexaResponse FillTrainingDayHandler(Request request)
        {
            var day = GetStringSlot(request);
            var output = "Got you";

            _signalRClientMethodsHelper.UpdateFormField("trainingDay", day);
            return new AlexaResponse(output, false);
        }
        private AlexaResponse FillGenderHandler(Request request)
        {
            var gender = GetStringSlot(request);
            var output = "Got you";
            
            _signalRClientMethodsHelper.UpdateFormField("gender", gender);
            return new AlexaResponse(output, false);
        }
        private AlexaResponse SessionEndedRequestHandler(Request request)
        {
            return null;
        }
        private string GetStringSlot(Request request)
        {
            return request.SlotsList[0].Value;
        }
    }
}