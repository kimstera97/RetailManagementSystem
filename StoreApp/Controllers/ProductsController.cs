using MediatR;
using Microsoft.AspNetCore.Mvc;
using StoreApp.Handlers.Products.Create;
using System.Net;

namespace StoreApp.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController(ISender sender) : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateProductRequest request,
        CancellationToken cancellationToken)
    {
        return StatusCode((int)HttpStatusCode.Created, await sender.Send(request, cancellationToken));
    }
}
