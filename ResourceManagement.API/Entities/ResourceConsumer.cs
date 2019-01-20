using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResourceManagement.API.Entities
{
    public class ResourceConsumer
    {
        [Key]
        public Guid ResourceConsumerId { get; set; }

        [Required]
        [MaxLength(250)]
        public string Name { get; set; }
    }
}
