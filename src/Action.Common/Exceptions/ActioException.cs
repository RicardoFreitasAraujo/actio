using System;
using System.Collections.Generic;
using System.Text;

namespace Actio.Common.Exceptions
{
    public class ActioException: Exception
    {
        public ActioException()
        {
        }

        public ActioException(string code, string message): base(message)
        {
            Code = code;
        }

        public string Code { get; }
    }
}
