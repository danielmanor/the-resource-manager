using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ResourceManagement.API.Services;
using AutoMapper;

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
    }
}
