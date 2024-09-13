using CofiApp.Api.Contracts;
using CofiApp.Api.Infrastructure;
using CofiApp.Application.Baskets.Commands.ClearBasket;
using CofiApp.Application.Baskets.Commands.CreateBasketItem;
using CofiApp.Application.Baskets.Commands.UpdateBasketItem;
using CofiApp.Application.Baskets.Commands.UpdateBasketItemQuantity;
using CofiApp.Application.Baskets.Queries.GetActiveBasketByUser;
using CofiApp.Contracts.Baskets;
using CofiApp.Domain.Core.Errors;
using CofiApp.Domain.Core.Primitives.Maybe;
using CofiApp.Domain.Core.Primitives.Result;
using CofiApp.Domain.Enums;
using CofiApp.Infrastructure.Authentication;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CofiApp.Api.Controllers
{
    public class BasketController : ApiController
    {
        public BasketController(IMediator mediator) : base(mediator)
        {
        }

        [HasPermission(Permission.GetActiveBasketByUser)]
        [HttpGet(ApiRoutes.Baskets.GetActiveBasket)]
        [ProducesResponseType(typeof(BasketResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetActiveBasket() =>
            await Maybe<GetActiveBasketByUserQuery>
                .From(new GetActiveBasketByUserQuery())
                .Bind(query => Mediator.Send(query))
                .Match(Ok, NotFound);

        [HasPermission(Permission.CreateBasketItem)]
        [HttpPost(ApiRoutes.Baskets.CreateBasketItem)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateBasketItem([FromBody] CreateBasketItemRequest createBasketItemRequest) =>
           await Result.Create(createBasketItemRequest, DomainErrors.General.UnProcessableRequest)
                .Map(request => new CreateBasketItemCommand(request.ProductId, request.ProductOptions, request.Quantity))
                .Bind(command => Mediator.Send(command))
                .Match(Created, BadRequest);

        [HasPermission(Permission.UpdateBasketItem)]
        [HttpPut(ApiRoutes.Baskets.UpdateBasketItem)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateBasketItem(Guid basketItemId, [FromBody] UpdateBasketItemRequest updateBasketItemRequest) => await Result.Create(updateBasketItemRequest, DomainErrors.General.UnProcessableRequest)
                .Map(request => new UpdateBasketItemCommand(basketItemId, request.ProductOptions, request.Quantity))
                .Bind(command => Mediator.Send(command))
                .Match(NoContent, BadRequest);

        [HasPermission(Permission.UpdateBasketItemQuantity)]
        [HttpPut(ApiRoutes.Baskets.UpdateBasketItemQuantity)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateBasketItemQuantity(Guid basketItemId, [FromBody] UpdateBasketItemQuantityRequest updateBasketItemQuantityRequest) =>
            await Result.Create(updateBasketItemQuantityRequest, DomainErrors.General.UnProcessableRequest)
                .Map(request => new UpdateBasketItemQuantityCommand(basketItemId, updateBasketItemQuantityRequest.IsIncrease))
                .Bind(command => Mediator.Send(command))
                .Match(NoContent, BadRequest);

        [HasPermission(Permission.ClearBasket)]
        [HttpDelete(ApiRoutes.Baskets.Clear)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ClearBasket() =>
            await Result.Success(new ClearBasketCommand())
                .Bind(command => Mediator.Send(command))
                .Match(Ok, BadRequest);
    }
}
