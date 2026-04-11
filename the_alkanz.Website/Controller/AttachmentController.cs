using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using the_alkanz.Website.DTOs;
using the_alkanz.Website.Services;

namespace the_alkanz.Website.Controller;

[Route("api/[controller]")]
[ApiController]
public class AttachmentController : ControllerBase
{
    private readonly IAttachmentService _attachmentService;
    private readonly IProductService _productService;
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

    /// <summary>
    /// Uploads a file attachment for a specific product.
    /// </summary>
    /// <param name="productId">The ID of the product to attach the file to.</param>
    /// <param name="file">The file to upload.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>The uploaded attachment information.</returns>
    /// <response code="200">Returns the uploaded attachment info.</response>
    /// <response code="400">If no file is provided.</response>
    /// <response code="404">If the product or attachment could not be found.</response>
  
        [HttpPost("/api/products/{productId:guid}/attachments")]
        public async Task<ActionResult<AttechmentResponseDto>> Upload(
            Guid productId,
           IFormFile file,
            CancellationToken cancellationToken)
        {
            var product = await _productService.GetByIdAsync(productId);
            if (product is null)
                return NotFound();

            if (file is null || file.Length == 0)
                return BadRequest("File required");

            await using var stream = file.OpenReadStream();

            var result = await _attachmentService.UploadAsync(
                productId,
                stream,
                file.FileName,
                file.ContentType,
                file.Length,
                cancellationToken
            );

            if (result is null)
                return NotFound();

            return Ok(result);
        }
    

    /// <summary>
    /// Downloads an attachment by its ID.
    /// </summary>
    /// <param name="id">The ID of the attachment to download.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>The file content for download.</returns>
    /// <response code="200">Returns the requested file.</response>
    /// <response code="404">If the attachment could not be found.</response>
    [HttpGet("{id:guid}/download")]
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

    /// <summary>
    /// Deletes an attachment by its ID.
    /// </summary>
    /// <param name="id">The ID of the attachment to delete.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>No content if the deletion was successful.</returns>
    /// <response code="204">Attachment successfully deleted.</response>
    /// <response code="404">If the attachment could not be found.</response>
    [HttpDelete("{id:guid}")]
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