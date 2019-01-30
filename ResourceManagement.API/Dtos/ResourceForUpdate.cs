using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResourceManagement.API.Dtos
{
    public class ResourceForUpdate : ResourceAbstractBase
    {
        [Required(AllowEmptyStrings = false,ErrorMessage = "required|When updating a resource, the description is required.")]
        public override string Description
        { get => base.Description; set => base.Description = value; }
    }
}
