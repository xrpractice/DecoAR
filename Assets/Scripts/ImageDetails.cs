using System.Numerics;

public class ImageDetails
{
    public readonly string name;
    public readonly UnityEngine.Vector3 position;

    public ImageDetails(string imageName, UnityEngine.Vector3 imagePosition)
    {
        name = imageName;
        position = imagePosition;
    }
}
