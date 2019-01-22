using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourceManagement.API.Dtos
{
    public class ResourceWithSubresources
    {
        public ICollection<Subresource> Subresources { get; set; } = new List<Subresource>();
    }
}
