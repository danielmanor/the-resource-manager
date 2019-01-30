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
using Microsoft.AspNetCore.JsonPatch;

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

        ///// add /////
        [HttpPost]
        [RequestHeaderMatchesMediaType("Content-Type",new[] { "application/json", "application/vnd.dm.resourceforcreation+json" })]
        public async Task<IActionResult> AddResource([FromBody] Dtos.ResourceForCreation resource)
        {
            return await AddSpecificResource(resource);
        }

        [HttpPost]
        [RequestHeaderMatchesMediaType("Content-Type", new[] { "application/json", "application/vnd.dm.resourcewithresourcemanagerforcreation+json" })]
        public async Task<IActionResult> AddResourceWithResourceManager([FromBody] Dtos.ResourceWithResourceManagerForCreation resource)
        {
            return await AddSpecificResource(resource);
        }

        [HttpPost]
        [RequestHeaderMatchesMediaType("Content-Type", new[] { "application/json", "application/vnd.dm.resourcewithsubresourcesforcreation+json" })]
        public async Task<IActionResult> AddResourceWithSubResources([FromBody] Dtos.ResourceWithSubresourcesForCreation resource)
        {
            return await AddSpecificResource(resource);
        }

        [HttpPost]
        [RequestHeaderMatchesMediaType("Content-Type", new[] { "application/json", "application/vnd.dm.resourcewithsubresourcesandresourcemanagerforcreation+json" })]
        public async Task<IActionResult> AddResourceWithSubResourcesAndResourceManager([FromBody] Dtos.ResourceWithSubresourcesAndResourceManagerForCreation resource)
        {
            return await AddSpecificResource(resource);
        }

        public async Task<IActionResult> AddSpecificResource<T>(T resource) where T : class
        {
            if(resource == null)
            {
                return BadRequest();
            }

            if(!ModelState.IsValid)
            {
                return new UnprocessableEntityObjectResult(ModelState);
            }

            var resourceEntity = Mapper.Map<Entities.Resource>(resource);

            await _resourceManagementRepository.AddResource(resourceEntity);

            if(!await _resourceManagementRepository.SaveAsync())
            {
                throw new Exception("adding resource failed on save");
            }

            var resourceRes = Mapper.Map<Dtos.Resource>(resourceEntity);

            return CreatedAtRoute("GetResource", new { resourceId = resourceRes.ResourceId });
        }

        [HttpPatch("{resourceId}")]
        public async Task<IActionResult> PartiallyUpdateResource(Guid resourceId, [FromBody] JsonPatchDocument<Dtos.ResourceForUpdate> jsonPatchDocument)
        {
            if(jsonPatchDocument == null)
            {
                return BadRequest();
            }

            var resFromDb = await _resourceManagementRepository.GetResource(resourceId);

            if(resFromDb == null)
            {
                return BadRequest();
            }

            var resourceToPatch = Mapper.Map<Dtos.ResourceForUpdate>(resFromDb);

            jsonPatchDocument.ApplyTo(resourceToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return new UnprocessableEntityObjectResult(ModelState);
            }

            if (!TryValidateModel(resourceToPatch))
            {
                return new UnprocessableEntityObjectResult(ModelState);
            }

            Mapper.Map(resourceToPatch, resFromDb);

            await _resourceManagementRepository.UpdateResource(resFromDb);

            if(!await _resourceManagementRepository.SaveAsync())
            {
                throw new Exception("Updating a resource failed on save.");
            }

            return NoContent();
        }
       

    }
}
