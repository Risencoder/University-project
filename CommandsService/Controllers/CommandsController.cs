using System;
using System.Collections.Generic;
using AutoMapper;
using CommandsService.Data;
using CommandsService.Dtos;
using CommandsService.Models;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers
{
    [Route("api/c/universitys/{universityId}/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommandRepo _repository;
        private readonly IMapper _mapper;

        public CommandsController(ICommandRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetCommandsForUniversity(int universityId)
        {
            Console.WriteLine($"--> Hit GetCommandsForUniversity: {universityId}");

            if (!_repository.PlaformExits(universityId))
            {
                return NotFound();
            }

            var commands = _repository.GetCommandsForUniversity(universityId);

            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commands));
        }

        [HttpGet("{commandId}", Name = "GetCommandForUniversity")]
        public ActionResult<CommandReadDto> GetCommandForUniversity(int universityId, int commandId)
        {
            Console.WriteLine($"--> Hit GetCommandForUniversity: {universityId} / {commandId}");

            if (!_repository.PlaformExits(universityId))
            {
                return NotFound();
            }

            var command = _repository.GetCommand(universityId, commandId);

            if(command == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CommandReadDto>(command));
        }

        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommandForUniversity(int universityId, CommandCreateDto commandDto)
        {
             Console.WriteLine($"--> Hit CreateCommandForUniversity: {universityId}");

            if (!_repository.PlaformExits(universityId))
            {
                return NotFound();
            }

            var command = _mapper.Map<Command>(commandDto);

            _repository.CreateCommand(universityId, command);
            _repository.SaveChanges();

            var commandReadDto = _mapper.Map<CommandReadDto>(command);

            return CreatedAtRoute(nameof(GetCommandForUniversity),
                new {universityId = universityId, commandId = commandReadDto.Id}, commandReadDto);
        }

    }
}