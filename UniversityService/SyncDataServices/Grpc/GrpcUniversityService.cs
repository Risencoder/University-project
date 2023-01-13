using System.Threading.Tasks;
using AutoMapper;
using Grpc.Core;
using UniversityService.Data;

namespace UniversityService.SyncDataServices.Grpc
{
    public class GrpcUniversityService : GrpcUniversity.GrpcUniversityBase
    {
        private readonly IUniversityRepo _repository;
        private readonly IMapper _mapper;

        public GrpcUniversityService(IUniversityRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public override Task<UniversityResponse> GetAllUniversitys(GetAllRequest request, ServerCallContext context)
        {
            var response = new UniversityResponse();
            var universitys = _repository.GetAllUniversitys();

            foreach(var plat in universitys)
            {
                response.University.Add(_mapper.Map<GrpcUniversityModel>(plat));
            }

            return Task.FromResult(response);
        }
    }
}