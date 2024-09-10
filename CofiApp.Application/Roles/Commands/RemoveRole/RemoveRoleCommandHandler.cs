using CofiApp.Application.Abstractions.Data;
using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Domain.Authentication;
using CofiApp.Domain.Core.Errors;
using CofiApp.Domain.Core.Primitives.Result;

namespace CofiApp.Application.Roles.Commands.RemoveRole
{
    public class RemoveRoleCommandHandler : ICommandHandler<RemoveRoleCommand, Result>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveRoleCommandHandler(IRoleRepository roleRepository, IUnitOfWork unitOfWork)
        {
            _roleRepository = roleRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(RemoveRoleCommand request, CancellationToken cancellationToken)
        {
            Role role = await _roleRepository.GetByIdAsync(request.RoleId);
            if (role is null)
            {
                return Result.Failure(DomainErrors.General.NotFound);
            }

            _roleRepository.Remove(role);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
