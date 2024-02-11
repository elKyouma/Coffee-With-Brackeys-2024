using UnityEngine;

public class Billboard : MonoBehaviour 
{
    private Camera mainCamera;
    [SerializeField] bool reversed;

    private void Start() 
    {
        mainCamera = Camera.main;
    }

    private void LateUpdate() 
    {
        transform.LookAt(transform.position + mainCamera.transform.forward);
        if (reversed)
        {
            transform.Rotate(0, 180, 0);
        }
    }
}
