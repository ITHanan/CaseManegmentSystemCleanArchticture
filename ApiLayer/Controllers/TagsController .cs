using ApplicationLayer.Features.Tags.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TagsController : ControllerBase
{
    private readonly IMediator _mediator;

    public TagsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("Get-All-Tags")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllTagsQuery());
        return Ok(result);
    }

    [HttpGet("{id}/Get-Tags-By-TagID")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(new GetTagByIdQuery(id));
        return Ok(result);
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpPost("Creat-Tags")]
    public async Task<IActionResult> Create(CreateTagDto dto)
    {
        var result = await _mediator.Send(new CreateTagCommand(dto.Name));
        return Ok(result);
    }

    [HttpPut("{id}/Update-Tags")]
    public async Task<IActionResult> Update(int id, UpdateTagDto dto)
    {
        var result = await _mediator.Send(new UpdateTagCommand(id, dto.Name));
        return Ok(result);
    }


    [Authorize(Policy = "AdminOnly")]
    [HttpDelete("{id}/Delete-Tags")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _mediator.Send(new DeleteTagCommand(id));
        return Ok(result);
    }
}
