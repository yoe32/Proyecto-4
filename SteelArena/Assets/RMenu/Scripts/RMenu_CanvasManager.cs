using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

[ExecuteInEditMode]
public class RMenu_CanvasManager : MonoBehaviour
{
    public Camera guiCamera;

    [System.NonSerialized]
    public Transform cameraTransform;

    [Range(1, 100)]
    public float canvasDistance = 10f;

    [Range(100, 10000)]
    public float pixelsForSmallSide = 1000f;

    [System.NonSerialized]
    public Canvas[] menuCanvasses;

    [System.NonSerialized]
    public RectTransform[] menuRectTransforms = new RectTransform[4];

    [System.NonSerialized]
    public GraphicRaycaster[] menuSlotRaycasters = new GraphicRaycaster[4];

    private readonly List<Vector3> directionArray = new List<Vector3>()
        {
            Vector3.forward,
            Vector3.right,
            Vector3.back,
            Vector3.left
        };

    private Transform _transform = null;
    public new Transform transform
    {
        get
        {
            if (this._transform == null)
                this._transform = GetComponent<Transform>();

            return this._transform;
        }
    }

    private float lastDistance;
    private float lastPixels;
    private float lastAspect;
    private bool lastProjectionMode;
    private float lastFOV;
    private float lastOrthSize;

    private void Awake()
    {
		
        // try to find the RTC camera, if it isn't assigned already
        if (guiCamera == null)
            guiCamera = GetComponentInParent<Camera>();

        // require camera
        if (guiCamera == null)
        {
            Debug.Log("RMenu_CanvasManager requires a Camera to function properly.");

            gameObject.SetActive(false);
            return;
        }

        // cache the camera's transform
        cameraTransform = guiCamera.transform;

        Canvas[] getCanvasses = GetComponentsInChildren<Canvas>();
        if (getCanvasses.Length < 4)
        {
            Debug.Log("RMenu_CanvasManager doesn't have enough child canvasses to function properly. Needs 4.");

            gameObject.SetActive(false);
            return;
        }
        menuCanvasses = getCanvasses.Take(4).ToArray();

        for (int i = 0; i < menuCanvasses.Length; i++)
        {
            menuCanvasses[i].renderMode = RenderMode.WorldSpace;
            menuCanvasses[i].worldCamera = guiCamera;

            menuRectTransforms[i] = menuCanvasses[i].GetComponent<RectTransform>();
            menuSlotRaycasters[i] = menuCanvasses[i].GetComponent<GraphicRaycaster>();
        }

        //set our cached values to current
        lastDistance = canvasDistance;
        lastPixels = pixelsForSmallSide;
        lastAspect = guiCamera.aspect;
        lastProjectionMode = guiCamera.orthographic;
        lastFOV = guiCamera.fieldOfView;
        lastOrthSize = guiCamera.orthographicSize;
    }

    private void Update()
    {
		
        //parenting the position (and only the position) to the camera
        transform.position = cameraTransform.position;

        //check if anything important changed
        if (canvasDistance != lastDistance ||
            lastPixels != pixelsForSmallSide ||
            guiCamera.aspect != lastAspect ||
            guiCamera.orthographic != lastProjectionMode ||
             guiCamera.orthographicSize != lastOrthSize ||
             guiCamera.fieldOfView != lastFOV)
        {
            //update our cached values
            lastDistance = canvasDistance;
            lastPixels = pixelsForSmallSide;
            lastAspect = guiCamera.aspect;
            lastProjectionMode = guiCamera.orthographic;
            lastFOV = guiCamera.fieldOfView;
            lastOrthSize = guiCamera.orthographicSize;

            //rearrange and match canvas view to the camera's aspect, projection mode, and FOV-
            //essentially simulating "screen space" canvasses when we're actually in world space
            AdjustToMatchCamera();
        }
    }

    private void AdjustToMatchCamera()
    {
		
        float height = 0f;
        float width = 0f;

        //get height and width values based on camera, at canvasDistance distance ahead

        //-->for orthographic camera projection mode (likely won't use this)
        if (guiCamera.orthographic)
        {
            guiCamera.orthographicSize = canvasDistance * (1f / guiCamera.aspect);

            height = 2f * guiCamera.orthographicSize;
            width = height * guiCamera.aspect;
        }
        //-->for perspective camera mode (the one we'll likely be using)
        else
        {
            float horizontalInDegrees = 90f;

            float horizontalInRadians = horizontalInDegrees * Mathf.Deg2Rad;
            float adjustByAspect = Mathf.Tan(horizontalInRadians * .5f) / guiCamera.aspect;
            float verticalInRadians = Mathf.Atan(adjustByAspect) * 2;
            guiCamera.fieldOfView = verticalInRadians * Mathf.Rad2Deg;

            Vector3 p1 = guiCamera.ViewportToWorldPoint(new Vector3(0f, 0f, canvasDistance));
            Vector3 p2 = guiCamera.ViewportToWorldPoint(new Vector3(1f, 0f, canvasDistance));
            Vector3 p3 = guiCamera.ViewportToWorldPoint(new Vector3(0f, 1f, canvasDistance));

            height = Vector3.Distance(p1, p3);
            width = Vector3.Distance(p1, p2);

        }
        guiCamera.farClipPlane = canvasDistance * 2f;

        //find true canvas size, with shorter side being "pixelScalar" in number of pixels
        Vector2 newSizeDelta = Vector2.zero;
        if (width > height)
            newSizeDelta = new Vector2(pixelsForSmallSide * (width / height), pixelsForSmallSide);
        else
            newSizeDelta = new Vector2(pixelsForSmallSide, pixelsForSmallSide * (height / width));

        Vector3 newScaler = new Vector3(width / newSizeDelta.x, height / newSizeDelta.y, 1f);

        //iterate over canvasses, setting new values
        for (int i = 0; i < 4; i++)
        {
            //set scale and distance to match screen size, based on new height and width
            menuRectTransforms[i].sizeDelta = newSizeDelta;
            menuRectTransforms[i].localScale = newScaler;
            menuRectTransforms[i].localRotation = Quaternion.LookRotation(directionArray[i]);
            menuRectTransforms[i].localPosition = directionArray[i] * canvasDistance;
        }
    }
}
