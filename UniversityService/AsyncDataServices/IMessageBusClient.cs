using UniversityService.DTOs;

namespace UniversityService.AsyncDataServices
{
    public interface IMessageBusClient
    {
        void PublishNewUniversity(UniversityPublishedDto universityPublishedDto);
    }
}