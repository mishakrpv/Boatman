﻿using Boatman.DataAccess.Interfaces;
using Boatman.DataAccess.Interfaces.Specifications;
using Boatman.Entities.Models.ApartmentAggregate;
using Boatman.Entities.Models.FavoritesAggregate;
using Boatman.Utils.Response;
using MediatR;

namespace Boatman.FrontendApi.UseCases.Commands.AddToFavorites;

public class AddToFavoritesRequestHandler : IRequestHandler<AddToFavoritesRequest, Response>
{
    private readonly IRepository<Favorites> _favoritesRepo;
    private readonly IRepository<Apartment> _apartmentRepo;

    public AddToFavoritesRequestHandler(IRepository<Favorites> favoritesRepo,
        IRepository<Apartment> apartmentRepo)
    {
        _favoritesRepo = favoritesRepo;
        _apartmentRepo = apartmentRepo;
    }

    public async Task<Response> Handle(AddToFavoritesRequest request, CancellationToken cancellationToken)
    {
        var spec = new CustomersFavoritesSpecification(request.CustomerId);
        var wishlist = await _favoritesRepo.FirstOrDefaultAsync(spec, cancellationToken)
                       ?? await _favoritesRepo.AddAsync(new Favorites(request.CustomerId), cancellationToken);

        var apartment = await _apartmentRepo.GetByIdAsync(request.ApartmentId, cancellationToken);

        if (apartment == null)
            return new Response
            {
                StatusCode = 404,
                Message = "Apartment not found."
            };
        
        wishlist.AddItem(request.ApartmentId);

        await _favoritesRepo.UpdateAsync(wishlist, cancellationToken);

        return new Response
        {
            Message = "Apartment has been successfully added to the wishlist."
        };
    }
}