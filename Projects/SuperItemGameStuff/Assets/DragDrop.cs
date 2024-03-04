using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour
{
    private Color mouseOverColor = Color.white;
    private Color originalColor = Color.yellow;
    private bool dragging = false;
    private float distance;

    public float pull = 1.4F;
    public GameObject circleGO;
    public SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = circleGO.GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        mouseOverColor.a = originalColor.a * 2;
    }

    void OnMouseEnter()
    {
        spriteRenderer.color = mouseOverColor;
    }

    void OnMouseExit()
    {
        spriteRenderer.color = originalColor; 
    }

    void OnMouseDown()
    {
        transform.localScale *= pull;
        distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        dragging = true;
        circleGO.GetComponent<CircleCollider2D>().enabled = false;
    }

    void OnMouseUp()
    {
        transform.localScale /= pull;
        dragging = false;
        circleGO.GetComponent<CircleCollider2D>().enabled = true;

    }

    void Update()
    {
        if (dragging)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 rayPoint = ray.GetPoint(distance);
            transform.position = rayPoint;
        }
    }
}
