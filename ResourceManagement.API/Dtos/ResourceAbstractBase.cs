using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResourceManagement.API.Dtos
{
    public class ResourceAbstractBase 
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "required|Title is required.")]
        [MaxLength(200, ErrorMessage = "maxLength|Title is too long.")]
        public string Title { get; set; }

        [MaxLength(2000, ErrorMessage = "maxLength|Description is too long.")]
        public virtual string Description { get; set; }

     
    }
}
