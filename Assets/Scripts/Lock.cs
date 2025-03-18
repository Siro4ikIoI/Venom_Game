using UnityEngine;
using System;

public class Lock : MonoBehaviour
{
    public static event Action OnLockActivated;
    private bool isActivated = false;

    public void ActivateLock()
    {
        if (!isActivated)
        {
            isActivated = true;
            Debug.Log(gameObject.name + " замок активирован!");
            OnLockActivated?.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && true) ActivateLock();
    }
}
