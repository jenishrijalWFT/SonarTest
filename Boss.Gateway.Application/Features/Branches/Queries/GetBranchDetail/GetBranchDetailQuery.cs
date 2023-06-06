using MediatR;

namespace Boss.Gateway.Application.Features.Branches {

    public class GetBranchDetailQuery : IRequest<BranchDetailVm> {
        
        public Guid Id;

      
    }
}