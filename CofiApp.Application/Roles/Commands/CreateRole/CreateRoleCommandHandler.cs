using CofiApp.Application.Abstractions.Data;
using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Domain.Authentication;
using CofiApp.Domain.Core.Errors;
using CofiApp.Domain.Core.Primitives.Result;

namespace CofiApp.Application.Roles.Commands.CreateRole
{
    public class CreateRoleCommandHandler : ICommandHandler<CreateRoleCommand, Result>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateRoleCommandHandler(IRoleRepository roleRepository, IUnitOfWork unitOfWork)
        {
            _roleRepository = roleRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            if (!await _roleRepository.IsNameUniqueAsync(request.Name, cancellationToken))
            {
                return Result.Failure(DomainErrors.Authentication.DuplicateName);
            }

            Role role = new()
            {
                Id = Guid.NewGuid(),
                Name = request.Name
            };

            _roleRepository.Insert(role);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
