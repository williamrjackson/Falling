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
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
        walls.EaseScale(Vector3.one * wallScale, 1f);
        fallController.isFalling = true;
    }
}
