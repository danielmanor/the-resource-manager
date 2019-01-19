using ResourceManagement.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourceManagement.API.Services
{
    public interface IResourceManagementRepository
    {
        Task AddResource(Resource resource);
        Task DeleteResource(Resource resource);
        Task<Resource> GetResource(Guid resourceId, bool includeSubResources);
    }
}
