using UnityEngine;

public class Door : MonoBehaviour
{
    private bool isOpen = false;

    public void OpenDoor()
    {
        if (!isOpen)
        {
            isOpen = true;
            Debug.Log("Дверь открыта!");
            gameObject.SetActive(false);
        }
    }
}
