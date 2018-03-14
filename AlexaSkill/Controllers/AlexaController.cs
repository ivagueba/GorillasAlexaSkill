using AlexaSkillGorillas.Data;
using Microsoft.AspNet.SignalR;
using System;
using System.Linq;
using System.Text;
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
            var response = new AlexaResponse("To use the Gorillas skill, you can say, Alexa, ask Gorillas for top courses, to retrieve the top courses or say, Alexa, ask Plural sight for the new courses, to retrieve the latest new courses. You can also say, Alexa, stop or Alexa, cancel, at any time to exit the Plural sight skill. For now, do you want to hear the Top Courses or New Courses?", false);
            response.Response.Reprompt.OutputSpeech.Text = "Please select one, top courses or new courses?";
            return response;
        }

        private AlexaResponse CancelOrStopIntentHandler(Request request)
        {
            return new AlexaResponse("Thanks for listening, let's talk again soon.", true);
        }

        private AlexaResponse ShowFormIntentHandler(Request request)
        {
            var output = "Done";
            var formToLoad = Convert.ToInt32(request.SlotsList[0].Value);
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


        private AlexaResponse SessionEndedRequestHandler(Request request)
        {
            return null;
        }
    }
}