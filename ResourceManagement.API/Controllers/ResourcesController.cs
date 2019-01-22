using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ResourceManagement.API.Services;
using AutoMapper;
using Microsoft.Extensions.Primitives;
using System.Diagnostics;
using ResourceManagement.API.Helpers;

namespace ResourceManagement.API.Controllers
{
    [Route("api/resources")]

    public class ResourcesController : ControllerBase
    {
        private readonly IResourceManagementRepository _resourceManagementRepository;
        public ResourcesController(IResourceManagementRepository resourceManagementRepository)
        {
            _resourceManagementRepository = resourceManagementRepository;
        }


        [HttpGet]
        public async Task<IActionResult> GetResources()
        {
            IEnumerable<Entities.Resource> resourcesFromRepo = new List<Entities.Resource>();
            resourcesFromRepo = await _resourceManagementRepository.GetResources();
            var resources = Mapper.Map<IEnumerable<Dtos.Resource>>(resourcesFromRepo);
            return Ok(resources);
        }

        [HttpGet("{resourceId}")]
        public async Task<IActionResult> GetDefaultResource(Guid resourceId)
        {
            if(Request.Headers.TryGetValue("Accept", out StringValues values))
            {
                Debug.WriteLine($"Accept header(s): {string.Join(",", values)}");

            }
            return await GetSpecificResource<Dtos.Resource>(resourceId);
        }

        [HttpGet("{resourceId}", Name = "GetResource")]
        [RequestHeaderMatchesMediaType("Accept", new[] { "application/vnd.dm.resource+json" })]
        public async Task<IActionResult> GetResource(Guid resourceId)
        {
            return await GetSpecificResource<Dtos.Resource>(resourceId);
        }

        [HttpGet("{resourceId}")]
        [RequestHeaderMatchesMediaType("Accept", new[] { "application/vnd.dm.resourcewithsomedecimalvalue+json" })]
        public async Task<IActionResult> GetResourceWithSomeDecimalValue(Guid resourceId)
        {
            return await GetSpecificResource<Dtos.ResourceWithSomeDecimalValue>(resourceId);
        }

        [HttpGet("{resourceId}")]
        [RequestHeaderMatchesMediaType("Accept", new[] { "application/vnd.dm.resourcewithsubresources+json" })]
        public async Task<IActionResult> GetResourceWithSubresources(Guid resourceId)
        {
            return await GetSpecificResource<Dtos.ResourceWithSubresources>(resourceId,true);
        }

        [HttpGet("{resourceId}")]
        [RequestHeaderMatchesMediaType("Accept", new[] { "application/vnd.dm.resourcewithsomedecimalvalueandsubresources+json" })]
        public async Task<IActionResult> GetResourceWithSomeDecimalValueAndSubresources(Guid resourceId)
        {
            return await GetSpecificResource<Dtos.ResourceWithSomeDecimalValueAndSubresources>(resourceId, true);
        }


        private async Task<IActionResult> GetSpecificResource<T>(Guid resourceId,bool includeSubresources = false) where T : class
        {
            var resourceFromRepo = await _resourceManagementRepository.GetResource(resourceId, includeSubresources);

            if(resourceFromRepo == null)
            {
                return BadRequest();
            }

            return Ok(Mapper.Map<T>(resourceFromRepo));
        }
    }
}
