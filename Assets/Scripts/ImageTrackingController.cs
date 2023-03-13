using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ImageTrackingController : MonoBehaviour
{
    [SerializeField] private ARTrackedImageManager arTrackedImageManager;

    public void OnEnable() {
        arTrackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    public void OnDisable() {
        arTrackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs args) {
        foreach (var trackedImage in args.added)
        {
            Debug.Log("Sai : Image Tracked " + trackedImage.referenceImage.name);
            ImageTracker.Instance.CurrentImage = trackedImage.referenceImage.name;
            ImageTracker.Instance.CurrentImageSize = trackedImage.referenceImage.size;
            ImageTracker.Instance.CurrentImageLocation = trackedImage.transform.position;
            trackedImage.destroyOnRemoval = false;
        }
    }
}