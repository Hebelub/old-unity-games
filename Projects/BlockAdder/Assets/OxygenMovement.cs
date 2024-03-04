using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenMovement : MonoBehaviour {

    public float currentLift;

    public float yPositionRelativeToCenter;

    Rigidbody2D rb;
    GameManager gm;
    Lung lung;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        gm = GameManager.instance;
        lung = gm.lung;

        StartCoroutine(IMovement());
    }

    public float CalculateNextLift()
    {
        float nextLift = -yPositionRelativeToCenter + gm.lung.breathing -0.5f;

        return nextLift;
    }

    IEnumerator IMovement()
    {
        while (true)
        {
            yPositionRelativeToCenter = transform.position.y;

            // Calibrate some of the variables
            currentLift = CalculateNextLift();

            Debug.Log("CurrentLift: " + currentLift);

            rb.AddForce(Vector3.up * currentLift * Time.deltaTime * 10);

            Avoid();

            yield return true;
        }
    }

    Transform closestBlodcell;

    public void Avoid()
    {
        float distanse = 2.5f;

        closestBlodcell = Physics2D.OverlapCircle(transform.position, distanse).transform;

        if (closestBlodcell == null)
        {
            return;
        }

        if (true)
        {
            Vector2 directionToAvoid = (transform.position.x > closestBlodcell.position.x) ? Vector2.right : Vector2.left;

            transform.position += ((Vector3)directionToAvoid * 0.1f);

        }
    }
    

}
