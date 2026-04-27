using Microsoft.AspNetCore.Mvc;
using the_alkanz.Website.Services;

[ApiController]
[Route("api")]
public class AttachmentController : ControllerBase
{
    private readonly IAttachmentService _service;

    public AttachmentController(IAttachmentService service)
    {
        _service = service;
    }

    [HttpPost("products/{productId:guid}/attachments")]
    public async Task<IActionResult> Upload(
        Guid productId,
        [FromForm] IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("File is empty");

        var result = await _service.UploadAsync(productId, file);
        return Ok(result);
    }


    [HttpGet("products/{productId:guid}/attachments")]
    public async Task<IActionResult> Get(Guid productId)
    {
        var result = await _service.GetByProductIdAsync(productId);
        return Ok(result);
    }


    [HttpDelete("attachments/{id:guid}")]
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