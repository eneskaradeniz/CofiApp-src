﻿using CofiApp.Api.Contracts;
using CofiApp.Api.Infrastructure;
using CofiApp.Application.Orders.Commands.CancelCustomerOrder;
using CofiApp.Application.Orders.Commands.CreateCustomerOrder;
using CofiApp.Application.Orders.Commands.UpdateShopOrderStatus;
using CofiApp.Application.Orders.Queries.GetCustomerOrderById;
using CofiApp.Application.Orders.Queries.GetCustomerOrders;
using CofiApp.Application.Orders.Queries.GetShopOrderById;
using CofiApp.Application.Orders.Queries.GetShopOrders;
using CofiApp.Contracts.Common;
using CofiApp.Contracts.Orders;
using CofiApp.Domain.Core.Errors;
using CofiApp.Domain.Core.Primitives.Maybe;
using CofiApp.Domain.Core.Primitives.Result;
using CofiApp.Domain.Enums;
using CofiApp.Infrastructure.Authentication;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CofiApp.Api.Controllers
{
    public class OrdersController : ApiController
    {
        public OrdersController(IMediator mediator) : base(mediator)
        {
        }

        [HasPermission(Permission.GetShopOrders)]
        [HttpGet(ApiRoutes.Shop.Orders.Get)]
        [ProducesResponseType(typeof(PagedList<OrderResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetShopOrders(int page, int pageSize) =>
            await Maybe<GetShopOrdersQuery>
                .From(new GetShopOrdersQuery(page, pageSize))
                .Bind(query => Mediator.Send(query))
                .Match(Ok, NotFound);

        [HasPermission(Permission.GetShopOrderById)]
        [HttpGet(ApiRoutes.Shop.Orders.GetById)]
        [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetShopOrderById(Guid orderId) =>
            await Maybe<GetShopOrderByIdQuery>
                .From(new GetShopOrderByIdQuery(orderId))
                .Bind(query => Mediator.Send(query))
                .Match(Ok, NotFound);

        [HasPermission(Permission.UpdateShopOrderStatus)]
        [HttpPatch(ApiRoutes.Shop.Orders.UpdateStatus)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateShopOrderStatus(Guid orderId, [FromBody] UpdateShopOrderStatusRequest updateShopOrderStatusRequest) =>
            await Result.Create(updateShopOrderStatusRequest, DomainErrors.General.UnProcessableRequest)
                .Map(request => new UpdateShopOrderStatusCommand(orderId, request.Status))
                .Bind(command => Mediator.Send(command))
                .Match(NoContent, BadRequest);

        [HasPermission(Permission.GetCustomerOrders)]
        [HttpGet(ApiRoutes.Customer.Orders.Get)]
        [ProducesResponseType(typeof(PagedList<OrderResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCustomerOrders(int page, int pageSize) =>
            await Maybe<GetCustomerOrdersQuery>
                .From(new GetCustomerOrdersQuery(page, pageSize))
                .Bind(query => Mediator.Send(query))
                .Match(Ok, NotFound);

        [HasPermission(Permission.GetCustomerOrderById)]
        [HttpGet(ApiRoutes.Customer.Orders.GetById)]
        [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCustomerOrderById(Guid orderId) =>
            await Maybe<GetCustomerOrderByIdQuery>
                .From(new GetCustomerOrderByIdQuery(orderId))
                .Bind(query => Mediator.Send(query))
                .Match(Ok, NotFound);

        [HasPermission(Permission.CreateCustomerOrder)]
        [HttpPost(ApiRoutes.Customer.Orders.Create)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateCustomerOrder() =>
            await Result.Success(new CreateCustomerOrderCommand())
                .Bind(command => Mediator.Send(command))
                .Match(NoContent, BadRequest);

        [HasPermission(Permission.CancelCustomerOrder)]
        [HttpPost(ApiRoutes.Customer.Orders.Cancel)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CancelCustomerOrder(Guid orderId) =>
            await Result.Success(new CancelCustomerOrderCommand(orderId))
                .Bind(command => Mediator.Send(command))
                .Match(NoContent, BadRequest);
    }
}
