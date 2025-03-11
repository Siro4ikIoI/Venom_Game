using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public bool[] keyActive;
    public GameObject[] Lazers;

    void Update()
    {
        if (keyActive[0] && keyActive[1] && keyActive[2])
        {
            Lazers[0].gameObject.SetActive(false);
            Lazers[1].gameObject.SetActive(false);
        }
    }

    public void KeyActive()
    {

    }
}
