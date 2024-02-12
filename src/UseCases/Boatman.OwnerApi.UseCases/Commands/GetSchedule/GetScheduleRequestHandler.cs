using Ardalis.GuardClauses;
using Boatman.DataAccess.Domain.Interfaces;
using Boatman.DataAccess.Domain.Interfaces.Specifications;
using Boatman.Entities.Models.ApartmentAggregate;
using MediatR;

namespace Boatman.OwnerApi.UseCases.Commands.GetSchedule;

public class GetScheduleRequestHandler : IRequestHandler<GetScheduleRequest, IEnumerable<Viewing>>
{
    private readonly IRepository<Apartment> _apartmentRepo;

    public GetScheduleRequestHandler(IRepository<Apartment> apartmentRepo)
    {
        _apartmentRepo = apartmentRepo;
    }
    
    public async Task<IEnumerable<Viewing>> Handle(GetScheduleRequest request, CancellationToken cancellationToken)
    {
        var spec = new ApartmentWithScheduleSpecification(request.ApartmentId);
        var apartment = await _apartmentRepo.FirstOrDefaultAsync(spec, cancellationToken);
        Guard.Against.Null(apartment);
        Guard.Against.Null(apartment.Schedule);

        return apartment.Schedule;
    }
}