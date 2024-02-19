using Boatman.Utils.Response;
using MediatR;

namespace Boatman.CustomerApi.UseCases.Commands.SubmitAnApplication;

public class SubmitAnApplicationRequest : IRequest<Response>
{
    public SubmitAnApplicationRequest(int apartmentId, string customerId)
    {
        ApartmentId = apartmentId;
        CustomerId = customerId;
    }

    public int ApartmentId { get; private set; }
    public string CustomerId { get; private set; }
}