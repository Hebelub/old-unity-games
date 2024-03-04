using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyButton : MonoBehaviour
{
    public SpriteRenderer sr;
    public GameManager gm;

    public GameObject whiteBackground;

    public float cost;

    public AudioClip sound;

    private void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        gm = GameManager.instance;

    }

    private void Awake()
    {
        whiteBackground.SetActive(false);
    }

    public string toolTips;

    public void UpdateStuff()
    {
        if (cost <= gm.score)
        {
            sr.color = new Color(0.6f, 1f, 0.5f);
        }
        else
        {
            sr.color = new Color(1f, 0.5f, 0.4f);
        }


    }

    private void OnMouseEnter()
    {
        sr.color = new Color(0.3f, 0.7f, 0.7f);

        gm.toolTips.SetText(name + " - Price: " + cost + "\n" + toolTips);

        whiteBackground.SetActive(true);

    }

    private void OnMouseExit()
    {
        UpdateStuff();
        gm.toolTips.SetText("");
        whiteBackground.SetActive(false);
    }

    public string power;

    private void OnMouseDown()
    {
        if (cost <= gm.score)
        {
            gm.score -= cost;

            if(power == "Heal")
            {
                gm.health += 10;
            }
            else if(power == "Injection")
            {
                gm.poison -= 100;
            }
            else if (power == "Shake")
            {
                gm.ExploreNewIslands();
            }
            else if (power == "Shake")
            {
                gm.ExploreNewIslands();
            }
            else if (power == "TNT")
            {
                if (gm.lastFinding.island.nextFinding == gm.lastFinding.island.map[gm.lastFinding.index])
                {
                    gm.lastFinding.island.Bomb();
                }
                gm.RemoveLastFinding();

            }
            else if (power == "Doctor")
            {
                gm.health += 40;
                gm.poison -= 100;
            }
            else if (power == "Nuke")
            {
                gm.Nuke();
            }

            gm.audioSource.PlayOneShot(sound, 1f);

            gm.UpdateUi();
        }
    }

}
