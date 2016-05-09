﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventureWorksService.Models
{
    public class OperationStatus
    {
        public bool Success { get; set; }        
        public List<string> Messages { get; set; }

        public OperationStatus()
        {
            Messages = new List<string>();
        }

        public static OperationStatus CreateFromException(string message, Exception ex)
        {
            OperationStatus opStatus = new OperationStatus
            {
                Success = true,
                Messages = new List<string>(),
            };

            if (ex != null)
            {
                opStatus.Messages.Add(ex.Message);
                opStatus.Success = false;
            }
            return opStatus;
        }
    }
}
