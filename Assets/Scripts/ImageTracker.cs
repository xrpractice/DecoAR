using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ImageTracker
{
    private ImageTracker() { }
    
    public static readonly ImageTracker Instance = new();
    public string CurrentImage { get; set; } = "null";
    public Vector2 CurrentImageSize { get; set; } = Vector2.zero;
    public List<ImageDetails> TrackedImages { get; } = new();
    public Vector3 CurrentImageLocation { get; set; }

    public bool IsImageTracked(string imageName) =>
         TrackedImages.Any(imageDetails => imageDetails.name.Equals(imageName));
    
    private List<ImageDetails> GetImageDetails(string imageName) =>
         TrackedImages.FindAll(details => details.name.Equals(imageName));

    public bool IsImageRangeIsClose(string imageName, int distance)
    {
        var allImages = GetImageDetails(imageName);
        return allImages.TrueForAll(details => Match(details, distance));
    }

    private bool Match(ImageDetails imageDetails, int expectedRange)
    {
        var distanceBetween = Vector3.Distance(imageDetails.position, CurrentImageLocation);
        return distanceBetween < expectedRange;
    }
}