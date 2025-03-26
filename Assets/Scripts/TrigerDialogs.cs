using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrigerDialogs : MonoBehaviour
{
    [TextArea(1, 10)]
    public string[] text;
    public GameObject[] dependentTriggers;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !Mesages.instance1._Dialogs)
        {
            Mesages.instance1.StartDiolog(text);
            
            if(dependentTriggers != null)
            {
                for (int i = 0; i < dependentTriggers.Length; i++) dependentTriggers[i].SetActive(true);
            }

            gameObject.SetActive(false);
        }
    }
}
