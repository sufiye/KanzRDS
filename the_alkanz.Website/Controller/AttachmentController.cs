using Microsoft.AspNetCore.Mvc;
using the_alkanz.Website.DTOs;
using the_alkanz.Website.Services;

namespace the_alkanz.Website.Controller;

[Route("api/[controller]")]
[ApiController] 
public class AttachmentController : ControllerBase
{
    private readonly IAttachmentService _service;

    public AttachmentController(IAttachmentService service)
    {
        _service = service;
    }

    [HttpPost("{productId:guid}")]
    public async Task<IActionResult> Upload(
        Guid productId,
        [FromForm] UploadAttachmentRequest request)
    {
        if (request.File == null || request.File.Length == 0)
            return BadRequest("File is empty");

        var result = await _service.UploadAsync(productId, request.File);
        return Ok(result);
    }


    [HttpGet("{productId:guid}")]
    public async Task<IActionResult> Get(Guid productId)
    {
        var result = await _service.GetByProductIdAsync(productId);
        return Ok(result);
    }


    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
}