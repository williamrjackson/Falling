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
    float targetFOV = 30f;
    
    [SerializeField]
    float targetCamPos = 30f;

    public bool isFalling = false;

    private Coroutine zoomCoro;

    void Update()
    {
        if (!isFalling) return;

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

    private IEnumerator ZoomRoutine()
    {
        Wrj.Utils.MapToCurve.EaseIn.Move(camera.transform, Vector3.up * targetCamPos, .75f);
        yield return Wrj.Utils.MapToCurve.EaseIn.ManipulateFloat((v) => camera.fieldOfView = v, camera.fieldOfView, targetFOV, .75f).coroutine;
    }
    private IEnumerator UnZoomRoutine()
    {
        Wrj.Utils.MapToCurve.EaseIn.Move(camera.transform, Vector3.up * 20f, .75f);
        yield return Wrj.Utils.MapToCurve.EaseIn.ManipulateFloat((v) => camera.fieldOfView = v, camera.fieldOfView, 60f, .75f).coroutine;
    }
}
