using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wrj;
public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField]
    FallController fallController;
    [SerializeField]
    ObjectPool pool;
    
    IEnumerator Start()
    {
        while(true)
        {
            yield return new WaitForSeconds(1f + Random.value);
            if (fallController.isFalling)
            {
                var spawn = pool.GetObject();
                spawn.transform.localPosition = spawn.transform.localPosition.With(x: Random.Range(-5f, 5f), z: Random.Range(-5f, 5f));
                Vector3 target = spawn.transform.localPosition.With(y: 16f);
                spawn.Move(target, 1f);
            }
        }
    }
}
