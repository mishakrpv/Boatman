using Boatman.DataAccess.Domain.Interfaces;
using Boatman.DataAccess.Domain.Interfaces.Specifications;
using Boatman.Entities.Models.ApartmentAggregate;
using MediatR;

namespace Boatman.OwnerApi.UseCases.Commands.MyApartments;

public class MyApartmentsRequestHandler : IRequestHandler<MyApartmentsRequest, IEnumerable<Apartment>>
{
    private readonly IRepository<Apartment> _apartmentRepo;

    public MyApartmentsRequestHandler(IRepository<Apartment> apartmentRepo)
    {
        _apartmentRepo = apartmentRepo;
    }
    
    public async Task<IEnumerable<Apartment>> Handle(MyApartmentsRequest request, CancellationToken cancellationToken)
    {
        var spec = new OwnersApartmentSpecification(request.OwnerId);
        var apartments = await _apartmentRepo.ListAsync(spec, cancellationToken);

        return apartments.OrderBy(a => a.PublicationDate);
    }
}