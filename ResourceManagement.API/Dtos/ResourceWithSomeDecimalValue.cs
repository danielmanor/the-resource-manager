using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourceManagement.API.Dtos
{
    public class ResourceWithSomeDecimalValue : Resource
    {
        public decimal SomeDecimalValue { get; set; }
    }
}
