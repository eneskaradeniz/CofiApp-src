using CofiApp.Application.Abstractions.Data;
using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Domain.Authentication;
using CofiApp.Domain.Core.Errors;
using CofiApp.Domain.Core.Primitives.Result;
using CofiApp.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace CofiApp.Application.Roles.Commands.AssignUser
{
    public class AssignUserCommandHandler : ICommandHandler<AssignUserCommand, Result>
    {
        private readonly IDbContext _dbContext;
        private readonly IUnitOfWork _unitOfWork;

        public AssignUserCommandHandler(IDbContext dbContext, IUnitOfWork unitOfWork)
        {
            _dbContext = dbContext;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(AssignUserCommand request, CancellationToken cancellationToken)
        {
            Role? role = await _dbContext.Set<Role>()
                .Where(x => x.Id == request.RoleId)
                .AsNoTracking()
                .SingleOrDefaultAsync();

            if (role is null)
            {
                return Result.Failure(DomainErrors.General.NotFound);
            }

            User? user = await _dbContext.Set<User>()
                .Include(x => x.Roles)
                .Where(x => x.Id == request.UserId)
                .SingleOrDefaultAsync();

            if (user is null)
            {
                return Result.Failure(DomainErrors.General.NotFound);
            }

            Role? userTargetRole = user.Roles.SingleOrDefault(x => x.Id == role.Id);

            if (userTargetRole is null)
            {
                user.Roles.Add(role);
            }
            else
            {
                user.Roles.Remove(userTargetRole);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
