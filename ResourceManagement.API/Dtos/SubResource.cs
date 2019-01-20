using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourceManagement.API.Dtos
{
    public class SubResource : SubResourceAbstractBase
    {
        Guid SubResourceId { get; set; }
    }
}
