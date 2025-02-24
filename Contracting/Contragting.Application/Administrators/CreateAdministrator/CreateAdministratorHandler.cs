using System;
using Contracting.Domain.Abstractions;
using Contracting.Domain.Administrators;
using MediatR;

namespace Contracting.Application.Administrators.CreateAdministrator;

public class CreateAdministratorHandler : IRequestHandler<CreateAdministratorCommand, Guid>
{
    private readonly IAdministratorFactory _administratorFactory;
    private readonly IAdministratorRepository _administratorRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateAdministratorHandler(IAdministratorFactory administratorFactory,
        IAdministratorRepository administratorRepository,
        IUnitOfWork unitOfWork)
    {
        _administratorFactory = administratorFactory;
        _administratorRepository = administratorRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateAdministratorCommand request, CancellationToken cancellationToken)
    {
        var administrator = _administratorFactory.Create(request.AdministratorName, request.AdministratorPhone);
        await _administratorRepository.AddSync(administrator);
        await _unitOfWork.CommitAsync(cancellationToken);
        return administrator.Id;
    }
}