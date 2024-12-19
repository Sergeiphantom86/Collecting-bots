using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class SpawnerResource : Spawner<Resource>
{
    private float _delay = 1f;
    private float _delayBetween—ycles = 5f;
    private int _quantityOfCycles = 3;
    private int _quantityOfItemsInCycle = 1;
    private int _radius = 10;
    private WaitForSeconds _wait;
    private WaitForSeconds _waitForEndOfCycle;

    private void Start()
    {
        _wait = new WaitForSeconds(_delay);
        _waitForEndOfCycle = new WaitForSeconds(_delayBetween—ycles);

        StartCoroutine(WaitForResource());
    }

    private Vector3 RandomNavSphere(float radius)
    {
        int areaMask = -1;

        Vector3 randomPosition = Random.insideUnitSphere * radius;

        randomPosition += transform.position;

        NavMesh.SamplePosition(randomPosition, out NavMeshHit navHit, radius, areaMask);

        return navHit.position;
    }

    private IEnumerator WaitForResource()
    {
        for (int i = 0; i <= _quantityOfCycles; i++)
        {
            for (int j = 0; j <= _quantityOfItemsInCycle; j++)
            {
                yield return _wait;

                Spawn(RandomNavSphere(_radius));
            }

            yield return _waitForEndOfCycle;
        }
    }
}