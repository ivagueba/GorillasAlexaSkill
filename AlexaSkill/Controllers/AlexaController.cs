using AlexaSkillGorillas.Data;
using Microsoft.AspNet.SignalR;
using System;
using System.Web.Http;

namespace AlexaSkill.Controllers
{
    public class AlexaController : ApiController
    {
        [HttpPost, Route("api/alexa/form")]
        public dynamic GetForm(AlexaRequest alexaRequest)
        {
            var request = new Requests().Create(new AlexaSkillGorillas.Data.Request
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
            var formToLoad = Convert.ToInt32(request.SlotsList[0].Value);
            var output = "Form Number " + formToLoad + " has been loaded for you, please fill the input fills as corresponding.";
            if (formToLoad < 1 || formToLoad > 3)
            {
                output = "No Form with that Id is currently available";
            }
            else
            {
                IHubContext context = GlobalHost.ConnectionManager.GetHubContext<AlexaHub>();
                context.Clients.All.UpdateFormVisibility(request.SlotsList[0].Value);
            }
            return new AlexaResponse(output.ToString());
        }

        private AlexaResponse FillInputNameHandler(Request request)
        {
            var firstName = request.SlotsList[0].Value;
            var output = "We filled your name, Thanks, " + firstName;
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<AlexaHub>();
            context.Clients.All.UpdateFirstNameInputField(firstName);
            return new AlexaResponse(output.ToString());
        }

        private AlexaResponse FillInputDateHandler(Request request)
        {
            var date = request.SlotsList[0].Value;
            var output = "We got you, Thanks. ";
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<AlexaHub>();
            context.Clients.All.UpdateDateInputField(date);
            return new AlexaResponse(output.ToString());
        }

        private AlexaResponse FillInputAgeHandler(Request request)
        {
            var age = request.SlotsList[0].Value;
            var output = "We filled your age, Thanks.";
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<AlexaHub>();
            context.Clients.All.UpdateAgeInputField(age);
            return new AlexaResponse(output.ToString());
        }

        private AlexaResponse FillInputCountryHandler(Request request)
        {
            var country = request.SlotsList[0].Value;
            var output = "Thank you, really beautiful country is " + country;
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<AlexaHub>();
            context.Clients.All.updateCountryInputField(country);
            return new AlexaResponse(output.ToString());
        }

        private AlexaResponse FillInputServiceHandler(Request request)
        {
            var serviceToChoose = Convert.ToInt32(request.SlotsList[0].Value);
            var output = "We are experts on that, thanks!";
            if (serviceToChoose < 1 || serviceToChoose > 4)
            {
                output = "No Service with that value is currently available";
            }
            else
            {
                IHubContext context = GlobalHost.ConnectionManager.GetHubContext<AlexaHub>();
                context.Clients.All.updateServicesInputField(request.SlotsList[0].Value);
            }
            return new AlexaResponse(output.ToString());
        }

        private AlexaResponse FillInputBudgetHandler(Request request)
        {
            var budget = Convert.ToInt32(request.SlotsList[0].Value);

            var output = "You will get the best product for that price, thanks!";
            if (budget != 1500 && budget != 2000 && budget != 2500)
            {
                output = "No Budget with that value is currently available";
            }
            else
            {
                IHubContext context = GlobalHost.ConnectionManager.GetHubContext<AlexaHub>();
                context.Clients.All.updateBudgetInputField(budget);
            }
            return new AlexaResponse(output.ToString());
        }

        private AlexaResponse SessionEndedRequestHandler(Request request)
        {
            return null;
        }
    }
}