using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OnHoverShowInfo : MonoBehaviour
{
    public bool scaleIsRunning = false;
    public Vector3 wantedSize = Vector3.zero;

    public Vector3 smallSize = Vector3.zero;
    public Vector3 bigSize = Vector3.one * 1f;

    public float scaleSpeed = 0.05f;

    public bool onIsland = true;

    private void Awake()
    {
        
        if (onIsland) island = GetComponentInParent<Island>();


    }

    private void Start()
    {

        transform.GetChild(0).localScale = smallSize;
    }

    private void OnMouseEnter()
    {
        wantedSize = bigSize;

        Scale();
    }

    private void OnMouseExit()
    {
        wantedSize = smallSize;

        Scale();
    }

    public void Scale()
    {

        if (!scaleIsRunning)
        {
            StartCoroutine(ScaleInfoShower());
        }
    }

    public IEnumerator ScaleInfoShower()
    {

        Vector3 oldScale = transform.GetChild(0).localScale;

        float part = 0;

        while (true)
        {
            part += scaleSpeed;

            if (part > 1) part = 1;

            transform.GetChild(0).localScale = Vector3.Slerp(oldScale, wantedSize, part);

            if (part >= 1)
            {
                break;
            }

            yield return null;
        }
    }

    public Island island;

    public TextMeshProUGUI score;
    public TextMeshProUGUI health;
    public TextMeshProUGUI poison;
    public TextMeshProUGUI comment;

    public void DisplayFinding()
    {
        Finding finding;

        if (onIsland)
        {
            finding = island.nextFinding;

        }
        else
        {
            finding = GameManager.instance.lastFinding.island.map[GameManager.instance.lastFinding.index];
        }

        score.text = finding.score.ToString();
        health.text = (finding.damage * -1).ToString();
        poison.text = finding.poison.ToString();

        comment.text = finding.extra;

        if (finding.extra == "")
        {
            comment.gameObject.SetActive(false);
        }
        else
        {
            comment.gameObject.SetActive(true);
        }

        // Display it
        // It needs textboxes and the finding should be changed from Island, also DisplayFinding function should run from Island
    }

}
