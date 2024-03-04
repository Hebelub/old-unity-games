using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DestroyOnTouch : MonoBehaviour
{

    SpriteRenderer sr;

    public Color highlightColor;
    Color normalColor;

    private bool isFading = false;

    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        normalColor = sr.color;
    }

    private void OnMouseDown()
    {
        if(!isFading)
            Eliminate();
    }

    public void Eliminate()
    {
        PlayEliminationSound();

        Fade();
    }

    public void Fade()
    {
        gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "";

        GameManager.instance.isAltered = true;

        isFading = true;

        Destroy(gameObject, 0.2F);

        StartCoroutine(IFade());

        IEnumerator IFade()
        {
            
            while(true)
            {
                sr.color = new Color(sr.color.r, sr.color.b, sr.color.g, sr.color.a - Time.deltaTime * 5);

                yield return null;
            }
        }

    }

    public void PlayEliminationSound()
    {
        GameManager.instance.audioSource.PlayOneShot(GameManager.instance.eliminationSound, GameManager.instance.volume);
    }

    private void OnMouseEnter()
    {
        if (!isFading && GameManager.instance.timeSpent <= 0)
            Highlight();
    }

    private void OnMouseOver()
    {
        if (GameManager.instance.timeSpent > 0)
        {
            DeLight();
        }
    }

    private void OnMouseExit()
    {
        if (!isFading)
            DeLight();
    }

    public void Highlight()
    {
        sr.color = highlightColor;
    }

    public void DeLight()
    {
        sr.color = normalColor;
    }



}
