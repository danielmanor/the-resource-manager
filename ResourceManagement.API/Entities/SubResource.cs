using ResourceManagement.API.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResourceManagement.API.Entities
{
    public class Subresource
    {
        [Key]
        public Guid SubResouceId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        
        public SubresourceType SubresourceType { get; set; }

        [ForeignKey("ResourceId")]
        public Resource Resource { get; set; }
        
        public Guid ResourceId { get; set; }
    }
}
