using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class SpawnerResource : Spawner<Resource>
{
    float _delay = 1f;
    float _delayBetween—ycles = 10f;
    private int _radius = 10;
    WaitForSeconds _wait;
    WaitForSeconds _waitForEndOfCycle;

    private void Start()
    {
        _wait = new WaitForSeconds(_delay);
        _waitForEndOfCycle = new WaitForSeconds(_delayBetween—ycles);

        StartCoroutine(WaitForResource());
    }

    private Vector3 RandomNavSphere(float radius)
    {
        int areaMask = -1;

        Vector3 randomDirection = Random.insideUnitSphere * radius;

        randomDirection += transform.position;

        NavMesh.SamplePosition(randomDirection, out NavMeshHit navHit, radius, areaMask);

        return navHit.position;
    }

    private IEnumerator WaitForResource()
    {
        for (int i = 0; i <= PoolCapacity; i++)
        {
            for (int j = 0; j <= PoolCapacity; j++)
            {
                yield return _wait;

                Spawn(RandomNavSphere(_radius));
            }

            yield return _waitForEndOfCycle;
        }
    }
}