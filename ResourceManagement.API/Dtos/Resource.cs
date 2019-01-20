using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourceManagement.API.Dtos
{
    public class Resource : ResourceAbstractBase
    {
        Guid ResourceId { get; set; }
    }
}
