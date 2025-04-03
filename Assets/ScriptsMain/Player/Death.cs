using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    public GameObject detahIntrfase;

    public void DeathInterfase()
    {
        detahIntrfase.SetActive(true);
    }
}
