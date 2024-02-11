using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;
    [SerializeField] bool reversed;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        transform.LookAt(mainCamera.transform);
        if (reversed)
        {
            transform.Rotate(0, 180, 0);
        }
    }

}
