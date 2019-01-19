using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResourceManagement.API.Entities
{
    public class ResourceManager
    {
        public Guid ResourceManagerId { get; set; }

        [MaxLength(200)]
        public string Name { get; set; }
    }
}
