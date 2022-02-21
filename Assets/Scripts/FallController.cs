using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallController : MonoBehaviour
{
    [SerializeField]
    private Transform humanoid;
    [SerializeField]
    private Vector2 range;
    [SerializeField]
    float xySpeed = 1f;
    [SerializeField]
    Camera camera;


    [SerializeField]
    private Camera threeDCam;
    [SerializeField]
    private Camera twoDCam;
    [SerializeField]
    ParticleSystem starfield;
    private Coroutine zoomCoro;
    private bool _isFalling = false;
    public bool IsFalling
    {
        set
        {
            if (_isFalling == value) return;
            _isFalling = value;
            if (_isFalling)
            {
                starfield.Play();
            }
            else
            {
                starfield.Stop();
            }
        }
        get => _isFalling;
    }

    private float initFOV;
    private Vector3 initCamPos;

    public static FallController Instance;
    void Awake ()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("Multiple FallController's instantiated. Component removed from " + gameObject.name + ". Instance already found on " + Instance.gameObject.name + "!");
            Destroy(this);
        }
    }
    private void Start()
    {
        initFOV = camera.fieldOfView;
        initCamPos = camera.transform.localPosition;
    }


    void Update()
    {
        if (!_isFalling) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (zoomCoro != null)
            {
                StopCoroutine(zoomCoro);
                zoomCoro = null;
            }
            zoomCoro = StartCoroutine(ZoomRoutine());
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            if (zoomCoro != null)
            {
                StopCoroutine(zoomCoro);
                zoomCoro = null;
            }
            zoomCoro = StartCoroutine(UnZoomRoutine());
        }

        float appliedSpeed = xySpeed * Time.deltaTime;

        Vector3 pos = humanoid.localPosition;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            pos.x += xySpeed;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            pos.x -= xySpeed;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            pos.z += xySpeed;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            pos.z -= xySpeed;
        }
        range.x = Mathf.Abs(range.x);
        range.y = Mathf.Abs(range.y);
        pos.x = Mathf.Clamp(pos.x, -range.x, range.x);
        pos.z = Mathf.Clamp(pos.z, -range.y, range.y);
        // Debug.Log($"Setting XY: {pos}");
        humanoid.localPosition = pos;
    }

    bool is3d = false;
    public bool Is3D => is3d;

    public void Toggle3D()
    {
        is3d = !is3d;
        Switcheroo();
    }
    private void Switcheroo()
    {
        Debug.Log("Switcheroo");
        if(is3d)
        {
            if (zoomCoro != null)
            {
                StopCoroutine(zoomCoro);
                zoomCoro = null;
            }
            zoomCoro = StartCoroutine(ZoomRoutine());
        }
        else
        {
            if (zoomCoro != null)
            {
                StopCoroutine(zoomCoro);
                zoomCoro = null;
            }
            zoomCoro = StartCoroutine(UnZoomRoutine());
        }
    }
    private IEnumerator ZoomRoutine()
    {
        Wrj.Utils.MapToCurve.Linear.MatchSibling(camera.transform, threeDCam.transform, .75f);
        yield return Wrj.Utils.MapToCurve.Linear.ManipulateFloat((v) => camera.fieldOfView = v, camera.fieldOfView, threeDCam.fieldOfView, .75f).coroutine;
    }
    private IEnumerator UnZoomRoutine()
    {
        Wrj.Utils.MapToCurve.Linear.MatchSibling(camera.transform, twoDCam.transform, .75f);
        yield return Wrj.Utils.MapToCurve.Linear.ManipulateFloat((v) => camera.fieldOfView = v, camera.fieldOfView, twoDCam.fieldOfView, .75f).coroutine;
    }
}
