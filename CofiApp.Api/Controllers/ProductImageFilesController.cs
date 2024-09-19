using CofiApp.Api.Contracts;
using CofiApp.Api.Infrastructure;
using CofiApp.Application.ProductImageFiles.Commands.RemoveProductImageFile;
using CofiApp.Application.ProductImageFiles.Commands.UploadProductImageFile;
using CofiApp.Domain.Core.Primitives.Result;
using CofiApp.Domain.Enums;
using CofiApp.Infrastructure.Authentication;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CofiApp.Api.Controllers
{
    public class ProductImageFilesController : ApiController
    {
        public ProductImageFilesController(IMediator mediator) : base(mediator)
        {
        }

        [HasPermission(Permission.UploadProductImageFile)]
        [HttpPost(ApiRoutes.Shop.ProductImageFiles.Upload)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UploadProductImageFile(Guid productId, IFormFile imageFile) =>
            await Result.Success(new UploadProductImageFileCommand(productId, imageFile))
                .Bind(command => Mediator.Send(command))
                .Match(NoContent, BadRequest);

        [HasPermission(Permission.RemoveProductImageFile)]
        [HttpDelete(ApiRoutes.Shop.ProductImageFiles.Remove)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RemoveProductImageFile(Guid productId) =>
            await Result.Success(new RemoveProductImageFileCommand(productId))
                .Bind(command => Mediator.Send(command))
                .Match(NoContent, BadRequest);
    }
}
