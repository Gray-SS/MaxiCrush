using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MaxiCrush.Api.Controllers;

[ApiController]
public abstract class CQRSController : ControllerBase
{
    protected IMapper Mapper => _mapper;
    protected ISender Mediator => _mediator;

    private readonly IMapper _mapper;
    private readonly ISender _mediator;

    public CQRSController(ISender mediator, IMapper mapper)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    protected async Task<TResponse> Send<TResponse>(IRequest<TResponse> request)
    {
        return await _mediator.Send(request);
    }
}
