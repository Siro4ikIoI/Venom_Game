using UnityEngine;

public class LockManager : MonoBehaviour
{
    public Lock[] locks;
    private int activatedLocks = 0;
    public Door door;

    private void OnEnable()
    {
        Lock.OnLockActivated += CheckLocks;
    }

    private void OnDisable()
    {
        Lock.OnLockActivated -= CheckLocks;
    }

    private void CheckLocks()
    {
        activatedLocks++;
        if (activatedLocks >= locks.Length)
        {
            Debug.Log("Все замки активированы! Открываем дверь.");
            door.OpenDoor();
        }
    }
}
