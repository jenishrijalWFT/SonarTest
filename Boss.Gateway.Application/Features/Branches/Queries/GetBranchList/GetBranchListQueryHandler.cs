using AutoMapper;
using Boss.Gateway.Application.Contracts.Persistence;
using MediatR;


namespace Boss.Gateway.Application.Features.Branches
{
    public class GetBranchListQueryHandler : IRequestHandler<GetBranchListQuery, List<BranchListVm>>


    {

        private readonly IBranchRepository _branchRepository;
        private readonly IMapper _mapper;

        public GetBranchListQueryHandler (IMapper mapper, IBranchRepository branchRepository){
            _branchRepository = branchRepository;
            _mapper = mapper;
        }
        public Task<List<BranchListVm>> Handle(GetBranchListQuery request, CancellationToken cancellationToken)

      
        {
              throw new NotImplementedException();
        //    var branchList =await _branchRepository.ListAllAsync();
        //    return _mapper.Map<List<BranchListVm>>(branchList);
        }
    }
}