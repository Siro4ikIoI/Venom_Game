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
            if (distance < cullingDistance && Inventory_list.instante == null &&
    Inventory_list.instante.list_inventory != null &&
    Inventory_list.instante.indx_now_obj >= 0 &&
    Inventory_list.instante.indx_now_obj < Inventory_list.instante.list_inventory.Count &&
    Inventory_list.instante.list_inventory[Inventory_list.instante.indx_now_obj] != null &&
    Inventory_list.instante.list_inventory[Inventory_list.instante.indx_now_obj].gameObject != null)
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
