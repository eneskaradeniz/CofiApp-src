using CofiApp.Api.Contracts;
using CofiApp.Api.Infrastructure;
using CofiApp.Application.ProductOptions.Commands.CreateProductOption;
using CofiApp.Application.ProductOptions.Commands.UpdateProductOption;
using CofiApp.Application.Products.Commands.RemoveProduct;
using CofiApp.Contracts.ProductOptions;
using CofiApp.Domain.Core.Errors;
using CofiApp.Domain.Core.Primitives.Result;
using CofiApp.Domain.Enums;
using CofiApp.Infrastructure.Authentication;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CofiApp.Api.Controllers
{
    public class ProductOptionController : ApiController
    {
        public ProductOptionController(IMediator mediator) : base(mediator)
        {
        }

        [HasPermission(Permission.CreateProductOption)]
        [HttpPost(ApiRoutes.ProductOptions.Create)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(Guid productOptionGroupId, [FromBody] CreateProductOptionRequest createProductOptionRequest) =>
            await Result.Create(createProductOptionRequest, DomainErrors.General.UnProcessableRequest)
                .Map(request => new CreateProductOptionCommand(productOptionGroupId, request.Name, request.Price))
                .Bind(command => Mediator.Send(command))
                .Match(Created, BadRequest);

        [HasPermission(Permission.UpdateProductOption)]
        [HttpPut(ApiRoutes.ProductOptions.Update)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(Guid productOptionId, [FromBody] UpdateProductOptionRequest updateProductOptionRequest) =>
            await Result.Create(updateProductOptionRequest, DomainErrors.General.UnProcessableRequest)
                .Map(request => new UpdateProductOptionCommand(productOptionId, request.Name, request.Price))
                .Bind(command => Mediator.Send(command))
                .Match(NoContent, BadRequest);

        [HasPermission(Permission.RemoveProductOption)]
        [HttpDelete(ApiRoutes.ProductOptions.Remove)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Remove(Guid productOptionId) =>
            await Result.Success(new RemoveProductCommand(productOptionId))
                .Bind(command => Mediator.Send(command))
                .Match(Ok, BadRequest);
    }
}
