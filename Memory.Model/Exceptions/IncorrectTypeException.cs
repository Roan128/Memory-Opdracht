using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory.Model.Exceptions
{
    public class IncorrectTypeException : Exception
    {

        public IncorrectTypeException() { }

        public IncorrectTypeException(string message) : base(message) { }
    }
}
