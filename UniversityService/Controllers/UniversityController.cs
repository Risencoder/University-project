using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UniversityService.AsyncDataServices;
using UniversityService.Data;
using UniversityService.DTOs;
using UniversityService.Models;
using UniversityService.SyncDataServices.Http;

namespace UniversityService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UniversitysController : ControllerBase
    {
        private readonly IUniversityRepo _repository;
        private readonly IMapper _mapper;
        private readonly ICommandDataClient _commandDataClient;
        private readonly IMessageBusClient _messageBusClient;

        public UniversitysController(
            IUniversityRepo repository, 
            IMapper mapper,
            ICommandDataClient commandDataClient,
            IMessageBusClient messageBusClient)
        {
            _repository = repository;
            _mapper = mapper;
            _commandDataClient = commandDataClient;
            _messageBusClient = messageBusClient;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UniversityReadDto>> GetUniversitys()
        {
            Console.WriteLine("--> Getting Universitys....");

            var universityItem = _repository.GetAllUniversitys();

            return Ok(_mapper.Map<IEnumerable<UniversityReadDto>>(universityItem));
        }

        [HttpGet("{id}", Name = "GetUniversityById")]
        public ActionResult<UniversityReadDto> GetUniversityById(int id)
        {
            var universityItem = _repository.GetUniversityById(id);
            if (universityItem != null)
            {
                return Ok(_mapper.Map<UniversityReadDto>(universityItem));
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<UniversityReadDto>> CreateUniversity(UniversityCreateDto universityCreateDto)
        {
            var universityModel = _mapper.Map<University>(universityCreateDto);
            _repository.CreateUniversity(universityModel);
            _repository.SaveChanges();

            var universityReadDto = _mapper.Map<UniversityReadDto>(universityModel);

            // Send Sync Message
            try
            {
                await _commandDataClient.SendUniversityToCommand(universityReadDto);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"--> Could not send synchronously: {ex.Message}");
            }

            //Send Async Message
            try
            {
                var universityPublishedDto = _mapper.Map<UniversityPublishedDto>(universityReadDto);
                universityPublishedDto.Event = "University_Published";
                _messageBusClient.PublishNewUniversity(universityPublishedDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not send asynchronously: {ex.Message}");
            }

            return CreatedAtRoute(nameof(GetUniversityById), new { Id = universityReadDto.Id}, universityReadDto);
        }
    }
}