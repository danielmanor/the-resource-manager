using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public Guid ResourceManagerId { get; set; }

        [ForeignKey("ResourceManagerId")]
        public ResourceManager Manager { get; set; }


    }
}
