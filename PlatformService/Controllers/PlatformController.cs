using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformController : ControllerBase
    {
        private IPlatformRepo _repository { get; }
        private IMapper _mapper { get; }
        public PlatformController(IPlatformRepo repository, IMapper mapper)
        => (_repository, _mapper) = (repository, mapper);

        [HttpGet]
        public IActionResult GetPlatforms()
        {
            Console.WriteLine("-- Get Platforms");
            var platformsItems = _repository.GetAllPlatforms();

            var result = _mapper.Map<IEnumerable<PlatformReadDto>>(platformsItems);

            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetPlatformById")]
        public IActionResult GetPlatformById(int id)
        {
            Console.WriteLine($"-- Get Platform by {id} id");
            var platformItem = _repository.GetPlatformById(id);

            if(platformItem is null)
                return NotFound($"Platform with id {id} not found");

            var result = _mapper.Map<PlatformReadDto>(platformItem);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreatePlatform(PlatformCreateDto platform)
        {
            var platformModel = _mapper.Map<Platform>(platform);
            _repository.CreatePlatform(platformModel);
            _repository.SaveChanges();

            var platformReadDto = _mapper.Map<PlatformReadDto>(platformModel);

            return CreatedAtRoute(nameof(GetPlatformById), new { Id = platformReadDto.Id}, platformReadDto);
        }
    }
}