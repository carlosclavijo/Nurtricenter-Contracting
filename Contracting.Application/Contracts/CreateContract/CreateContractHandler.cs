using Contracting.Domain.Abstractions;
using Contracting.Domain.Contracts;
using Contracting.Domain.Delivery;
using Joseco.DDD.Core.Results;
using MediatR;

namespace Contracting.Application.Contracts.CreateContract;

public class CreateContractHandler(IContractFactory ContractFactory, IContractRepository ContractRepository, IUnitOfWork UnitOfWork) : IRequestHandler<CreateContractCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateContractCommand request, CancellationToken cancellationToken)
    {
        var contract = request.Type switch
		{
			"HalfMonth" => ContractFactory.CreateHalfMonthContract(request.AdministratorId, request.PatientId, request.StartDate),
			"FullMonth" => ContractFactory.CreateFullMonthContract(request.AdministratorId, request.PatientId, request.StartDate),
			_ => throw new NotImplementedException()
		};

        List<DeliveryDay> deliveryDays = [];
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
                DeliveryDay d = new(contract.Id, days.Start.AddDays(i), days.Street, days.Number, days.Longitude, days.Latitude);
                deliveryDays.Add(d);
            }
        }
        contract.CreateCalendar(deliveryDays);

        await ContractRepository.AddSync(contract);
        await UnitOfWork.CommitAsync(cancellationToken);
        return contract.Id;
    }
}