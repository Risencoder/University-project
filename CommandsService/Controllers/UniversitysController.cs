using System;
using System.Collections.Generic;
using AutoMapper;
using CommandsService.Data;
using CommandsService.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers
{
    [Route("api/c/[controller]")]
    [ApiController]
    public class UniversitysController : ControllerBase
    {
        private readonly ICommandRepo _repository;
        private readonly IMapper _mapper;

        public UniversitysController(ICommandRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UniversityreadDto>> GetUniversitys()
        {
            Console.WriteLine("--> Getting Universitys from CommandsService");

            var UniversityItems = _repository.GetAllUniversitys();

            return Ok(_mapper.Map<IEnumerable<UniversityreadDto>>(UniversityItems));
        }

        [HttpPost]
        public ActionResult TestInboundConnection()
        {
            Console.WriteLine("--> Inbound POST # Command Service");

            return Ok("Inbound test of from Universitys Controler");
        }
    }
}