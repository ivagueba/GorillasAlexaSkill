﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlexaSkill.Models
{
    public class EmployeeModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string SlackId { get; set; }

        public string ProjectId { get; set; }
    }
}