using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class MultipleImageTracking : MonoBehaviour
{
    [SerializeField] private ImageModel[] modelsToPlace;
    private Dictionary<string, ImageModel> models = new Dictionary<string, ImageModel>();

    [SerializeField] private ARTrackedImageManager imageManager; 

    void Awake()
    {
        foreach (var prefab in modelsToPlace) // Creates a game object for every AR placeable model
        {
            ImageModel currentModel = Instantiate(prefab.gameObject, Vector3.zero, Quaternion.identity).GetComponent<ImageModel>();
            currentModel.InitializeName(prefab.name);
            models.Add(currentModel.ImageName, currentModel);
        }
    }
    private void OnEnable() => imageManager.trackedImagesChanged += ImageFound;
    private void OnDisable() => imageManager.trackedImagesChanged -= ImageFound;

    private void ImageFound(ARTrackedImagesChangedEventArgs eventData)
    {
        foreach (var trackedImage in eventData.added)
        {
            UpdateImage(trackedImage);
        }
        foreach (var trackedImage in eventData.updated)
        {
            UpdateImage(trackedImage);
        }
        foreach (var trackedImage in eventData.removed)
        {
            HideImage(trackedImage);
        }
    }

    private void HideImage(ARTrackedImage trackedImage)
    {
        var currentModel = GetModelOf(trackedImage);
        currentModel.DisableVisibility();
    }

    private void UpdateImage(ARTrackedImage trackedImage)
    {
        var currentModel = GetModelOf(trackedImage);
        currentModel.UpdateTrackedImage(trackedImage);
    }

    private ImageModel GetModelOf(ARTrackedImage trackedImage) => models[trackedImage.referenceImage.name];



}
