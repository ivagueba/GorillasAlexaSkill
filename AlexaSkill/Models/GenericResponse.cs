﻿namespace AlexaSkill.Models
{
    public class GenericResponse<T>
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}