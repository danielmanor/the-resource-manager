using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResourceManagement.API.Entities
{
    public class Resource
    {
        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(2000)]
        public string Description { get; set; }

        public decimal SomeDecimalValue { get; set; }

        public ICollection<Subresource> SubResources { get; set; } = new List<Subresource>();


    }
}
