using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGen : MonoBehaviour
{
    public GameObject[] mapRoom;
    public int maxRooms = 10;
    private int currentRoomCount = 1;

    void Start()
    {
        int randomMainRoom = Random.Range(0, mapRoom.Length);
        GameObject mainRoom = Instantiate(mapRoom[randomMainRoom]);
        mainRoom.transform.position = new Vector3(0, 0, 0);
        GenRoom(mainRoom.transform);
    }

    public void GenRoom(Transform parentRoom)
    {
        if (currentRoomCount >= maxRooms)
            return;

        List<Transform> conectors = GetConnectors(parentRoom);

        foreach (Transform connector in conectors)
        {
            if (connector != null)
            {
                int randomRoom = Random.Range(1, mapRoom.Length);
                GameObject room = Instantiate(mapRoom[randomRoom]);
                currentRoomCount++;

                connector.SetParent(null);
                Vector3 newRoomPosition = GetRoomPosition(connector, room);

                room.transform.position = newRoomPosition;

                GenRoom(room.transform);
            }
        }
    }

    private List<Transform> GetConnectors(Transform room)
    {
        List<Transform> connectors = new List<Transform>();
        foreach (Transform child in room)
        {
            if (child.name == "Conecter") connectors.Add(child);
        }
        return connectors;
    }

    private Vector3 GetRoomPosition(Transform connector, GameObject room)
    {
        Vector3 position = Vector3.zero;

        if (connector.position.x > 0)
        {
            room.transform.Rotate(0, -90, 0);
            position = new Vector3(connector.position.x * -2, 0, connector.position.z);
        }
        else if (connector.position.x < 0)
        {
            room.transform.Rotate(0, 90, 0);
            position = new Vector3(connector.position.x * -2, 0, -connector.position.z);
        }
        else if (connector.position.z > 0)
        {
            room.transform.Rotate(0, 0, 0);
            position = new Vector3(0, 0, connector.position.z * 2);
        }
        else if (connector.position.z < 0)
        {
            room.transform.Rotate(0, 180, 0);
            position = new Vector3(0, 0, connector.position.z * 2);
        }

        return position;
    }
}
