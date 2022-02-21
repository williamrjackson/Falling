using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Enter On Portal");
        FallController.Instance.Toggle3D();
    }
}
