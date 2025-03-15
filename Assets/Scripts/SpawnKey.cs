using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnKey : MonoBehaviour
{
    public GameObject[] key;

    void Start()
    {
        for(int i = 0;i < 3; i++)
        {
            Vector3 pos = LevelGen.SpavnKeys[Random.Range(0, LevelGen.SpavnKeys.Count)].position;
            pos.y = 0.5f;
            GameObject keys = Instantiate(key[i], pos, key[i].transform.rotation);
            keys.transform.localScale = new Vector3(6, 6, 6);
        }
    }
}

