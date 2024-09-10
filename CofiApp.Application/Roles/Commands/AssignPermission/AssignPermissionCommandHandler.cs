using CofiApp.Application.Abstractions.Data;
using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Domain.Authentication;
using CofiApp.Domain.Core.Errors;
using CofiApp.Domain.Core.Primitives.Result;
using Microsoft.EntityFrameworkCore;

namespace CofiApp.Application.Roles.Commands.AssignPermission
{
    public class AssignPermissionCommandHandler : ICommandHandler<AssignPermissionCommand, Result>
    {
        private readonly IDbContext _dbContext;
        private readonly IUnitOfWork _unitOfWork;

        public AssignPermissionCommandHandler(IDbContext dbContext, IUnitOfWork unitOfWork)
        {
            _dbContext = dbContext;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(AssignPermissionCommand request, CancellationToken cancellationToken)
        {
            Role? role = await _dbContext.Set<Role>()
                .Include(x => x.Permissions)
                .Where(x => x.Id == request.RoleId)
                .SingleOrDefaultAsync();

            if (role is null)
            {
                return Result.Failure(DomainErrors.General.NotFound);
            }

            if (!Enum.IsDefined(typeof(Domain.Enums.Permission), request.PermissionId))
            {
                return Result.Failure(DomainErrors.General.NotFound);
            }

            Domain.Enums.Permission targetPermission = (Domain.Enums.Permission)request.PermissionId;

            if (role.Permissions.Any(x => x.Id == (int)targetPermission))
            {
                role.Permissions.Remove(role.Permissions.Single(x => x.Id == (int)targetPermission));
            }
            else
            {
                role.Permissions.Add(new Permission()
                {
                    Id = (int)targetPermission,
                    Name = targetPermission.ToString()
                });
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
