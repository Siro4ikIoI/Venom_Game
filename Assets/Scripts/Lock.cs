using UnityEngine;
using System;

public class Lock : MonoBehaviour
{
    public static event Action OnLockActivated;
    private bool isActivated = false;

    public void ActivateLock()
    {
        if (!isActivated && Inventory_list.instante.list_inventory[Inventory_list.instante.indx_now_obj].gameObject.tag == "Keys")
        {
            isActivated = true;
            Inventory_list.instante.Drop_obj();
            Destroy(Inventory_list.instante.list_inventory[Inventory_list.instante.indx_now_obj].gameObject);
            Debug.Log(gameObject.name + " замок активирован!");
            OnLockActivated?.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && true) ActivateLock();
    }
}
