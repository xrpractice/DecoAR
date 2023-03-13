using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public partial class TrackedImageActionManager : MonoBehaviour
{
    [SerializeField] private int rangeBetweenMultiInstancesInMeters;
    [SerializeField] private List<ImageAction> imagesWithAction;
    private GameObject _instancedPrefab;
    private const string IMAGE_PREFAB_PATH = "Prefabs/ImagePrefab";
    private const string VIDEO_PREFAB_PATH = "Prefabs/VideoPrefab";
    private const string AUDIO_PREFAB_PATH = "Prefabs/AudioPrefab";

    private void Start()
    {
        _instancedPrefab = new GameObject();
    }

    private void OnEnable()
    {
        _instancedPrefab = new GameObject();
        StartCoroutine(InstancePrefab());
    }

    private GameObject LoadImage(string sourcePath)
    {
        var imagePrefab = Resources.Load<GameObject>(IMAGE_PREFAB_PATH);
        var image = Resources.Load<Texture>(sourcePath);
        var imageComponent = imagePrefab.GetComponentInChildren<RawImage>();
        
        imageComponent.texture = image;
        return imagePrefab;
    }

    private GameObject LoadAudio(string sourcePath)
    {
        var audioPrefab = Resources.Load<GameObject>(AUDIO_PREFAB_PATH);
        var audioClip = Resources.Load<AudioClip>(sourcePath);
        var audioSourceComponent = audioPrefab.GetComponentInChildren<AudioSource>();
        
        audioSourceComponent.clip = audioClip;
        return audioPrefab;
    }
    
    private GameObject LoadVideo(string sourcePath)
    {
        var videoPrefab = Resources.Load<GameObject>(VIDEO_PREFAB_PATH);
        var videoClip = Resources.Load<VideoClip>(sourcePath);
        var videoSourceComponent = videoPrefab.GetComponentInChildren<VideoPlayer>();
        
        videoSourceComponent.clip = videoClip;
        return videoPrefab;
    }

    private GameObject GetPrefabFromActions(String imageName)
    {
        var actionEntry = imagesWithAction.Find(image => image.imageName.Equals(imageName));
        var action = actionEntry.action;
        var sourcePath = actionEntry.sourcePath;

        return action switch
        {
            Action.Audio => LoadAudio(sourcePath),
            Action.Image => LoadImage(sourcePath),
            Action.Video => LoadVideo(sourcePath),
            _ => Resources.Load<GameObject>(sourcePath)
        };
    }

    private Quaternion GetViewAngle()
    {
        // Opposite of the camera view to instance the prefab
        return Camera.main.transform.rotation * Quaternion.AngleAxis(180, Vector3.up);
    }

    private bool IsImageTrackable(string imageName)
    {
        var isImageTracked = ImageTracker.Instance.IsImageTracked(imageName);
        var isImageRangeIsClose = ImageTracker.Instance.IsImageRangeIsClose(imageName, rangeBetweenMultiInstancesInMeters);

        return isImageTracked && isImageRangeIsClose;
    }
    
    private void InstancePrefab(string imageName, Vector3 imagePosition)
    {
        ImageTracker.Instance.TrackedImages.Add(new ImageDetails(imageName, imagePosition));
        var actionPrefab = GetPrefabFromActions(imageName);
        _instancedPrefab = Instantiate(actionPrefab, gameObject.transform.position, GetViewAngle());
        _instancedPrefab.transform.parent = gameObject.transform;
    }
    
    private IEnumerator InstancePrefab()
    {
        yield return new WaitForSeconds(1.5f);
        
        var imageName = ImageTracker.Instance.CurrentImage;
        var imagePosition = ImageTracker.Instance.CurrentImageLocation;
        if (IsImageTrackable(imageName)) yield break;
        InstancePrefab(imageName, imagePosition);
    }
}
