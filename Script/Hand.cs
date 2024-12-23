using UnityEngine;

public class Hand : MonoBehaviour
{
    [SerializeField] private Bot _bot;
    
    private Vector3 _resourcePosition;
    private Vector3 _startPosition;

    private void Start()
    {
        _startPosition = transform.position;
    }

    private void OnEnable()
    {
        _bot.HasCome += InteractWithResource;
    }

    private void OnDisable()
    {
        _bot.HasCome -= InteractWithResource;
    }

    public void InteractWithResource(Resource resource, Transform destination)
    {
        SetParent(resource, destination);
        SetPosition(resource, destination);
    }

    private void SetParent(Resource resource, Transform destination)
    {
        resource.transform.parent = destination;
    }

    private void SetPosition(Resource resource, Transform destination)
    {
        resource.transform.position = destination.position;
    }
}