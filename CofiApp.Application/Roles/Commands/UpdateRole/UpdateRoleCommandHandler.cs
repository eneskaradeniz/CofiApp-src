using CofiApp.Application.Abstractions.Data;
using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Domain.Authentication;
using CofiApp.Domain.Core.Errors;
using CofiApp.Domain.Core.Primitives.Result;

namespace CofiApp.Application.Roles.Commands.UpdateRole
{
    public class UpdateRoleCommandHandler : ICommandHandler<UpdateRoleCommand, Result>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateRoleCommandHandler(IRoleRepository roleRepository, IUnitOfWork unitOfWork)
        {
            _roleRepository = roleRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            Role role = await _roleRepository.GetByIdAsync(request.RoleId, cancellationToken);

            if (role is null)
            {
                return Result.Failure(DomainErrors.General.NotFound);
            }

            role.Name = request.Name;

            _roleRepository.Update(role);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
