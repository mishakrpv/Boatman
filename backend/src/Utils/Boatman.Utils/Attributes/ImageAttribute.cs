using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Http;

namespace Boatman.Utils.Attributes;

[AttributeUsage(AttributeTargets.Parameter)]
public class ImageAttribute : ValidationAttribute
{
    private const int ImageMaximumBytes = 512000000;
    
    public override bool IsValid(object? value)
    {
        var file = (IFormFile)value!;
        
        return file.Length is > 0 and <= ImageMaximumBytes && IsExtensionValid(file.FileName);
    }

    private static bool IsExtensionValid(string fileName)
    {
        var extension = Path.GetExtension(fileName);

        return string.Equals(extension, ".jpg", StringComparison.OrdinalIgnoreCase) ||
               string.Equals(extension, ".png", StringComparison.OrdinalIgnoreCase) ||
               string.Equals(extension, ".gif", StringComparison.OrdinalIgnoreCase) ||
               string.Equals(extension, ".jpeg", StringComparison.OrdinalIgnoreCase);
    }
}