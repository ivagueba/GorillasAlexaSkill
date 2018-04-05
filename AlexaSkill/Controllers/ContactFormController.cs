using AlexaSkill.Models;
using AlexaSkillGorillas.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AlexaSkill.Controllers
{
    public class ContactFormController : ApiController
    {
        //ADD RETURN OBJECT WITH CORRESPONDING STATUS CODE
        [HttpPost, Route("api/contact/submit")]
        public void Post([FromBody]ContactFormData contactFormData)
        {
            //ADD LAYER BETWEEN CONTROLLER AND EF?
            var test = contactFormData;
            var testInfo = new ContactFormData("Alonso", 29, DateTime.Now);

            using (var db = new AlexaGorillas_dbEntities())
            {
                //VALIDATE INFO
                //IF GOOD SAVE / IF NOT THEN RETURN ERROR TO CLIENT
                db.ContactForms.Add(new ContactForm
                {
                    FirstName = testInfo.FirstName,
                    Age = testInfo.Age,
                    Date = testInfo.Date,
                    DayOfTheWeek = "Monday",
                    Gender = "Male"
                });

                db.SaveChanges();
            }

        }
    }
}
