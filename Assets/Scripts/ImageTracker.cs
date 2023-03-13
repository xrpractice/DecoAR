using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class ImageTracker
{
    private ImageTracker()
    {
        
    }
    
    public static readonly ImageTracker Instance = new();
    public string CurrentImage { get; set; } = "null";
    public Vector2 CurrentImageSize { get; set; } = Vector2.zero;
    public List<ImageDetails> TrackedImages { get; } = new();
    public Vector3 CurrentImageLocation { get; set; }

    private List<ImageDetails> getImageDetails(string imageName)
    {
        Debug.Log("Sai : getImageDetails");
        return TrackedImages.FindAll(details => details.name.Equals(imageName));
    }
    
    public bool IsImageTracked(string imageName)
    {
        Debug.Log("Sai : isImageTracked");
        return TrackedImages.Any(ImageDetails => ImageDetails.name.Equals(imageName));
    }

    public bool isImageRangeIsClose(string imageName, int distance)
    {
        var allImages = getImageDetails(imageName);
        Debug.Log("Sai : " + allImages);
        return allImages.TrueForAll(details => Match(details, distance));
    }

    private bool Match(ImageDetails imageDetails, int distance)
    {
        var distanceBetween = Vector3.Distance(imageDetails.position, CurrentImageLocation);
        Debug.Log("Sai : distance " + distanceBetween);
        return distanceBetween < distance;
    }
}