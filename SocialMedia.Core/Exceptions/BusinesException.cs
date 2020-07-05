using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Core.Exceptions
{
    public class BusinesException :Exception
    {
        public BusinesException()
        {

        }
        public BusinesException(string message) :base(message)
        {

        }
    }
}
