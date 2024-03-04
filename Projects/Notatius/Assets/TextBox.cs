using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBox : MonoBehaviour
{

    public bool isHoveringOver = false;
    public bool isSelected = false;
    public bool isHookedToMouse = false;

    public Vector3 hookedVectorOffset = Vector3.zero;

    public Color hoverColor;
    public Color normalColor;

    public Image background;

    private void Start()
    {
        normalColor = background.color;
        hoverColor = new Color(normalColor.r, normalColor.g, normalColor.b, 0.5f);
    }

    private void OnMouseOver()
    {
        HoverOver(true);

        if(Input.GetMouseButtonUp(1))
        {
            Select(true);
        }
        if (Input.GetMouseButtonDown(1))
        {
            Hook(true);
        }

    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(1))
        {
            Hook(false);
        }
        if (Input.GetMouseButtonDown(1))
        {
            Select(false);
        }
        if(isHookedToMouse)
        {
            MoveToMouse();
        }
    }

    public void MoveToMouse()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + hookedVectorOffset;
    }
    public void Hook(bool hook)
    {
        isHookedToMouse = hook;
        hookedVectorOffset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    public void Select(bool select)
    {
        isSelected = select;

        if (select)
        {
            
        }
        else
        {
            //
        }
    }

    private void OnMouseExit()
    {
        HoverOver(false);
    }

    public void HoverOver(bool hoveres)
    {
        isHoveringOver = hoveres;

        if (hoveres)
        {
            background.color = hoverColor;
        }
        else
        {
            background.color = normalColor;
        }
    }

}
