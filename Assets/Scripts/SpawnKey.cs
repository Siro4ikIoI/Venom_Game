using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnKey : MonoBehaviour
{
    public GameObject[] key;
    public GameObject[] gameOBJ;

    public List<Vector3> usedPositions = new List<Vector3>();

    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            Vector3 pos = GetUniquePosition();
            usedPositions.Add(pos);

            GameObject keys = Instantiate(key[i], pos, key[i].transform.rotation);
            keys.gameObject.tag = "Key" + (i + 1).ToString();
            keys.transform.localScale = new Vector3(6, 6, 6);
        }

        for (int i = 0; i < gameOBJ.Length; i++)
        {
            Vector3 pos = GetUniquePosition();
            usedPositions.Add(pos);

            GameObject GameOBJ = Instantiate(gameOBJ[i], pos, gameOBJ[i].transform.rotation);
        }
    }

    Vector3 GetUniquePosition()
    {
        Vector3 pos;
        int attempts = 100;

        do
        {
            pos = LevelGen.SpavnKeys[Random.Range(0, LevelGen.SpavnKeys.Count)].position;
            pos.y = 0.5f;
            attempts--;
        }
        while (usedPositions.Contains(pos) && attempts > 0);

        return pos;
    }
}
