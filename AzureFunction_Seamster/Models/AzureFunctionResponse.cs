﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AzureFunction_Seamster.Models
{
    public class AzureFunctionResponse
    {
        /*
       * ErrorCode is 0 for success
       * ErrorCode is 100 for failure of business of validation
       * ErroCode is 200 for validation failure or bad data
       * ErrorCode is 500 for Unknown Technical Error
       */
        public int ErrorCode { get; set; }

        public string Message { get; set; }

    }
}
