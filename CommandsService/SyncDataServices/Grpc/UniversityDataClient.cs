using System;
using System.Collections.Generic;
using AutoMapper;
using CommandsService.Models;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;


namespace CommandsService.SyncDataServices.Grpc
{
    public class UniversityDataClient : IUniversityDataClient
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public UniversityDataClient(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        public IEnumerable<University> ReturnAllUniversitys()
        {
            Console.WriteLine($"--> Calling GRPC Service {_configuration["GrpcUniversity"]}");
            var channel = GrpcChannel.ForAddress(_configuration["GrpcUniversity"]!);
            var client = new GrpcUniversity.GrpcUniversityClient(channel);
            var request = new GetAllRequest();

            try
            {
                var reply = client.GetAllUniversitys(request);
                return _mapper.Map<IEnumerable<University>>(reply.University);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Couldnot call GRPC Server {ex.Message}");
                return null!;
            }
        }
    }
}