using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Transform door; // ������ �� ������ �����
    public float openPosition; // �������, ����� ����� �������
    private Vector3 closedPosition; // �������, ����� ����� �������
    public float speed = 2f; // �������� �������� �����
    private int playersInside = 0; // ���������� ������� � ���� ���������

    public AudioSource doorOpenVolium;

    public bool _isBlackOut = true;

    public static DoorController instante { get; set; }

    private void Start()
    {
        instante = this;
        closedPosition = door.position; // ���������� ��������� ������� �����
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
                // ������� ����� �����
                door.position = Vector3.Lerp(door.position, new Vector3(door.position.x, openPosition, door.position.z), Time.deltaTime * speed);
            }
            else
            {
                // ������� ����� ����
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
