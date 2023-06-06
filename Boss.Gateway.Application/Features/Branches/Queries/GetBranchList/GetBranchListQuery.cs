using MediatR;

namespace Boss.Gateway.Application.Features.Branches {
    public class GetBranchListQuery  : IRequest<List<BranchListVm>>{}
}