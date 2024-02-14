using Boatman.DataAccess.Domain.Interfaces;
using Boatman.DataAccess.Domain.Interfaces.Specifications;
using Boatman.Entities.Models.ApartmentAggregate;
using Boatman.Utils;
using MediatR;

namespace Boatman.OwnerApi.UseCases.Commands.GetSchedule;

public class GetScheduleRequestHandler : IRequestHandler<GetScheduleRequest, Response<IEnumerable<Viewing>>>
{
    private readonly IRepository<Apartment> _apartmentRepo;

    public GetScheduleRequestHandler(IRepository<Apartment> apartmentRepo)
    {
        _apartmentRepo = apartmentRepo;
    }

    public async Task<Response<IEnumerable<Viewing>>> Handle(GetScheduleRequest request,
        CancellationToken cancellationToken)
    {
        var spec = new ApartmentWithScheduleSpecification(request.ApartmentId);
        var apartment = await _apartmentRepo.FirstOrDefaultAsync(spec, cancellationToken);

        if (apartment == null)
            return new Response<IEnumerable<Viewing>>
            {
                StatusCode = 404,
                Message = "Apartment not found."
            };

        return new Response<IEnumerable<Viewing>>(apartment.Schedule);
    }
}