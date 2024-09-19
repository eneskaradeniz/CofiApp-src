using CofiApp.Api.Contracts;
using CofiApp.Api.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CofiApp.Api.Controllers
{
    public class FilesController : ApiController
    {
        private readonly IConfiguration _configuration;

        public FilesController(IMediator mediator, IConfiguration configuration) : base(mediator)
        {
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpGet(ApiRoutes.Public.Files.GetBaseStorageUrl)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetBaseStorageUrl() =>
            Ok(_configuration["StorageBaseUrl"]);
    }
}
