using ApplicationLayer.Features.Cases.Commands.CreatCases;
using ApplicationLayer.Features.Cases.Commands.DeleteCase;
using ApplicationLayer.Features.Cases.Commands.UpdateCase;
using ApplicationLayer.Features.Cases.Queries.GetAllCases;
using ApplicationLayer.Features.Cases.Queries.GetCaseById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CasesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CasesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCaseCommand command, CancellationToken token)
    {
        var result = await _mediator.Send(command, token);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllCasesQuery());
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(new GetCaseByIdQuery(id));
        return result.IsSuccess ? Ok(result) : NotFound(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateCaseCommand command, CancellationToken token)
    {
        if (id != command.Id)
            return BadRequest("Id mismatch");

        var result = await _mediator.Send(command, token);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _mediator.Send(new DeleteCaseCommand(id));
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
}
