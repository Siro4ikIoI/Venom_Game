using UnityEngine;
using System;

public class Lock : MonoBehaviour
{
    public static event Action OnLockActivated;
    private bool isActivated = false;
    public Material yourNewMaterial;

    public string key;

    public void ActivateLock()
    {
        if (!isActivated && Inventory_list.instante != null &&
    Inventory_list.instante.list_inventory != null &&
    Inventory_list.instante.indx_now_obj >= 0 &&
    Inventory_list.instante.indx_now_obj < Inventory_list.instante.list_inventory.Count &&
    Inventory_list.instante.list_inventory[Inventory_list.instante.indx_now_obj] != null &&
    Inventory_list.instante.list_inventory[Inventory_list.instante.indx_now_obj].gameObject != null)
        {
            if ((Inventory_list.instante.list_inventory[Inventory_list.instante.indx_now_obj].gameObject.tag == key))
            {
                isActivated = true;
                MeshRenderer renderer = gameObject.GetComponentInChildren<MeshRenderer>();
                if (renderer != null)
                {
                    int index = 6;
                    if (index < renderer.materials.Length)
                    {
                        Material[] materials = renderer.materials;
                        materials[index] = yourNewMaterial;
                        renderer.materials = materials;
                    }
                }
                Debug.Log(gameObject.name + " замок активирован!");
                OnLockActivated?.Invoke();
            }
        }

        else
        {
            Debug.Log(gameObject.name + " замок не активирован!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && true) ActivateLock();
    }
}
