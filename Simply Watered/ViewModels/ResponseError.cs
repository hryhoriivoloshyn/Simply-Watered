using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;

namespace Simply_Watered.ViewModels
{
    public class ResponseError
    {
        public string Message { get; set; }

        public ResponseError(string message)
        {
            Message = message;
        }
    }
}
