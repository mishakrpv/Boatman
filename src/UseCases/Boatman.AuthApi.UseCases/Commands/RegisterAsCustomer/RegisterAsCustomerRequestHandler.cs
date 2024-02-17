using Boatman.DataAccess.Domain.Interfaces;
using Boatman.DataAccess.Identity.Interfaces;
using Boatman.DataAccess.Identity.Interfaces.Dtos;
using Boatman.Entities.Models.CustomerAggregate;
using Boatman.Entities.Models.OwnerAggregate;
using Boatman.Utils;
using Boatman.Utils.Response;
using MediatR;

namespace Boatman.AuthApi.UseCases.Commands.RegisterAsCustomer;

public class RegisterAsCustomerRequestHandler : IRequestHandler<RegisterAsCustomerRequest, Response>
{
    private readonly IUserService _userService;
    private readonly IRepository<Customer> _customerRepo;

    public RegisterAsCustomerRequestHandler(IUserService userService, IRepository<Customer> customerRepo)
    {
        _userService = userService;
        _customerRepo = customerRepo;
    }

    public async Task<Response> Handle(RegisterAsCustomerRequest request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;
        var response = await _userService.RegisterUserAsync(new RegisterAsDto(nameof(Customer))
        {
            Email = dto.Email,
            Password = dto.Password,
            ConfirmPassword = dto.ConfirmPassword
        });

        if (response.StatusCode != 200)
            return response;

        var owner = await _customerRepo.AddAsync(new Customer(response.Value!)
        {
            FirstName = dto.FirstName,
            MiddleName = dto.MiddleName,
            LastName = dto.LastName,
            Bio = dto.Bio
        }, default);

        return response;
    }
}