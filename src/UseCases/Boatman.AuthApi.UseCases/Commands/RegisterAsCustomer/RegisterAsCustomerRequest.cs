using Boatman.AuthApi.UseCases.Dtos;
using Boatman.Utils;
using Boatman.Utils.Response;
using MediatR;

namespace Boatman.AuthApi.UseCases.Commands.RegisterAsCustomer;

public class RegisterAsCustomerRequest : IRequest<Response>
{
    public RegisterAsCustomerRequest(RegisterAsCustomerDto dto)
    {
        Dto = dto;
    }

    public RegisterAsCustomerDto Dto { get; private set; }
}