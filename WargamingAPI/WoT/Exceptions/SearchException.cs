using System;
using System.Collections.Generic;
using System.Text;

namespace WargamingAPI.WoT.Exceptions
{
    public class SearchException : Exception
    {
        public SearchException(string message) : base(message)
        {

        }
    }
}
