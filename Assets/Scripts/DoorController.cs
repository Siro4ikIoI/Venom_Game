using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Transform door; // ������ �� ������ �����
    public float openPosition; // �������, ����� ����� �������
    private Vector3 closedPosition; // �������, ����� ����� �������
    public float speed = 2f; // �������� �������� �����
    private int playersInside = 0; // ���������� ������� � ���� ���������

    private void Start()
    {
        closedPosition = door.position; // ���������� ��������� ������� �����
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // ���������, ��� ����� �����
        {
            playersInside++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // ���������, ��� ����� �����
        {
            playersInside--;
        }
    }

    private void Update()
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
}
