using ApplicationLayer.Features.CaseNotes.Commands.CreateNoteCommand;
using ApplicationLayer.Features.CaseNotes.Commands.DeleteNote;
using ApplicationLayer.Features.CaseNotes.Commands.UpdateCaseNoteCommand;
using ApplicationLayer.Features.CaseNotes.Dtos;
using ApplicationLayer.Features.CaseNotes.Queries.GetNotesByCaseId;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace ApiLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaseNotesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CaseNotesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("{caseId}/notes")]
        public async Task<IActionResult> AddNote(int caseId, [FromBody] CreateNoteDto dto)
        {
            var command = new CreateCaseNoteCommand(caseId, dto.Content);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("{caseId}/notes")]
        public async Task<IActionResult> GetNotes(int caseId)
        {
            var result = await _mediator.Send(new GetNotesByCaseIdQuery(caseId));
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(int caseId, CreateNoteDto dto)
        {
            var command = new CreateCaseNoteCommand(caseId, dto.Content);
            var result = await _mediator.Send(command);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPut("{noteId}")]
        public async Task<IActionResult> Update(int noteId, UpdateNoteDto dto)
        {
            var command = new UpdateCaseNoteCommand(noteId, dto.Content);
            var result = await _mediator.Send(command);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("{noteId}")]
        public async Task<IActionResult> Delete(int noteId)
        {
            var command = new DeleteCaseNoteCommand(noteId);
            var result = await _mediator.Send(command);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
