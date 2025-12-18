using EvergreenWebAPI.DTOs;
using EvergreenWebAPI.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace EvergreenWebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FlowerController : ControllerBase
{
    private readonly ILogger<FlowerController> _logger;
    private readonly IFlowerService _flowerService;
    private readonly IFlowerMlClient _ml;
    private readonly IDropboxService _dropboxService;

    public FlowerController(ILogger<FlowerController> logger, IFlowerService flowerService, IFlowerMlClient ml, IDropboxService dropboxService)
    {
        _logger = logger;
        _flowerService = flowerService;
        _ml = ml;
        _dropboxService = dropboxService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<FlowerInfoDTO?>> GetFlowerInfoByIdAsync(Guid id)
    {
        try
        {
            var flowerInfo = await _flowerService.GetFlowerInfoByIdAsync(id);

            if(flowerInfo != null)
            {
                string[] fileInfoArray = flowerInfo.ImagePath.Split('/', StringSplitOptions.RemoveEmptyEntries);
                string folder = fileInfoArray[0], file = fileInfoArray[1];

                flowerInfo.ImagePath = await _dropboxService.GetTemporaryLinkAsync(folder, file);
            }

            return flowerInfo;
        }
        catch(Exception exception)
        {
            _logger.LogError(exception.Message);
            return BadRequest();
        }
    }

    [HttpPost("predict")]
    public async Task<IActionResult> Predict(IFormFile file, CancellationToken ct)
    {
        if (file is null || file.Length == 0)
            return BadRequest("File is required");

        await using var stream = file.OpenReadStream();

        var result = await _ml.PredictAsync(
            imageStream: stream,
            fileName: file.FileName,
            contentType: file.ContentType ?? "application/octet-stream",
            ct: ct);

        return Ok(result);
    }
}