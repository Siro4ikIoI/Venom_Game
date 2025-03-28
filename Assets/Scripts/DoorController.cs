using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Transform door; // Ссылка на объект двери
    public float openPosition; // Позиция, когда дверь открыта
    private Vector3 closedPosition; // Позиция, когда дверь закрыта
    public float speed = 2f; // Скорость движения двери
    private int playersInside = 0; // Количество игроков в зоне триггеров

    public AudioSource doorOpenVolium;

    public bool _isBlackOut = true;

    public static DoorController instante { get; set; }

    private void Start()
    {
        instante = this;
        closedPosition = door.position; // Запоминаем начальную позицию двери
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorOpenVolium.Play();
            playersInside++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorOpenVolium.Play();
            playersInside--;
        }
    }

    private void Update()
    {
        if (_isBlackOut)
        {
            if (playersInside > 0)
            {
                // Двигаем дверь вверх
                door.position = Vector3.Lerp(door.position, new Vector3(door.position.x, openPosition, door.position.z), Time.deltaTime * speed);
            }
            else
            {
                // Двигаем дверь вниз
                door.position = Vector3.Lerp(door.position, new Vector3(door.position.x, 10, door.position.z), Time.deltaTime * speed);
            }
        }

        else door.position = Vector3.Lerp(door.position, new Vector3(door.position.x, 10, door.position.z), Time.deltaTime * speed);
    }

    public void BlackOut()
    {
        doorOpenVolium.Play();
        door.position = Vector3.Lerp(door.position, new Vector3(door.position.x, openPosition, door.position.z), Time.deltaTime * speed);
    }
}
