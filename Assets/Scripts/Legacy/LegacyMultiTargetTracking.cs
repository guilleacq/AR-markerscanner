using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class LegacyMultiTargetTracking : MonoBehaviour
{
    [SerializeField] private ARTrackedImageManager imageManager;
    [SerializeField] private GameObject[] modelsToPlace;

    private Dictionary<string, GameObject> models = new Dictionary<string, GameObject>();   
    private Dictionary<string, bool> modelState = new Dictionary<string, bool>(); //Whether a gameobject is currently active or not

    private void Start()
    {
        foreach (var model in modelsToPlace)
        {
            GameObject currentModel = Instantiate(model, Vector3.zero, Quaternion.identity);
            currentModel.name = model.name;
            models.Add(currentModel.name, currentModel);
            currentModel.SetActive(false);
            modelState.Add(currentModel.name, false); 
        }
    }

    private void OnEnable()
    {
        imageManager.trackedImagesChanged += ImageFound;
    }

    private void OnDisable()
    {
        imageManager.trackedImagesChanged -= ImageFound;
    }

    private void ImageFound(ARTrackedImagesChangedEventArgs eventData)
    {
        foreach (var trackedImage in eventData.added)
        {
            ShowARModel(trackedImage);
        }
        foreach (var trackedImage in eventData.updated)
        {
            if (trackedImage.trackingState == TrackingState.Tracking) //If it is being tracked
            {
                ShowARModel(trackedImage);
            }
            else if (trackedImage.trackingState == TrackingState.Limited)
            {
                HideARModel(trackedImage);
            }
        }
    }

    private void ShowARModel(ARTrackedImage trackedImage)
    {
        bool isActive = modelState[trackedImage.referenceImage.name];

        if (!isActive)
        {
            GameObject currentModel = models[trackedImage.referenceImage.name];
            currentModel.transform.position = trackedImage.transform.position;
            currentModel.transform.rotation = trackedImage.transform.rotation;
            currentModel.SetActive(true);
            modelState[trackedImage.referenceImage.name] = true;
        }
        else
        {
            GameObject currentModel = models[trackedImage.referenceImage.name];
            currentModel.transform.position = trackedImage.transform.position;
            currentModel.transform.rotation = trackedImage.transform.rotation;
        }


    }
    private void HideARModel(ARTrackedImage trackedImage)
    {
        bool isActive = modelState[trackedImage.referenceImage.name];

        if (isActive)
        {
            GameObject currentModel = models[trackedImage.referenceImage.name];
            currentModel.SetActive(false);
            modelState[trackedImage.referenceImage.name] = false;
        }
    }
}
