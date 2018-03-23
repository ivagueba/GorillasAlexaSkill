using AlexaSkillGorillas.Data;
using Microsoft.AspNet.SignalR;
using System;
using System.Web.Http;

namespace AlexaSkill.Controllers
{
    public class AlexaController : ApiController
    {

        public readonly IHubContext context;

        public AlexaController()
        {
            context = GlobalHost.ConnectionManager.GetHubContext<AlexaHub>();
        }

        [HttpPost, Route("api/alexa/form")]
        public dynamic GetForm(AlexaRequest alexaRequest)
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
            var response = new AlexaResponse("Welcome Gorillas. Welcome to Website Navigator");
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
        private AlexaResponse SubmitForm(Request request)
        {
            var output = "Thanks.";
            context.Clients.All.SubmitForm();
            return new AlexaResponse(output.ToString());
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
            var formToLoad = Convert.ToInt32(GetStringSlot(request));
            var output = "Form Number " + formToLoad + " has been loaded for you, please fill the input fills as corresponding.";
            if (formToLoad < 1 || formToLoad > 3)
            {
                output = "No Form with that Id is currently available";
            }
            else
            {
                context.Clients.All.UpdateFormVisibility(GetStringSlot(request));
            }
            return new AlexaResponse(output.ToString());
        }

        private AlexaResponse FillInputNameHandler(Request request)
        {
            var firstName = GetStringSlot(request);
            var output = "We filled your name, Thanks, " + firstName;
            context.Clients.All.UpdateFirstNameInputField(firstName);
            return new AlexaResponse(output.ToString());
        }

        private AlexaResponse FillInputDateHandler(Request request)
        {
            var date = GetStringSlot(request);
            var output = "We got you, Thanks. ";
            context.Clients.All.UpdateDateInputField(date);
            return new AlexaResponse(output.ToString());
        }

        private AlexaResponse FillInputAgeHandler(Request request)
        {
            var age = GetStringSlot(request);
            var output = "We filled your age, Thanks.";
            context.Clients.All.UpdateAgeInputField(age);
            return new AlexaResponse(output.ToString());
        }

        private AlexaResponse FillInputCountryHandler(Request request)
        {
            var country = GetStringSlot(request);
            var output = "Thank you, really beautiful country is " + country;
            context.Clients.All.updateCountryInputField(country);
            return new AlexaResponse(output.ToString());
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
                context.Clients.All.updateServicesInputField(GetStringSlot(request));
            }
            return new AlexaResponse(output.ToString());
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
                context.Clients.All.updateBudgetInputField(budget);
            }
            return new AlexaResponse(output.ToString());
        }

        private AlexaResponse FillTrainingDayHandler(Request request)
        {
            var day = GetStringSlot(request);
            var output = "Got you";
            context.Clients.All.UpdateTrainingDayInputField(day);
            return new AlexaResponse(output.ToString());
        }

        private AlexaResponse FillGenderHandler(Request request)
        {
            var gender = GetStringSlot(request);
            var output = "Got you";
            context.Clients.All.UpdateGenderInputField(gender);
            return new AlexaResponse(output.ToString());
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