using Boatman.Utils.Response;
using MediatR;

namespace Boatman.FrontendApi.UseCases.Commands.SubmitRequest;

public class SubmitRequestRequest : IRequest<Response>
{
    public SubmitRequestRequest(int apartmentId, string customerId)
    {
        ApartmentId = apartmentId;
        CustomerId = customerId;
    }

    public int ApartmentId { get; private set; }
    public string CustomerId { get; private set; }
}