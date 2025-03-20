using System;
using Contracting.Domain.Abstractions;
using Contracting.Domain.Contracts;
using Contracting.Domain.Contracts.Exceptions;
using Contracting.Domain.Delivery;
using MediatR;

namespace Contracting.Application.Contracts.CreateContract;

public class CreateContractHandler : IRequestHandler<CreateContractCommand, Guid>
{
    private readonly IContractFactory _contractFactory;
    private readonly IContractRepository _contractRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateContractHandler(IContractFactory contractFactory, IContractRepository contractRepository, IUnitOfWork unitOfWork)
    {
        _contractFactory = contractFactory;
        _contractRepository = contractRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateContractCommand request, CancellationToken cancellationToken)
    {
        var contract = request.Type switch
        {
            "HalfMonth" => _contractFactory.CreateHalfMonthContract(request.AdministratorId, request.PatientId, request.StartDate),
            "FullMonth" => _contractFactory.CreateFullMonthContract(request.AdministratorId, request.PatientId, request.StartDate)
        };

        List<DeliveryDay> deliveryDays = new List<DeliveryDay>();
        foreach (var days in request.Days)
        {
            int span;
            if (request.Type == "HalfMonth")
            {
                span = 14;
            }
            else
            { 
                span = 29;
            }
            for (int i = 0; i <= span; i++)
            {
                DeliveryDay d = new DeliveryDay(contract.Id, days.Start.AddDays(i), days.Street, days.Number, days.Longitude, days.Latitude);
                deliveryDays.Add(d);
            }
        }
        contract.CreateCalendar(deliveryDays);

        await _contractRepository.AddSync(contract);
        await _unitOfWork.CommitAsync(cancellationToken);
        return contract.Id;
    }
}