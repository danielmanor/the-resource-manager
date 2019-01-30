using System;
using System.Collections.Generic;


namespace ResourceManagement.API.Dtos
{
    public class ResourceWithSubresourcesAndResourceManagerForCreation : ResourceWithResourceManagerForCreation
    {
        public ICollection<SubresourceForCreation> SubResources { get; set; } = new List<SubresourceForCreation>();
    }
}
