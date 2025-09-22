using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftBuy.Shared.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string name , object key)
            :base($"{name} with ({key}) is not found")
        {
            
        }
    }
}
