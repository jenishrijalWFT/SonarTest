using AutoMapper;
using Boss.Gateway.Application.Contracts.Persistence;
using MediatR;

namespace Boss.Gateway.Application.Features.Branches
{

    public class GetBranchDetailQueryHandler : IRequestHandler<GetBranchDetailQuery, BranchDetailVm>
    {

        private readonly IBranchRepository _branchRepository;
        private readonly IMapper _mapper;

        public GetBranchDetailQueryHandler(IBranchRepository branchRepository, IMapper mapper)
        {
            _branchRepository = branchRepository;
            _mapper = mapper;

        }

        public  Task<BranchDetailVm> Handle(GetBranchDetailQuery request, CancellationToken cancellationToken)
        {
            // var branchDetail = await _branchRepository.GetByIdAsync(request.Id);
            // ToDo
            throw new NotImplementedException();
            // return _mapper.Map<BranchDetailVm>(branchDetail);
        }
    }
}