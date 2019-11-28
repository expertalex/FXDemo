using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;

namespace FXDemo.Models.Http
{
    public class ApiError
    {
        public string Message { get; set; }

        public string Detail { get; set; }


        public ApiError(){ }

        public ApiError(string message)
        {
            this.Message = message;
        }

        public ApiError(ModelStateDictionary modelState)
        {
            Message = "Invalid parameters.";
            Detail = modelState
                .FirstOrDefault(x => x.Value.Errors.Any()).Value.Errors
                .FirstOrDefault().ErrorMessage;
        }


    }
}
