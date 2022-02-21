using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAfterDelay : MonoBehaviour
{
    public float delay = 10f;
    [SerializeField]
    private FallController fallController;
    [SerializeField]
    private Transform walls;
    [SerializeField]
    private float wallScale = 2f;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(delay - 1f);
        Wrj.Utils.AffectGORecursively(gameObject, (go) => go.Alpha(0f, 1f));
        Wrj.Utils.DeferredExecution(2f, () => gameObject.SetActive(false));
        walls.EaseScale(Vector3.one * wallScale, 1f);
        fallController.isFalling = true;
    }
}
