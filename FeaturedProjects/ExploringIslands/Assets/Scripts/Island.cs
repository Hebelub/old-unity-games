using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Island : MonoBehaviour
{

    public int mapSize; // How many things in the deck to explore

    public Finding[] map;

    GameManager gm;

    public Finding nextFinding;
    public int nextFindingIndex;

    public Vector3 ShowOffset = Vector3.zero;

    public OnHoverShowInfo ohsi;

    public Transform findingSprite;

    float otherScale = 1.33f;
    float originalScale = 1f;

    public bool isHoveringOver = false;

    public IEnumerator IScaleFindingSprite()
    {
        while(isHoveringOver)
        {
            float t = 0;
            Vector3 v = Vector3.one;

            float scalingspeed = 0.025f + gm.poison * 0.0025f;

            while(true)
            {
                t += scalingspeed;

                if (t < 0.5f) findingSprite.localScale = Vector3.Slerp(originalScale * v * scaleSize, otherScale * v * scaleSize, t * 2);
                else if (t > 1)
                {
                    findingSprite.localScale = originalScale * v * scaleSize;
                    break;
                }
                else
                {
                    float t2 = t - 0.5f;
                    findingSprite.localScale = Vector3.Slerp(otherScale * v * scaleSize, originalScale * v * scaleSize, t2 * 2);
                }

                yield return null;

            }
        }
    }

    float scaleSize;

    private void Start()
    {

        gm = GameManager.instance;

        showingItem = Instantiate(gm.islandLooker, transform.position, Quaternion.identity, transform).GetComponentInChildren<SpriteRenderer>();

        ohsi = transform.GetComponentInChildren<OnHoverShowInfo>();

        findingSprite = transform.GetChild(2).GetChild(0);
        scaleSize = findingSprite.localScale.x;

        CreateDeck();

        CreateNextFinding();

    }

    private void OnMouseDown()
    {
        if (gm.possibleToExplore)
            WantToExplore();
    }

    public void CreateDeck()
    {
        map = new Finding[mapSize];

        for (int i = 0; i < mapSize; i++)
        {
            // Debug.Log(gm.findings.Count);
            map[i] = gm.findings[Random.Range(0, gm.findings.Count)];
        }
    }

    private void OnMouseEnter()
    {
        isHoveringOver = true;

        GetComponentInChildren<SpriteRenderer>().color = new Color(1,1,1,0.75f);

        ohsi.wantedSize = ohsi.bigSize;
        ohsi.Scale();

        StartCoroutine(IScaleFindingSprite());
    }
    private void OnMouseExit()
    {
        isHoveringOver = false;

        GetComponentInChildren<SpriteRenderer>().color = new Color(1, 1, 1, 1);

        ohsi.wantedSize = ohsi.smallSize;
        ohsi.Scale();

    }

    public void WantToExplore()
    {
        gm.boat.SetTargetIsland(this);
    }

    //public void UpdateInventory(GameManager.inventorySlot slot, int amount)
    //{
    //    int slotInt = (int)slot;
    //    gm.inventoryAmounts[slotInt] += amount;
    //    gm.inventoryTexts[slotInt].SetText(gm.inventoryAmounts[slotInt].ToString());
    //}

    public void Explore()
    {

        // gm.score -= 1f;

        Finding f = nextFinding;

        if (f.name == "Wolf" && Random.value > 0.8f)
        {
            map[nextFindingIndex] = gm.deadWolf;
            f = map[nextFindingIndex];
            GameManager.instance.inventory.currentObjective.Update(TaskType.WOLF, 1);
        }
        else if (f.name == "Closed Chest" && Random.value > 0.8f)
        {
            map[nextFindingIndex] = gm.treasure;
            f = map[nextFindingIndex];
        }
        else if (f.name == "Nuke" && Random.value > 0.5f)
        {
            gm.Nuke();

            gm.poison += 9f;
            gm.score += 10f;

            f = gm.explodedHole;
            gm.RemoveLastFinding();

            gm.audioSource.PlayOneShot(gm.explotionSound, 2f);
        }

        gm.health -= f.damage;
        gm.score += f.score;
        gm.poison += f.poison;
        if (gm.poison < 0)
        {
            gm.poison = 0;
        }

        if (gm.poison > gm.poisonBar.max)
        {
            gm.poison = gm.poisonBar.max;
        }
        gm.health -= gm.poison;

        if (gm.score < 0)
        {
            gm.score = 0;
        }

        gm.UpdateUi();

        gm.lastFinding = new LastFinding(this, nextFindingIndex);
        ShowCard(f);
        ShowDrop(f);

        CreateNextFinding();
    }

    public void Bomb()
    {
        nextFinding = gm.explodedHole;
        ShowNextFinding(gm.explodedHole);
        map[nextFindingIndex] = gm.explodedHole;

    }

    public void CreateNextFinding()
    {
        int randomLocation = Random.Range(0, map.Length);
        nextFinding = map[randomLocation];
        nextFindingIndex = randomLocation;

        ShowNextFinding(nextFinding);

        ohsi.DisplayFinding();
    }

    public SpriteRenderer showingItem;

    public void ShowNextFinding(Finding f)
    {
        showingItem.sprite = f.sprite;

    }

    public void ShowDrop(Finding f)
    {
        FindingAlive findingDrop = Instantiate(gm.findingDrop, transform.position, Quaternion.identity, gm.islandFolder).GetComponent<FindingAlive>();

        findingDrop.Activate(f);
    }

    public void ShowCard(Finding f)
    {
        // gm.MoveCardToPosition();

        gm.ohsi.DisplayFinding();

        gm.slotForPicture.sprite = f.sprite;

        gm.cardText.SetText(f.funText);

        //gm.cardText.SetText(f.name + 
        //    "\nScore: " + f.score + 
        //    "\nDamage: " + f.damage +
        //    "\nPoison: " + f.poison +
        //    "\n" + f.Description
        //    );

        if (f.sound != null)
        {
            //gm.audioSource.PlayOneShot(f.sound, 1f);
            gm.audioSource.clip = f.sound;
            gm.audioSource.Play();
        }
    }

}
