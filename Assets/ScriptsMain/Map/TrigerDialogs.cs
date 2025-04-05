using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using YG;

public class TrigerDialogs : MonoBehaviour
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

    public GameObject[] dependentTriggers;
    public string tagObj;

    private void OnTriggerEnter(Collider other)
    {
        ArrowPointer.instante.RemoveObj();
        Tasks.instante.DelitTask();

        if (other.gameObject.tag == "Player" && !Messages.instance1._Dialogs)
        {
            if (YandexGame.lang == "ru") Messages.instance1.StartDialog(text, task, tagObj, dependentTriggers);
            else if (YandexGame.lang == "tr") Messages.instance1.StartDialog(textTr, taskTr, tagObj, dependentTriggers);
            else Messages.instance1.StartDialog(textEn, taskEn, tagObj, dependentTriggers);

            if (dependentTriggers != null)
            {
                for (int i = 0; i < dependentTriggers.Length; i++) dependentTriggers[i].SetActive(true);
            }

            gameObject.SetActive(false);
        }
    }
}
