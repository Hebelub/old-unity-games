using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteLine : MonoBehaviour
{
    public Transform[] lines;

    public Cleff cleff;

    public float noteDistance = 0f;

    public Transform transformAccidentals;

    private void Start()
    {
        // cleff = M.cleffF;

        Instantiate(cleff.gameObject.gameObject, lines[2].transform.position + Vector3.right * -11, Quaternion.identity, transform);

        if (lines.Length >= 2)
        {
            noteDistance = (lines[0].position.y - lines[1].position.y) / 2;
        }

        SetKeySignature();
    }

    public float GetYPositionOfPlacement(int placement)
    {
        return noteDistance * (placement + cleff.centerFromC) + transform.position.y;
    }

    public void SetKeySignature()
    {
        int currentKeySignature = C.CurrentKeySignature;

        float xSpacing = 0.2f;

        float startPosition = -7.3f;

        int[] placementOfAccidentals = M.GetPlacementOfAccidentals(currentKeySignature, cleff);

        GameObject accidentalGo;

        if(currentKeySignature > 0)
        {
            accidentalGo = M.singleSharp;
        }
        else
        {
            accidentalGo = M.singleFlat;
        }

        foreach (Transform transform in transformAccidentals)
        {
            Destroy(transform.gameObject);
        }

        for (int i = 0; i < Mathf.Abs(currentKeySignature); i++)
        {
            float xPosition = startPosition + i * xSpacing;

            float yPosition = GetYPositionOfPlacement(placementOfAccidentals[i]);

            Vector3 position = new Vector3(xPosition, yPosition, 0);

            Instantiate(accidentalGo, position, Quaternion.identity, transformAccidentals);
        }
    }

}
