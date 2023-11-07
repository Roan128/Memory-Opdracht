using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory.Model.Exceptions
{
    public class CardNotFoundException : Exception
    {
        public CardNotFoundException() { }

        public CardNotFoundException(string message) : base(message) {
            
        }
    }
}
