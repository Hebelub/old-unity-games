using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public Color baseColor;
    public Color shapeColor;
    public Shape shape;

    public float range;

    public SpriteRenderer srBase;
    public SpriteRenderer srShape;
    public Collider2D thisCollider;

    private void Start()
    {
        Refresh();
    }

    private void OnMouseDown()
    {
        ActivateAllInRange();
    }

    public void Refresh()
    {
        srBase.color = baseColor;
        srShape.color = shapeColor;
        srShape.sprite = shape.shape;
    }

    public void ActivateAllInRange()
    {
        thisCollider.enabled = false;

        RaycastHit2D hit;
        foreach (Unit unit in GameManager.instance.allUnits)
        {
            Debug.Log(unit.name);

            if (unit == this) continue;

            hit = Physics2D.Raycast(transform.position, unit.transform.position - transform.position, 100f);

            if (hit.collider == null) continue;
            if (hit.transform.GetComponent<Unit>().baseColor == shapeColor)

            // Debug.DrawLine(transform.position, hit.point, Color.blue, 4f);

            if (hit.collider.gameObject == unit.gameObject)
            {
                hit.transform.GetComponent<Unit>().RunHitFunction(shape.method, this);
            }

        }

        thisCollider.enabled = true;
    }

    public void RunHitFunction(string function, Unit fromUnit)
    {
        shapeColor = fromUnit.baseColor;
        Refresh();
    }

}
