using Contracting.Domain.Abstractions;
using Contracting.Domain.Administrators;
using Joseco.DDD.Core.Results;
using MediatR;

namespace Contracting.Application.Administrators.CreateAdministrator;

public class CreateAdministratorHandler(IAdministratorFactory AdministratorFactory, IAdministratorRepository AdministratorRepository, IUnitOfWork UnitOfWork) : IRequestHandler<CreateAdministratorCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateAdministratorCommand request, CancellationToken cancellationToken)
    {
        var administrator = AdministratorFactory.Create(request.AdministratorName, request.AdministratorPhone);
        await AdministratorRepository.AddSync(administrator);
        await UnitOfWork.CommitAsync(cancellationToken);
        return administrator.Id;
    }
}