using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBlackout : MonoBehaviour
{
    [TextArea(1, 10)]
    public string[] text;
    private Coroutine cor;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && Inventory_list.instante != null &&
    Inventory_list.instante.list_inventory != null &&
    Inventory_list.instante.indx_now_obj >= 0 &&
    Inventory_list.instante.indx_now_obj < Inventory_list.instante.list_inventory.Count &&
    Inventory_list.instante.list_inventory[Inventory_list.instante.indx_now_obj] != null &&
    Inventory_list.instante.list_inventory[Inventory_list.instante.indx_now_obj].gameObject != null)
        {
            if ((Inventory_list.instante.list_inventory[Inventory_list.instante.indx_now_obj].gameObject.tag == "ToolBOX"))
            {
                Mesages.instance1.StartDiolog(text);
                cor = StartCoroutine(StartCountdown());
            }
        }
    }

    private IEnumerator StartCountdown()
    {
        Debug.Log("Таймер запущен!");
        yield return new WaitForSeconds(15f);
        Debug.Log("15 секунд прошло!");
        StartHunting.Instance.isCounting = true;
    }

}
