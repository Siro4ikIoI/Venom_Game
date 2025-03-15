using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public bool[] keyActive;
    public GameObject[] lazers;
    public GameObject[] keys;
    public GameObject[] theLock;

    void Update()
    {
        if (keyActive[0] && keyActive[1] && keyActive[2])
        {
            lazers[0].gameObject.SetActive(false);
            lazers[1].gameObject.SetActive(false);
        }
    }

    public void KeyActive()
    {

    }
}
