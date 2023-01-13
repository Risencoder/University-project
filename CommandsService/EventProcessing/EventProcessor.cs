using System;
using System.Text.Json;
using AutoMapper;
using CommandsService.Data;
using CommandsService.Dtos;
using CommandsService.Models;
using Microsoft.Extensions.DependencyInjection;

namespace CommandsService.EventProcessing
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;

        public EventProcessor(IServiceScopeFactory scopeFactory, AutoMapper.IMapper mapper)
        {
            _scopeFactory = scopeFactory;
            _mapper = mapper;
        }

        public void ProcessEvent(string message)
        {
            var eventType = DetermineEvent(message);

            switch (eventType)
            {
                case EventType.UniversityPublished:
                    addUniversity(message);
                    break;
                default:
                    break;
            }
        }

        private EventType DetermineEvent(string notifcationMessage)
        {
            Console.WriteLine("--> Determining Event");

            var eventType = JsonSerializer.Deserialize<GenericEventDto>(notifcationMessage)!;

            switch(eventType.Event)
            {
                case "University_Published":
                    Console.WriteLine("--> University Published Event Detected");
                    return EventType.UniversityPublished;
                default:
                    Console.WriteLine("--> Could not determine the event type");
                    return EventType.Undetermined;
            }
        }

        private void addUniversity(string universityPublishedMessage)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<ICommandRepo>();
                
                var universityPublishedDto = JsonSerializer.Deserialize<UniversityPublishedDto>(universityPublishedMessage);

                try
                {
                    var univ = _mapper.Map<University>(universityPublishedDto);
                    if(!repo.ExternalUniversityExists(univ.ExternalID))
                    {
                        repo.CreateUniversity(univ);
                        repo.SaveChanges();
                        Console.WriteLine("--> University added!");
                    }
                    else
                    {
                        Console.WriteLine("--> University already exisits...");
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Could not add University to DB {ex.Message}");
                }
            }
        }
    }

    enum EventType
    {
        UniversityPublished,
        Undetermined
    }
}