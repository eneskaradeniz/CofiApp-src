using CofiApp.Api.Contracts;
using CofiApp.Api.Infrastructure;
using CofiApp.Application.ProductOptionGroups.Commands.CreateProductOptionGroup;
using CofiApp.Application.ProductOptionGroups.Commands.RemoveProductOptionGroup;
using CofiApp.Application.ProductOptionGroups.Commands.UpdateProductOptionGroup;
using CofiApp.Application.ProductOptionGroups.Queries.GetProductOptionGroupsWithOptions;
using CofiApp.Contracts.ProductOptionGroups;
using CofiApp.Domain.Core.Errors;
using CofiApp.Domain.Core.Primitives.Maybe;
using CofiApp.Domain.Core.Primitives.Result;
using CofiApp.Domain.Enums;
using CofiApp.Infrastructure.Authentication;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CofiApp.Api.Controllers
{
    public class ProductOptionGroupsController : ApiController
    {
        public ProductOptionGroupsController(IMediator mediator) : base(mediator)
        {
        }

        [HasPermission(Permission.GetProductOptionGroupsWithOptions)]
        [HttpGet(ApiRoutes.ProductOptionGroups.GetWithOptions)]
        [ProducesResponseType(typeof(List<ProductOptionGroupsWithOptionsResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetWithOptions(Guid productId) =>
            await Maybe<GetProductOptionGroupsWithOptionsQuery>
                .From(new GetProductOptionGroupsWithOptionsQuery(productId))
                .Bind(query => Mediator.Send(query))
                .Match(Ok, NotFound);

        [HasPermission(Permission.CreateProductOptionGroup)]
        [HttpPost(ApiRoutes.ProductOptionGroups.Create)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(Guid productId, [FromBody] CreateProductOptionGroupRequest createProductOptionGroupRequest) =>
            await Result.Create(createProductOptionGroupRequest, DomainErrors.General.UnProcessableRequest)
                .Map(request => new CreateProductOptionGroupCommand(productId, request.Name, request.IsRequired, request.AllowMultiple))
                .Bind(command => Mediator.Send(command))
                .Match(Created, BadRequest);

        [HasPermission(Permission.UpdateProductOptionGroup)]
        [HttpPut(ApiRoutes.ProductOptionGroups.Update)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(Guid productOptionGroupId, [FromBody] UpdateProductOptionGroupRequest updateProductOptionGroupRequest) =>
            await Result.Create(updateProductOptionGroupRequest, DomainErrors.General.UnProcessableRequest)
                .Map(request => new UpdateProductOptionGroupCommand(productOptionGroupId, request.Name, request.IsRequired, request.AllowMultiple))
                .Bind(command => Mediator.Send(command))
                .Match(NoContent, BadRequest);

        [HasPermission(Permission.RemoveProductOptionGroup)]
        [HttpDelete(ApiRoutes.ProductOptionGroups.Remove)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Remove(Guid productOptionGroupId) =>
            await Result.Success(new RemoveProductOptionGroupCommand(productOptionGroupId))
                .Bind(command => Mediator.Send(command))
                .Match(Ok, BadRequest);
    }
}
