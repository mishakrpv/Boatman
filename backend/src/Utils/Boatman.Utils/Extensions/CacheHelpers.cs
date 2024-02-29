namespace Boatman.Utils.Extensions;

public static class CacheHelpers
{
    public static string GenerateApartmentCacheKey(int apartmentId)
    {
        return $"apartment_{apartmentId}";
    }
    
    public static string GenerateOwnersApartmentsCacheKey(string ownerId)
    {
        return $"apartments_{ownerId}";
    }

    public static string GenerateFavoritesCacheKey(string customerId)
    {
        return $"favorites_{customerId}";
    }
}