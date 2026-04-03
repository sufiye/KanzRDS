using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Security.Claims;
using the_alkanz.Website.DTOs;
using the_alkanz.Website.Services;

namespace the_alkanz.Website.Controller;

[Route("api/[controller]")]
[ApiController]
public class AttachmentController : ControllerBase
{
    private readonly IAttachmentService _attachmentService;
    private readonly IProductService  _productService;
    private readonly IAuthorizationService _authorizationService;

    private string? UserId => User.FindFirstValue(ClaimTypes.NameIdentifier);

    public AttachmentController(
        IAttachmentService attachmentService,
        IProductService taskItemService,
        IAuthorizationService authorizationService)
    {
        _attachmentService = attachmentService;
        _productService = taskItemService;
        _authorizationService = authorizationService;
    }

    [HttpPost("~/api/products/{productId}/attachments")]
    public async Task<ActionResult<AttechmentResponseDto>> Upload(
        Guid productId,
        IFormFile file,
        CancellationToken cancellationToken
        )
    {
        var product = await _productService.GetByIdAsync(productId);
        if (product is null)
            return NotFound();

        if (file is null || file.Length == 0)
            return BadRequest("File is required");

        AttechmentResponseDto? attachment;
        await using var stream = file.OpenReadStream();
        attachment = await _attachmentService.UploadAsync(
            productId,
            stream,
            file.FileName,
            file.ContentType,
            file.Length,
            UserId!,
            cancellationToken
            );

        if (attachment is null)
            return NotFound();

        return Ok(attachment);
    }

    [HttpGet("{id}/download")]
    public async Task<IActionResult> Download(Guid id, CancellationToken cancellationToken)
    {
        var info = await _attachmentService.GetAttachmentInfoAsync(id, cancellationToken);

        if (info is null)
            return NotFound();

        var result = await _attachmentService.GetDownloadAsync(id, cancellationToken);

        if (result is null)
            return NotFound();

        return File(result.Value.stream, result.Value.contentType, result.Value.fileName);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var info = await _attachmentService.GetAttachmentInfoAsync(id, cancellationToken);

        if (info is null)
            return NotFound();

        var deleted = await _attachmentService.DeleteAsync(id, cancellationToken);
        if (!deleted)
            return NotFound();

        return NoContent();
    }
}
