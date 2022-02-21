using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wrj;
public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField]
    ObjectPool pool;
    
    [SerializeField]
    ObjectPool topDownPool;

    
    IEnumerator Start()
    {
        while(true)
        {
            yield return new WaitForSeconds(1f + Random.value);
            if (FallController.Instance.isFalling)
            {
                if (FallController.Instance.Is3D)
                {
                    // Top Down
                    var _topDownSpawn = topDownPool.GetObject();
                    _topDownSpawn.transform.localPosition = _topDownSpawn.transform.localPosition.With(x: Random.Range(-5f, 5f));
                    Vector3 target = _topDownSpawn.transform.localPosition.With(z: -16f);
                    _topDownSpawn.Move(target, 1f);

                }
                else
                {
                    // Driving toward
                    var spawn = pool.GetObject();
                    spawn.transform.localPosition = spawn.transform.localPosition.With(x: Random.Range(-5f, 5f), z: Random.Range(-5f, 5f));
                    Vector3 target = spawn.transform.localPosition.With(y: 16f);
                    spawn.Move(target, 1f);
                }
            }
        }
    }
}
