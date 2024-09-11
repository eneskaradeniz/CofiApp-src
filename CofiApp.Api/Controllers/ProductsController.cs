using CofiApp.Api.Contracts;
using CofiApp.Api.Infrastructure;
using CofiApp.Application.Products.Commands.CreateProduct;
using CofiApp.Application.Products.Commands.RemoveProduct;
using CofiApp.Application.Products.Commands.UpdateProduct;
using CofiApp.Application.Products.Commands.UpdateProductMenuCategories;
using CofiApp.Application.Products.Queries.GetProductById;
using CofiApp.Application.Products.Queries.GetProducts;
using CofiApp.Application.Products.Queries.PublicGetProductById;
using CofiApp.Contracts.Common;
using CofiApp.Contracts.Products;
using CofiApp.Domain.Core.Errors;
using CofiApp.Domain.Core.Primitives.Maybe;
using CofiApp.Domain.Core.Primitives.Result;
using CofiApp.Domain.Enums;
using CofiApp.Infrastructure.Authentication;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CofiApp.Api.Controllers
{
    public class ProductsController : ApiController
    {
        public ProductsController(IMediator mediator) : base(mediator)
        {
        }

        [HasPermission(Permission.GetProducts)]
        [HttpGet(ApiRoutes.Products.Get)]
        [ProducesResponseType(typeof(PagedList<ProductResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int page, int pageSize) =>
            await Maybe<GetProductsQuery>
                .From(new GetProductsQuery(page, pageSize))
                .Bind(query => Mediator.Send(query))
                .Match(Ok, NotFound);

        [HasPermission(Permission.GetProductById)]
        [HttpGet(ApiRoutes.Products.GetById)]
        [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid productId) =>
            await Maybe<GetProductByIdQuery>
                .From(new GetProductByIdQuery(productId))
                .Bind(query => Mediator.Send(query))
                .Match(Ok, NotFound);

        [HasPermission(Permission.CreateProduct)]
        [HttpPost(ApiRoutes.Products.Create)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateProductRequest createProductRequest) =>
            await Result.Create(createProductRequest, DomainErrors.General.UnProcessableRequest)
                .Map(request => new CreateProductCommand(request.Name, request.Description, request.Price))
                .Bind(command => Mediator.Send(command))
                .Match(Created, BadRequest);

        [HasPermission(Permission.UpdateProduct)]
        [HttpPut(ApiRoutes.Products.Update)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(Guid productId, [FromBody] UpdateProductRequest updateProductRequest) =>
            await Result.Create(updateProductRequest, DomainErrors.General.UnProcessableRequest)
                .Map(request => new UpdateProductCommand(productId, request.Name, request.Description, request.Price))
                .Bind(command => Mediator.Send(command))
                .Match(NoContent, BadRequest);

        [HasPermission(Permission.RemoveProduct)]
        [HttpDelete(ApiRoutes.Products.Remove)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Remove(Guid productId) =>
            await Result.Success(new RemoveProductCommand(productId))
                .Bind(command => Mediator.Send(command))
                .Match(Ok, BadRequest);

        [HasPermission(Permission.UpdateProductMenuCategories)]
        [HttpPut(ApiRoutes.Products.UpdateProductMenuCategories)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateProductMenuCategories(Guid productId, [FromBody] UpdateProductMenuCategoriesRequest updateProductMenuCategoriesRequest) =>
            await Result.Create(updateProductMenuCategoriesRequest, DomainErrors.General.UnProcessableRequest)
                .Map(request => new UpdateProductMenuCategoriesCommand(productId, request.MenuCategoryIds))
                .Bind(command => Mediator.Send(command))
                .Match(NoContent, BadRequest);

        [AllowAnonymous]
        [HttpGet(ApiRoutes.Products.PublicGetById)]
        [ProducesResponseType(typeof(PublicProductWithOptionsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PublicGetById(Guid productId) =>
            await Maybe<PublicGetProductByIdQuery>
                .From(new PublicGetProductByIdQuery(productId))
                .Bind(query => Mediator.Send(query))
                .Match(Ok, NotFound);
    }
}
