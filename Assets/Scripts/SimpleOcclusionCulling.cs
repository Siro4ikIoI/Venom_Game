using UnityEngine;

public class SimpleOcclusionCulling : MonoBehaviour
{
    public float cullingDistance = 100f; // Дистанция, на которой скрываются объекты
    private Camera mainCamera;
    public bool children;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (!children)
        {
            foreach (Transform obj in transform)
            {
                float distance = Vector3.Distance(mainCamera.transform.position, obj.position);
                if (distance < cullingDistance)
                {
                    obj.gameObject.SetActive(true);
                }
                else
                {
                    obj.gameObject.SetActive(false);
                }
            }
        }

        if (children)
        {
            float distance = Vector3.Distance(mainCamera.transform.position, gameObject.transform.position);
            if (distance < cullingDistance)
            {
                gameObject.GetComponent<Rigidbody>().isKinematic = false;
            }
            else
            {
                gameObject.GetComponent<Rigidbody>().isKinematic = true;
            }
        }
    }
}
