using Unity.VisualScripting;
using UnityEngine;

public class ArrowPointer : MonoBehaviour
{
    private Transform target; // Объект, к которому направляем игрока
    private float maxDistance = 5000f; // Дальность луча

    public static ArrowPointer instante { get; set; }

    private LineRenderer lineRenderer; // Линия для визуализации луча

    void Start()
    {
        instante = this;

        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.5f;
        lineRenderer.endWidth = 0.5f;
        lineRenderer.positionCount = 2;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
    }

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 direction = target.position - transform.position;
        Ray ray = new Ray(transform.position, direction);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            lineRenderer.startColor = new Color(0, 255, 0, 0.5f);
            lineRenderer.endColor = new Color(0, 255, 0, 0.5f);

            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, hit.point);
        }

    }

    public void SetObj(string tagObj)
    {
        if (tagObj == "null") target = null;
        else target = GameObject.FindGameObjectWithTag(tagObj).transform;
        Debug.Log(tagObj);
    }

    public void RemoveObj()
    {
        target = null;
    }
}

