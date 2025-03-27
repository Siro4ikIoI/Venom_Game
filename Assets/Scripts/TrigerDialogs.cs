using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrigerDialogs : MonoBehaviour
{
    [TextArea(1, 10)]
    public string[] text;
    [TextArea(1, 10)]
    public string task;
    public GameObject[] dependentTriggers;
    public GameObject[] blackoutDependentTriggers;

    private void OnTriggerEnter(Collider other)
    {
        Tasks.instante.DelitTask();

        if (other.gameObject.tag == "Player" && !Messages.instance1._Dialogs)
        {
            Messages.instance1.StartDialog(text, task);

            if (dependentTriggers != null)
            {
                for (int i = 0; i < dependentTriggers.Length; i++) dependentTriggers[i].SetActive(true);
            }

            gameObject.SetActive(false);
        }
    }
}
