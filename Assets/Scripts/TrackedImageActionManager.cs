using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using Object = UnityEngine.Object;

public partial class TrackedImageActionManager : MonoBehaviour
{
    [SerializeField] private int distance;
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
        Debug.Log("Sai : Prefab instantiated");
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

        if (action == Action.Audio)
        {
            return LoadAudio(sourcePath);
        }
        
        if (action == Action.Image)
        {
            return LoadImage(sourcePath);
        }

        if (action == Action.Video)
        {
            return LoadVideo(sourcePath);
        }

        return Resources.Load<GameObject>(sourcePath);
    }

    private Quaternion GetViewAngle()
    {
        // Opposite of the camera view to instance the prefab
        return Camera.main.transform.rotation * Quaternion.AngleAxis(180, Vector3.up);
    }

    private IEnumerator InstancePrefab()
    {
        yield return new WaitForSeconds(1.5f);
        
        var imageName = ImageTracker.Instance.CurrentImage;
        var imagePosition = ImageTracker.Instance.CurrentImageLocation;
        // if (ImageTracker.Instance.IsImageTracked(imageName)) yield break;
        
        Debug.Log("Sai : " + imageName + " " + imagePosition);
        
        var isImageTracked = ImageTracker.Instance.IsImageTracked(imageName);
        var isImageRangeIsClose = ImageTracker.Instance.isImageRangeIsClose(imageName, distance);
        Debug.Log("Sai : isImageTracked : " + isImageTracked + " isImageRangeClose : " + isImageRangeIsClose);
        if (isImageTracked && isImageRangeIsClose)
        {
            yield break;
        }
        
        ImageTracker.Instance.TrackedImages.Add(new ImageDetails(imageName, imagePosition));
        var actionPrefab = GetPrefabFromActions(imageName);
        
        // instantiating prefab
        _instancedPrefab = Instantiate(actionPrefab, gameObject.transform.position, GetViewAngle());
        _instancedPrefab.transform.parent = gameObject.transform;
    }
}
