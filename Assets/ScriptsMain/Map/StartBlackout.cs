using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class StartBlackout : MonoBehaviour
{
    [TextArea(1, 10)]
    public string[] text;
    [TextArea(1, 10)]
    public string[] textEn;
    [TextArea(1, 10)]
    public string[] textTr;

    [TextArea(1, 10)]
    public string task;
    [TextArea(1, 10)]
    public string taskEn;
    [TextArea(1, 10)]
    public string taskTr;

    private Coroutine cor;
    public GameObject[] dependentTriggers;
    public string tagObj;

    private void OnTriggerEnter(Collider other)
    {
        ArrowPointer.instante.RemoveObj();
        Tasks.instante.DelitTask();

        if (other.gameObject.tag == "Player" && Inventory_list.instante != null &&
    Inventory_list.instante.list_inventory != null &&
    Inventory_list.instante.indx_now_obj >= 0 &&
    Inventory_list.instante.indx_now_obj < Inventory_list.instante.list_inventory.Count &&
    Inventory_list.instante.list_inventory[Inventory_list.instante.indx_now_obj] != null &&
    Inventory_list.instante.list_inventory[Inventory_list.instante.indx_now_obj].gameObject != null)
        {
            if ((Inventory_list.instante.list_inventory[Inventory_list.instante.indx_now_obj].gameObject.tag == "ToolBOX") && !Messages.instance1._Dialogs)
            {
                if (YandexGame.lang == "ru") Messages.instance1.StartDialog(text, task, tagObj, dependentTriggers);
                else if (YandexGame.lang == "tr") Messages.instance1.StartDialog(textTr, taskTr, tagObj, dependentTriggers);
                else Messages.instance1.StartDialog(textEn, taskEn, tagObj, dependentTriggers);

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
