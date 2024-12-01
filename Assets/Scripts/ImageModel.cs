using System;
using UnityEngine;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;

public class ImageModel : MonoBehaviour
{
    private const float TIME_TO_HIDE = 2; // Amount of time a model waits to hide when not being seen by camera

    [SerializeField] private DecorationController decorationController;

    [field: Tooltip("This property must be equal to the image name. If left empty, its value will be 'this.gameobject.name'")]
    [field: SerializeField] public string ImageName { get; private set; }

    private Camera cam;

    private float hideTimer = 0f;
    private bool hasBeenTrackedOnce = false;

    public event Action OnFirstTracked;

    private void Start()
    {
        if (decorationController == null)
            decorationController = GetComponentInChildren<DecorationController>();

        cam = Camera.main;
    }

    public void PlaceInPosition(Transform refObj)
    {
        this.transform.position = refObj.position;
        this.transform.rotation = refObj.rotation;
    }

    private void Update()
    {
        if (hasBeenTrackedOnce)
        {
            if (CanBeSeenByCamera())
            {
                hideTimer = 0;
            }
            else
            {
                hideTimer += Time.deltaTime;

                if (hideTimer > TIME_TO_HIDE)
                    DisableVisibility();
            }
        }
    }

    public void UpdateTrackedImage(ARTrackedImage trackedImage)
    {
        if (trackedImage.trackingState == TrackingState.Tracking)
        {
            PlaceInPosition(trackedImage.transform);
            decorationController.EnableVisibility();

            if (!hasBeenTrackedOnce)
            {
                hasBeenTrackedOnce = true;
                OnFirstTracked?.Invoke();
            }
            hideTimer = 0f;
        }
    }
    private bool CanBeSeenByCamera()
    {
        var bounds = GetComponent<BoxCollider>().bounds;
        var cameraFrustum = GeometryUtility.CalculateFrustumPlanes(cam);

        return GeometryUtility.TestPlanesAABB(cameraFrustum, bounds);
    }
    public void InitializeName(string name)
    {
        gameObject.name = name;

        if (string.IsNullOrEmpty(ImageName))
        {
            Debug.LogWarning("MISSING IMAGENAME FOR PREFAB: " + gameObject.name);
            ImageName = name;
        }
    }
    public void DisableVisibility() => decorationController.DisableVisibility();
}
