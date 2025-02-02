using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracting.Domain.Abstractions;
using Contracting.Domain.Contracts;
using Contracting.Domain.Contracts.Exceptions;
using Contracting.Domain.Delivery;
using MediatR;

namespace Contracting.Application.Contracts.CreateContract;

internal class CreateContractHandler : IRequestHandler<CreateContractCommand, Guid>
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
            "FullMonth" => _contractFactory.CreateFullMonthContract(request.AdministratorId, request.PatientId, request.StartDate),
            _ => throw new ContractCreationException("Invalid contract type")
        };

        List<DeliveryDay> deliveryDays = new List<DeliveryDay>();
        TimeSpan timespan = request.Days.Last().Ends - request.Days.First().Start;
        if (request.Type == "HalfMonth" && timespan.Days == 14 || request.Type == "FullMonth" && timespan.Days == 29)
        {
            foreach (var days in request.Days)
            {
                timespan = days.Ends - days.Start;
                for (int i = 0; i <= timespan.Days; i++)
                {
                    DeliveryDay d = new DeliveryDay(contract.Id, days.Start.AddDays(i), days.Street, days.Number, days.Longitude, days.Latitude);
                    deliveryDays.Add(d);
                }
            }
        } 
        else
        {
            throw new ArgumentException("If type is half month the days needs to be 15 and if type is FullMonth needs to be 30");
        }
        contract.CreateCalendar(deliveryDays);

        await _contractRepository.AddSync(contract);
        await _unitOfWork.CommitAsync(cancellationToken);
        return contract.Id;
    }
}