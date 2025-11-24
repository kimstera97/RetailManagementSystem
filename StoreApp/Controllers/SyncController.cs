using MediatR;
using Microsoft.AspNetCore.Mvc;
using RetailManagement.Shared.Dtos;
using StoreApp.Handlers.Sync.ReceiveFromCentral;

namespace StoreApp.Controllers;

[ApiController]
[Route("api/sync")]
public class SyncController(ISender sender) : ControllerBase
{
    [HttpPost("receive")]
    public async Task<IActionResult> ReceiveFromCentral([FromBody] ProductDto dto)
    {
        await sender.Send(new ReceiveFromCentralRequest(dto));
        return Ok();
    }
}
