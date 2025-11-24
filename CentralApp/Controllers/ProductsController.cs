using CentralApp.Handlers.Create;
using CentralApp.Handlers.ReceiveFromStore;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RetailManagement.Shared.Dtos;
using RetailManagement.Shared.Models;

namespace CentralApp.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController(ISender sender) : ControllerBase
{
    [HttpPost("receive")]
    public async Task<IActionResult> ReceiveSync([FromBody] SyncPayload payload)
    {
        await sender.Send(new ReceiveFromStoreRequest(payload));
        return Ok();
    }

    [HttpPost("create-for-store/{storeId}")]
    public async Task<IActionResult> CreateForSpecificStore(string storeId, [FromBody] ProductDto dto)
    {
        await sender.Send(new CreateProductRequest(storeId, dto));
        return Accepted();
    }
}
