using UnityEngine;

public class Door : MonoBehaviour
{
    private bool isOpen = false;

    public void OpenDoor()
    {
        if (!isOpen)
        {
            isOpen = true;
            Debug.Log("����� �������!");
            gameObject.SetActive(false);
        }
    }
}
