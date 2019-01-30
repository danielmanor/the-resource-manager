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
        Task<Resource> GetResource(Guid resourceId, bool includeSubResources = false);
        Task<IEnumerable<Resource>> GetResources(bool includeSubResources = false);
        Task<IEnumerable<Resource>> GetResourceForManager(Guid managerId, bool includeSubResources = false);
        Task<bool> IsResourceManager(Guid resourceId, Guid managerId);
        Task<bool> SaveAsync();
        Task<bool> ResourceExists(Guid resourceId);
        Task UpdateResource(Resource resource);
        Task<IEnumerable<Subresource>> GetSubResources(Guid resourceId);
        Task<IEnumerable<Subresource>> GetSubResources(Guid resourceId, IEnumerable<Guid> subResourceIds);
        Task AddSubResource(Guid resourceId, Subresource subResource);
        Task<IEnumerable<ResourceManager>> GetResourceManagers();

    }
}
