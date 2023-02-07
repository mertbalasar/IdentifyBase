using IdentifyBase.Application.Abstractions.Services;
using IdentifyBase.Domain.Features.Commands.User;
using IdentifyBase.Domain.Features.Responses;
using IdentifyBase.Domain.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentifyBase.Presentation.Controllers
{
    [Authorize(Roles = "standart")]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet, Route("")]
        public async Task<IActionResult> GetProducts()
        {
            var response = await _productService.GetProductsAsync();

            if (response.Succeed)
            {
                return Ok();
            }
            else
            {
                return BadRequest(response.Message);
            }
        }
    }
}
