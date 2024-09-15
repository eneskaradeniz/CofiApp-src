using CofiApp.Api.Contracts;
using CofiApp.Api.Infrastructure;
using CofiApp.Application.MenuCategories.Commands.CreateMenuCategory;
using CofiApp.Application.MenuCategories.Commands.RemoveMenuCategory;
using CofiApp.Application.MenuCategories.Commands.UpdateMenuCategory;
using CofiApp.Application.MenuCategories.Queries.GetMenuCategories;
using CofiApp.Application.MenuCategories.Queries.GetMenuCategoryById;
using CofiApp.Application.MenuCategories.Queries.PublicGetMenuCategories;
using CofiApp.Application.MenuCategories.Queries.PublicGetMenuCategoryByIdWithProducts;
using CofiApp.Contracts.Common;
using CofiApp.Contracts.MenuCategories;
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
    public class MenuCategoriesController : ApiController
    {
        public MenuCategoriesController(IMediator mediator) : base(mediator)
        {
        }

        [HasPermission(Permission.GetMenuCategories)]
        [HttpGet(ApiRoutes.Shop.MenuCategories.Get)]
        [ProducesResponseType(typeof(PagedList<MenuCategoryResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int page, int pageSize) =>
            await Maybe<GetMenuCategoriesQuery>
                .From(new GetMenuCategoriesQuery(page, pageSize))
                .Bind(query => Mediator.Send(query))
                .Match(Ok, NotFound);

        [HasPermission(Permission.GetMenuCategoryById)]
        [HttpGet(ApiRoutes.Shop.MenuCategories.GetById)]
        [ProducesResponseType(typeof(MenuCategoryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid menuCategoryId) =>
            await Maybe<GetMenuCategoryByIdQuery>
                .From(new GetMenuCategoryByIdQuery(menuCategoryId))
                .Bind(query => Mediator.Send(query))
                .Match(Ok, NotFound);

        [HasPermission(Permission.CreateMenuCategory)]
        [HttpPost(ApiRoutes.Shop.MenuCategories.Create)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateMenuCategoryRequest createMenuCategoryRequest) =>
            await Result.Create(createMenuCategoryRequest, DomainErrors.General.UnProcessableRequest)
                .Map(request => new CreateMenuCategoryCommand(request.Name))
                .Bind(command => Mediator.Send(command))
                .Match(Created, BadRequest);

        [HasPermission(Permission.UpdateMenuCategory)]
        [HttpPut(ApiRoutes.Shop.MenuCategories.Update)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(Guid menuCategoryId, [FromBody] UpdateMenuCategoryRequest updateMenuCategoryRequest) =>
            await Result.Create(updateMenuCategoryRequest, DomainErrors.General.UnProcessableRequest)
                .Map(request => new UpdateMenuCategoryCommand(menuCategoryId, request.Name))
                .Bind(command => Mediator.Send(command))
                .Match(Ok, BadRequest);

        [HasPermission(Permission.RemoveMenuCategory)]
        [HttpDelete(ApiRoutes.Shop.MenuCategories.Remove)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Remove(Guid menuCategoryId) =>
            await Result.Success(new RemoveMenuCategoryCommand(menuCategoryId))
                .Bind(command => Mediator.Send(command))
                .Match(Ok, BadRequest);

        [AllowAnonymous]
        [HttpGet(ApiRoutes.Public.MenuCategories.Get)]
        [ProducesResponseType(typeof(PagedList<PublicMenuCategoryResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PublicGet() =>
            await Maybe<PublicGetMenuCategoriesQuery>
                .From(new PublicGetMenuCategoriesQuery())
                .Bind(query => Mediator.Send(query))
                .Match(Ok, NotFound);
        
        [AllowAnonymous]
        [HttpGet(ApiRoutes.Public.MenuCategories.GetByIdWithProducts)]
        [ProducesResponseType(typeof(PagedList<MenuCategoryWithProductsResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMenuCategoryByIdWithProducts(Guid menuCategoryId, int page, int pageSize) =>
            await Maybe<GetMenuCategoryByIdWithProductsQuery>
                .From(new GetMenuCategoryByIdWithProductsQuery(menuCategoryId, page, pageSize))
                .Bind(query => Mediator.Send(query))
                .Match(Ok, NotFound);
    }
}
