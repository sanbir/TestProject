﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Exceptions
{
    public class BusinessLayerException : ApplicationException
    {
        public BusinessLayerException(string message)
            : base(message)
        {
        }

        public BusinessLayerException(string message, Exception ex)
            : base(message, ex)
        {
        }

    }
}
